using System;
using Managers;
using Models;
using TMPro;
using UI.Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Views
{
    public class OverviewPanelView: MonoBehaviour
    {
        private const string CASE_NUMBER_TEXT = "CASE NUMBER: ";
        private const string LOCATION_NOTES_TEXT = "LOCATION NOTES: \n";
        private const string IMAGE_NOTES_TEXT = "PHOTO NOTES: \n";
        private const string ERROR_TEXT = "The case could not be submited. Please try again.";

        [Header("Controller")] 
        [SerializeField] private OverviewPanel overviewPanel;
        [Header("View elements")]
        [SerializeField] private TMP_Text caseNumberText;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text dateText;
        [SerializeField] private TMP_Text locationText;
        [SerializeField] private TMP_Text locationNotesText;
        [SerializeField] private TMP_Text imageNotesText;
        [SerializeField] private RawImage image;
        [SerializeField] private Button submitButton;
        [SerializeField] private Button backButton;
        [SerializeField] private GameObject loading;
        [SerializeField] private TMP_Text errorText;
        [SerializeField] private TMP_Text submitText;
        [SerializeField] private Scrollbar scrollbar;

        private ICallbacks _callbacks;

        private void Start()
        {
            _callbacks = overviewPanel;
        }

        private void OnEnable()
        {
            caseNumberText.text = CASE_NUMBER_TEXT + UIManager.Instance.activeCase.id;
            nameText.text = UIManager.Instance.activeCase.name;
            dateText.text = DateTime.Today.ToString();
            locationNotesText.text = LOCATION_NOTES_TEXT + UIManager.Instance.activeCase.locationNotes;
            imageNotesText.text = IMAGE_NOTES_TEXT + UIManager.Instance.activeCase.photoNotes;
            Texture2D reconstructedImage = new Texture2D(1, 1);
            reconstructedImage.LoadImage(UIManager.Instance.activeCase.photoTaken);
            image.texture = reconstructedImage;
            scrollbar.value = 1;
            submitButton.onClick.AddListener(() =>
            {
                ToggleLoading(true);
                _callbacks.OnProcessInfo(UIManager.Instance.activeCase, 
                    () =>
                    {
                        ToggleLoading(false);
                        SceneManager.LoadScene(0);
                    },
                    () =>
                    {
                        errorText.text = ERROR_TEXT;
                    });
            });
            
            backButton.onClick.AddListener(() =>
            {
                _callbacks.OnBack();
            });
        }

        private void OnDisable()
        {
            submitButton.onClick.RemoveAllListeners();
        }
        
        private void ToggleLoading(bool isLoading)
        {
            submitText.gameObject.SetActive(!isLoading);
            loading.SetActive(isLoading);
            submitButton.interactable = !isLoading;
        }


        public interface ICallbacks
        {
            void OnProcessInfo(Case newCase, Action onComplete, Action onError);
            void OnBack();
        }
    }
}