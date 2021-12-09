using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Models;
using UI.Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;
        
        [SerializeField] private ClientInfoPanel clientInfoPanel;
        
        public Case activeCase;
        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.Log("UIManager is NULL");
                
                return _instance;
            }
        }

        
        public void CreateNewCase()
        {
            activeCase = new Case();
            int randomCaseId = Random.Range(0, 1000);
            activeCase.id = randomCaseId.ToString();
            clientInfoPanel.gameObject.SetActive(true);
        }

        public void SubmitCase(Case newCase, Action onComplete=null, Action onError = null)
        {
            BinaryFormatter bf = new BinaryFormatter();
            string filePath = Application.persistentDataPath + "/case#" + newCase.id + ".dat";
            FileStream file = File.Create(filePath);
            bf.Serialize(file, newCase);
            file.Close();
            Stream dataStream = new FileStream(filePath, FileMode.Open);
            
            // Upload case file to Firebase
            FirebaseManager.Instance.UploadToFirebaseStorage("CaseFiles/", dataStream,
                "/case#" + newCase.id + ".dat",onComplete,onError );
        }

        public void RetrieveCase(string caseId, Action onComplete=null, Action onError = null)
        {
            FirebaseManager.Instance.DownloadFromFirebaseStorage("CaseFiles", 
                caseId, onComplete, onError);
        }
        private void Awake()
        {
            _instance = this;
        }
    }
}
