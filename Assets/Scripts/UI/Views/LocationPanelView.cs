using Managers;
using TMPro;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class LocationPanelView: MonoBehaviour
    {
        private const string CASE_STRING_LOC = "CASE NUMBER: ";
        [Header("Controller")] 
        [SerializeField] private LocationPanel locationPanel;
        [Header("View elements")]
        [SerializeField] private TMP_Text caseNumberText;
        [SerializeField] private TMP_Text imageErrorText;
        [SerializeField] private RawImage mapImage;
        [SerializeField] private TMP_InputField notesInput;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button backButton;
        [SerializeField] private GameObject loading;


        private ICallbacks _callbacks;
        
        public interface ICallbacks
        {
            void OnProcessInfo(string locationNotes);
            void OnBack();
        }
        
        public void SetLoading(bool isLoading)
        {
            loading.SetActive(isLoading);
        }
        
        public void SetError(string error)
        {
            imageErrorText.text = error;
        }

        public void SetImage(Texture imageTexture)
        {
            mapImage.gameObject.SetActive(true);
            mapImage.texture = imageTexture;
        }

        private void Start()
        {
            _callbacks = locationPanel;
            
            nextButton.onClick.AddListener(() =>
            {
                _callbacks.OnProcessInfo(notesInput.text);
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
            mapImage.gameObject.SetActive(false);
        }
    }
}