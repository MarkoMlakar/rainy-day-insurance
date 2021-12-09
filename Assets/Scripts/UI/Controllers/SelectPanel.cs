using UI.Views;
using UnityEngine;

namespace UI.Controllers
{
    public class SelectPanel : MonoBehaviour, SelectPanelView.ICallbacks
    {
        [SerializeField] private GameObject overviewContainer;
        [SerializeField] private UiTweener uiTweener;

        void SelectPanelView.ICallbacks.OnProcessInfo()
        {
            overviewContainer.SetActive(true);
        }

        void SelectPanelView.ICallbacks.OnBack()
        {
            uiTweener.Hide();
        }
    }
}
