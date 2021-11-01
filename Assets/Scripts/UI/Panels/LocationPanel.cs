using TMPro;
using UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels
{
    public class LocationPanel : MonoBehaviour, IPanel
    {
        [SerializeField] private RawImage mapImage;
        [SerializeField] private TMP_InputField notesInput;
        public void ProcessInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
