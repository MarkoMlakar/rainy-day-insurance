using System;
using Managers;
using UI.Views;
using UnityEngine;

namespace UI.Controllers
{
    public class SearchPanel : MonoBehaviour, SearchPanelView.ICallbacks
    {
        [SerializeField] private SelectPanel selectPanel;
        [SerializeField] private UiTweener uiTweener;
        void SearchPanelView.ICallbacks.OnProcessInfo(string caseNumber, Action onComplete, Action onError)
        {
            UIManager.Instance.RetrieveCase(caseNumber,
                () =>
                {
                    selectPanel.gameObject.SetActive(true);
                    onComplete?.Invoke();
                },
                () =>
                {
                    onError?.Invoke();
                });
            
        }

        void SearchPanelView.ICallbacks.OnBack()
        {
            uiTweener.Hide();
        }
    }
}
