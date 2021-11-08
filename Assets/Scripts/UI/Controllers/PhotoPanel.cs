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
        void PhotoPanelView.ICallbacks.OnTakePhoto()
        {
            NativeCamera.Permission permission = NativeCamera.TakePicture( ( path ) =>
            {
                Debug.Log( "Image path: " + path );
                if( path != null )
                {
                    // Create a Texture2D from the captured image
                    Texture2D texture = NativeCamera.LoadImageAtPath( path, photoMaxSize );
                    if( texture == null )
                    {
                        Debug.Log( "Couldn't load texture from " + path );
                        return;
                    }

                    /*// Assign texture to a temporary quad and destroy it after 5 seconds
                    GameObject quad = GameObject.CreatePrimitive( PrimitiveType.Quad );
                    quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
                    quad.transform.forward = Camera.main.transform.forward;
                    quad.transform.localScale = new Vector3( 1f, texture.height / (float) texture.width, 1f );
			
                    Material material = quad.GetComponent<Renderer>().material;
                    if( !material.shader.isSupported ) // happens when Standard shader is not included in the build
                        material.shader = Shader.Find( "Legacy Shaders/Diffuse" );

                    material.mainTexture = texture;
				
                    Destroy( quad, 5f );

                    // If a procedural texture is not destroyed manually, 
                    // it will only be freed after a scene change
                    Destroy( texture, 5f );
                    */
                    photoPanelView.SetTakenPhoto(texture);
                }
            }, photoMaxSize );

            Debug.Log( "Permission result: " + permission );        
        }

        void PhotoPanelView.ICallbacks.OnProcessInfo(string photoNotes, Texture photo)
        {
            UIManager.Instance.activeCase.photoNotes = photoNotes;
            UIManager.Instance.activeCase.photoTaken = photo;
            overviewPanel.SetActive(true);
        }
    }
}
