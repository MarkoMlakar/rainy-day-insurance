using Managers;
using TMPro;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class PhotoPanelView: MonoBehaviour
    {
        private const string CASE_STRING_LOC = "CASE NUMBER: ";
        [Header("Controller")]
        [SerializeField] private PhotoPanel photoPanel;
        [Header("View elements")]
        [SerializeField] private RawImage takenPhoto;
        [SerializeField] private TMP_InputField notes;
        [SerializeField] private TMP_Text caseNumberText;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button takePhotoButton;
        [SerializeField] private Button backButton;


        private ICallbacks _callbacks;

        private void Start()
        {
            _callbacks = photoPanel;
            
            takePhotoButton.onClick.AddListener(() =>
            {
                _callbacks.OnTakePhoto();
            });
            nextButton.onClick.AddListener(() =>
            {
                _callbacks.OnProcessInfo(notes.text, takenPhoto.texture);
            });
            
            backButton.onClick.AddListener(() =>
            {
                _callbacks.OnBack();
            });
        }

        private void OnEnable()
        {
            caseNumberText.text = CASE_STRING_LOC + UIManager.Instance.activeCase.id;
        }

        private void OnDisable()
        {
            takePhotoButton.onClick.RemoveAllListeners();
            nextButton.onClick.RemoveAllListeners();
        }

        public void SetTakenPhoto(Texture photo)
        {
            takenPhoto.texture = photo;
            takenPhoto.gameObject.SetActive(true);
        }
        
        public interface ICallbacks
        {
            void OnTakePhoto();
            void OnProcessInfo(string photoNotes, Texture photo);
            void OnBack();
        }
    }
}