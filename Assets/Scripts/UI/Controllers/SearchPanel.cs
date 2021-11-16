using Managers;
using UI.Views;
using UnityEngine;

namespace UI.Controllers
{
    public class SearchPanel : MonoBehaviour, SearchPanelView.ICallbacks
    {
        [SerializeField] private SelectPanel selectPanel;
        void SearchPanelView.ICallbacks.OnProcessInfo(string caseNumber)
        {
            UIManager.Instance.RetrieveCase(caseNumber,
                () =>
                {
                    selectPanel.gameObject.SetActive(true);
                });
        }
    }
}
