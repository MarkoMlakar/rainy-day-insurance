using System;
using TMPro;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class SelectPanelView: MonoBehaviour
    {
        [Header("Controller")]
        [SerializeField] private SelectPanel selectPanel;
        [Header("View elements")]
        [SerializeField] private TMP_Text informationText;
        [SerializeField] private Button acceptButton;

        private ICallbacks _callbacks;
        private void Start()
        {
            _callbacks = selectPanel;
        }
        
        private void OnEnable()
        {
            acceptButton.onClick.AddListener(() =>
            {
                _callbacks.OnProcessInfo();
            });
        }

        private void OnDisable()
        {
            acceptButton.onClick.RemoveAllListeners();
        }
        
        public interface ICallbacks
        {
            void OnProcessInfo();
        }
    }
}