using Managers;
using TMPro;
using UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels
{
    public class PhotoPanel : MonoBehaviour, IPanel
    {
        private const string CASE_STRING_LOC = "CASE NUMBER: ";
        [SerializeField] private RawImage takenPhoto;
        [SerializeField] private TMP_InputField notes;
        [SerializeField] private TMP_Text caseNumberText;

        private void OnEnable()
        {
            caseNumberText.text = CASE_STRING_LOC + UIManager.Instance.activeCase.id;
        }

        public void ProcessInfo()
        {
            if (string.IsNullOrEmpty(notes.text) || takenPhoto.texture == null) return;

            UIManager.Instance.activeCase.photoNotes = notes.text;
            UIManager.Instance.activeCase.photoTaken = takenPhoto;
        }

        public void TakePicture( int maxSize )
        {
            NativeCamera.Permission permission = NativeCamera.TakePicture( ( path ) =>
            {
                Debug.Log( "Image path: " + path );
                if( path != null )
                {
                    // Create a Texture2D from the captured image
                    Texture2D texture = NativeCamera.LoadImageAtPath( path, maxSize );
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
                    takenPhoto.texture = texture;
                    takenPhoto.gameObject.SetActive(true);
                }
            }, maxSize );

            Debug.Log( "Permission result: " + permission );
        }
    }
}
