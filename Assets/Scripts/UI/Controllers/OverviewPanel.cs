using System;
using Models;
using UI.Views;
using UnityEngine;

namespace UI.Controllers
{
    public class OverviewPanel : MonoBehaviour, OverviewPanelView.ICallbacks
    {
        void OverviewPanelView.ICallbacks.OnProcessInfo(Case newCase)
        {
            print("New case created with ID: " + newCase.id);
            throw new NotImplementedException("Save to AWS");
        }
    }
}
