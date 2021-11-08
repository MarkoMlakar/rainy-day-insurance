using System;
using TMPro;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class SearchPanelView: MonoBehaviour
    {
        [Header("Controller")] 
        [SerializeField] private SearchPanel searchPanel;
        [Header("View elements")]
        [SerializeField] private TMP_InputField caseNumberInput;
        [SerializeField] private Button searchButton;

        private ICallbacks _callbacks;

        private void Start()
        {
            throw new NotImplementedException();
        }

        private void OnEnable()
        {
            searchButton.onClick.AddListener(() =>
            {
                _callbacks.OnProcessInfo();
            });
        }

        private void OnDisable()
        {
            searchButton.onClick.RemoveAllListeners();
        }
        public interface ICallbacks
        {
            void OnProcessInfo();
        }
    }
}