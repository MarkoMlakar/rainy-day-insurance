using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Firebase.Extensions;
using Firebase.Storage;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class FirebaseManager : MonoBehaviour
    {
        private const long MAX_ALLOWED_SIZE = 20000000; // bytes

        private static FirebaseManager _instance;
        private FirebaseStorage _storage;
        private StorageReference _storageRef;

        public static FirebaseManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("Firebase Manager is null!");
                }

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
            _storage = FirebaseStorage.DefaultInstance;
            _storageRef = _storage.RootReference;
        }

        public void UploadToFirebaseStorage(string bucketDirectory, Stream dataStream, string fileName, Action onComplete, Action onError)
        {
            StorageReference caseFileRef = _storageRef.Child(bucketDirectory + "/" + fileName);
            caseFileRef
                .PutStreamAsync(dataStream)
                .ContinueWithOnMainThread((Task<StorageMetadata> task) =>
                {
                    if (task.IsFaulted || task.IsCanceled)
                    {
                        Debug.LogError(task.Exception.ToString());
                    }
                    else
                    {
                        SceneManager.LoadScene(0);
                    }
                });
        }

        public void DownloadFromFirebaseStorage(string bucketDirectory, string fileId, Action onComplete = null, 
            Action onError = null) 
        {
            _storageRef.Child(bucketDirectory + "/" + "case#"+fileId+".dat")
                .GetBytesAsync(MAX_ALLOWED_SIZE)
                .ContinueWithOnMainThread(task => {
                if (task.IsFaulted || task.IsCanceled) {
                    onError?.Invoke();
                }
                else {
                    byte[] fileContents = task.Result;
                    using (MemoryStream memory = new MemoryStream(fileContents))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        Case downloadedCase = (Case) bf.Deserialize(memory);
                        UIManager.Instance.activeCase = downloadedCase;
                        onComplete?.Invoke();
                    }
                }
                });
        }
    }
}
