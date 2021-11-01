using TMPro;
using UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels
{
    public class OverviewPanel : MonoBehaviour, IPanel
    {
        [SerializeField] private TMP_Text caseNumberText;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text dateText;
        [SerializeField] private TMP_Text locationText;
        [SerializeField] private TMP_Text locationNotesText;
        [SerializeField] private TMP_Text imageNotesText;
        [SerializeField] private RawImage image;
        
        public void ProcessInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
