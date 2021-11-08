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
        [SerializeField] private TMP_InputField firstName;
        [SerializeField] private TMP_InputField lastName;
        [SerializeField] private Button nextButton;

        private ICallbacks _callbacks;
        
        private void Start()
        {
            _callbacks = clientInfoPanel;
        }
        private void OnEnable()
        {
            caseNumberText.text = CASE_ID_LOC + UIManager.Instance.activeCase.id;
            
            nextButton.onClick.AddListener(() =>
            {
                if (IsNullOrEmpty(firstName.text) || IsNullOrEmpty(lastName.text))
                {
                    print("<color=red>First name or last name is empty and we can not continue</color>");
                    // TODO: Display the error message on the screen
                    return;
                }
                _callbacks.OnProcessInfo(firstName.text, lastName.text);
                gameObject.SetActive(false);
            });
        }

        private void OnDisable()
        {
            nextButton.onClick.RemoveAllListeners();
            firstName.text = Empty;
            lastName.text = Empty;
        }
        public interface ICallbacks
        {
            void OnProcessInfo(string firstName, string lastName);
        }
    }
}