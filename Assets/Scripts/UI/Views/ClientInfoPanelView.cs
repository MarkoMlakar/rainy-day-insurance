using Managers;
using TMPro;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;
using static System.String;

namespace UI.Views
{
    public class ClientInfoPanelView: MonoBehaviour
    {
        private const string CASE_ID_LOC = "CASE ID: ";
        [Header("Controller")]
        [SerializeField] private ClientInfoPanel clientInfoPanel;
        [Header("View elements")]
        [SerializeField] private TMP_Text caseNumberText;
        [SerializeField] private TMP_Text firstNameErrorText;
        [SerializeField] private TMP_Text lastNameErrorText;
        [SerializeField] private TMP_InputField firstName;
        [SerializeField] private TMP_InputField lastName;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button backButton;
        [SerializeField] private GameObject loading;

        private ICallbacks _callbacks;
        
        public interface ICallbacks
        {
            void OnProcessInfo(string firstName, string lastName);
            void OnBack();
        }
        
        public void SetLoading(bool isLoading)
        {
            loading.SetActive(isLoading);
        }
        
        private void Start()
        {
            _callbacks = clientInfoPanel;
            
            nextButton.onClick.AddListener(() =>
            {
                if (IsNullOrEmpty(firstName.text) || IsNullOrEmpty(lastName.text))
                {
                    if (IsNullOrEmpty(firstName.text))
                    {
                        firstNameErrorText.text = "Please enter the first name";
                    }  
                    else if(IsNullOrEmpty(lastName.text))
                    {
                        lastNameErrorText.text = "Please enter the last name";
                    }
                    return;
                }
                _callbacks.OnProcessInfo(firstName.text, lastName.text);
            });
            
            backButton.onClick.AddListener(() => 
            {
                _callbacks.OnBack();
            });
        }
        private void OnEnable()
        {
            caseNumberText.text = CASE_ID_LOC + UIManager.Instance.activeCase.id;
           
        }

        private void OnDisable()
        {
            firstName.text = Empty;
            lastName.text = Empty;
        }
    }
}