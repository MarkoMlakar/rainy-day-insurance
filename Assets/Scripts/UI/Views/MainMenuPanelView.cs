using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class MainMenuPanelView : MonoBehaviour
    {
        [SerializeField] private Button findCaseButton;
        [SerializeField] private Button createCaseButton;
        [SerializeField] private MainMenuPanel mainMenuPanel;
        
        private ICallbacks _callbacks;
        public interface ICallbacks
        {
            void OnFindCase();
            void OnCreateCase();
        }

        private void Start()
        {
            _callbacks = mainMenuPanel;
            findCaseButton.onClick.AddListener(() =>
            {
                _callbacks.OnFindCase();
            });
            
            createCaseButton.onClick.AddListener(() =>
            {
                _callbacks.OnCreateCase();
            });
        }
    }
}
