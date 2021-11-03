using System;
using Managers;
using TMPro;
using UI.Interfaces;
using UnityEngine;

namespace UI.Panels
{
    public class ClientInfoPanel : MonoBehaviour, IPanel
    {
        [SerializeField] private TMP_Text caseNumberText;
        [SerializeField] private TMP_InputField firstName;
        [SerializeField] private TMP_InputField lastName;
        [SerializeField] private LocationPanel locationPanel;

        private void OnEnable()
        {
            caseNumberText.text = "CASE ID: " + UIManager.Instance.activeCase.id;
        }

        public void ProcessInfo()
        {
            if (string.IsNullOrEmpty(firstName.text) || string.IsNullOrEmpty(lastName.text))
            {
                print("<color=red>First name or last name is empty and we can not continue</color>");
                return;
            }

            UIManager.Instance.activeCase.name = firstName.text + " " + lastName.text;
            locationPanel.gameObject.SetActive(true);
        }
    }
}
