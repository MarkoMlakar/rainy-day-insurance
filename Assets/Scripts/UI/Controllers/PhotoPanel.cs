using Managers;
using UI.Views;
using UnityEngine;

namespace UI.Controllers
{
    public class PhotoPanel : MonoBehaviour, PhotoPanelView.ICallbacks
    {
        [SerializeField] private GameObject overviewPanel;
        [Header("View")] 
        [SerializeField] private PhotoPanelView photoPanelView;

        [SerializeField] private int photoMaxSize;

        private string _imagePath;
        void PhotoPanelView.ICallbacks.OnTakePhoto()
        {
            NativeCamera.Permission permission = NativeCamera.TakePicture( ( path ) =>
            {
                Debug.Log( "Image path: " + path );
                if( path != null )
                {
                    // Create a Texture2D from the captured image
                    Texture2D texture = NativeCamera.LoadImageAtPath( path, photoMaxSize,false);
                    if( texture == null )
                    {
                        Debug.Log( "Couldn't load texture from " + path );
                        return;
                    }
                    
                    photoPanelView.SetTakenPhoto(texture);
                    _imagePath = path;
                }
            }, photoMaxSize );

            Debug.Log( "Permission result: " + permission );        
        }

        void PhotoPanelView.ICallbacks.OnProcessInfo(string photoNotes, Texture photo)
        {
            UIManager.Instance.activeCase.photoNotes = photoNotes;

            Texture2D img = NativeCamera.LoadImageAtPath(_imagePath, 512, false);
            byte[] imageData = img.EncodeToPNG();
            UIManager.Instance.activeCase.photoTaken = imageData;
            overviewPanel.SetActive(true);
        }
    }
}
