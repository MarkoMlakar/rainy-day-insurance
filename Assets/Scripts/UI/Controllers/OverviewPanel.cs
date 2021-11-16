using System;
using Managers;
using Models;
using UI.Views;
using UnityEngine;

namespace UI.Controllers
{
    public class OverviewPanel : MonoBehaviour, OverviewPanelView.ICallbacks
    {
        void OverviewPanelView.ICallbacks.OnProcessInfo(Case newCase)
        {
            UIManager.Instance.SubmitCase(newCase);
        }
    }
}
