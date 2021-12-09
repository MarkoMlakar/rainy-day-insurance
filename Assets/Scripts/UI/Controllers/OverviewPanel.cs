using System;
using Managers;
using Models;
using UI.Views;
using UnityEngine;

namespace UI.Controllers
{
    public class OverviewPanel : MonoBehaviour, OverviewPanelView.ICallbacks
    {
        [SerializeField] private UiTweener uiTweener;
        void OverviewPanelView.ICallbacks.OnProcessInfo(Case newCase, Action onComplete, Action onError)
        {
            UIManager.Instance.SubmitCase(newCase, 
                () =>
            {
                onComplete?.Invoke();

            }, 
                () =>
            {
                onError?.Invoke();
            });
        }

        void OverviewPanelView.ICallbacks.OnBack()
        {
            uiTweener.Hide();
        }
    }
}
