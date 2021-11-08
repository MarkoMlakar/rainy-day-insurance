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
        [SerializeField] private RawImage mapImage;
        [SerializeField] private TMP_InputField notesInput;
        [SerializeField] private Button nextButton;

        private ICallbacks _callbacks;

        private void Start()
        {
            _callbacks = locationPanel;
        }

        private void OnEnable()
        {
            caseNumberText.text = CASE_STRING_LOC + UIManager.Instance.activeCase.id;
            nextButton.onClick.AddListener(() =>
            {
                if (string.IsNullOrEmpty(notesInput.text)) return;
                _callbacks.OnProcessInfo(notesInput.text);
            });
        }
        private void OnDisable()
        {
            nextButton.onClick.RemoveAllListeners();
        }

        public void SetError(string error)
        {
            notesInput.text = error;
        }

        public void SetImage(Texture imageTexture)
        {
            mapImage.texture = imageTexture;

        }
        public interface ICallbacks
        {
            void OnProcessInfo(string locationNotes);
        }
    }
}