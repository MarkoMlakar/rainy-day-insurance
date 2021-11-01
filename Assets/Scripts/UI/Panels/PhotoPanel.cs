using TMPro;
using UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels
{
    public class PhotoPanel : MonoBehaviour, IPanel
    {
        [SerializeField] private RawImage takenPhoto;
        [SerializeField] private TMP_InputField notes;
        [SerializeField] private TMP_Text caseNumberText;
        public void ProcessInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
