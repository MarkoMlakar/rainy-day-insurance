using TMPro;
using UI.Interfaces;
using UnityEngine;

namespace UI.Panels
{
    public class ClientInfoPanel : MonoBehaviour, IPanel
    {
        [SerializeField] private TMP_Text caseNumberText;
        [SerializeField] private TMP_InputField firstName;
        [SerializeField] private TMP_InputField lastName;
        public void ProcessInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
