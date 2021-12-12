using UI.Views;
using UnityEngine;

namespace UI.Controllers
{
    public class MainMenuPanel : MonoBehaviour, MainMenuPanelView.ICallbacks
    {
        [SerializeField] private GameObject searchPanel;
        [SerializeField] private GameObject createCasePanel;
        void MainMenuPanelView.ICallbacks.OnFindCase()
        {
            searchPanel.SetActive(true);
        }

        void MainMenuPanelView.ICallbacks.OnCreateCase()
        {
            createCasePanel.SetActive(true);
        }
    }
}
