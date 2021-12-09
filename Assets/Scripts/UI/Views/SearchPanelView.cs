using System;
using TMPro;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class SearchPanelView: MonoBehaviour
    {
        private const string ERROR_MESSAGE =
            "Case file does not exist.";

        private const string MISSING_CASE_NUMBER_MESSAGE = "Please enter a case number";
        
        [SerializeField] private SearchPanel searchPanel;
        [SerializeField] private TMP_InputField caseNumberInput;
        [SerializeField] private Button searchButton;
        [SerializeField] private Button backButton;
        [SerializeField] private GameObject loading;
        [SerializeField] private TMP_Text searchText;
        [SerializeField] private TMP_Text errorText;
 
        private ICallbacks _callbacks;
        public interface ICallbacks
        {
            void OnProcessInfo(string caseNumberInput, Action onComplete, Action onError);
            void OnBack();
        }
        private void Start()
        {
            _callbacks = searchPanel;
        }

        private void OnEnable()
        {
            searchButton.onClick.AddListener(() =>
            {
                if (string.IsNullOrEmpty(caseNumberInput.text))
                {
                    errorText.text = MISSING_CASE_NUMBER_MESSAGE;
                    return;
                }
                
                ToggleLoading(true);
                _callbacks.OnProcessInfo(caseNumberInput.text, 
                    () =>
                {
                    OnCloseReset();
                },
                    () =>
                    {
                        errorText.text = ERROR_MESSAGE;
                        ToggleLoading(false);
                    });
            });
            
            backButton.onClick.AddListener(() =>
            {
                _callbacks.OnBack();
                OnCloseReset();
            });
        }

        private void OnDisable()
        {
            searchButton.onClick.RemoveAllListeners();
        }

        private void ToggleLoading(bool isLoading)
        {
            searchText.gameObject.SetActive(!isLoading);
            loading.SetActive(isLoading);
            searchButton.interactable = !isLoading;
        }

        private void OnCloseReset()
        {
            ToggleLoading(false);
            caseNumberInput.text = String.Empty;
            errorText.text = String.Empty;
        }
    }
}