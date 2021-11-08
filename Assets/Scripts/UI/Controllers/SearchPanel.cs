using UI.Views;
using UnityEngine;

namespace UI.Controllers
{
    public class SearchPanel : MonoBehaviour, SearchPanelView.ICallbacks
    {
        void SearchPanelView.ICallbacks.OnProcessInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
