using UI.Views;
using UnityEngine;

namespace UI.Controllers
{
    public class SelectPanel : MonoBehaviour, SelectPanelView.ICallbacks
    {
        [SerializeField] private GameObject overviewContainer;
        void SelectPanelView.ICallbacks.OnProcessInfo()
        {
            overviewContainer.SetActive(true);
        }
    }
}
