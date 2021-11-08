using Managers;
using UI.Views;
using UnityEngine;

namespace UI.Controllers
{
    public class ClientInfoPanel : MonoBehaviour, ClientInfoPanelView.ICallbacks
    {
        [SerializeField] private LocationPanel locationPanel;
        void ClientInfoPanelView.ICallbacks.OnProcessInfo(string firstName, string lastName)
        {
            UIManager.Instance.activeCase.name = firstName + " " + lastName;
            locationPanel.gameObject.SetActive(true);
        }
    }
}
