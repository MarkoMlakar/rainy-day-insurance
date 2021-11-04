using System;
using Managers;
using TMPro;
using UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels
{
    public class OverviewPanel : MonoBehaviour, IPanel
    {
        private const string CASE_NUMBER_TEXT_LOC = "CASE NUMBER: ";
        private const string LOCATION_NOTES_TEXT_LOC = "LOCATION NOTES: \n";
        private const string IMAGE_NOTES_TEXT_LOC = "PHOTO NOTES: \n";
        [SerializeField] private TMP_Text caseNumberText;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text dateText;
        [SerializeField] private TMP_Text locationText;
        [SerializeField] private TMP_Text locationNotesText;
        [SerializeField] private TMP_Text imageNotesText;
        [SerializeField] private RawImage image;

        private void OnEnable()
        {
            caseNumberText.text = CASE_NUMBER_TEXT_LOC + UIManager.Instance.activeCase.id;
            nameText.text = UIManager.Instance.activeCase.name;
            dateText.text = DateTime.Today.ToString();
            locationNotesText.text = LOCATION_NOTES_TEXT_LOC + UIManager.Instance.activeCase.locationNotes;
            imageNotesText.text = IMAGE_NOTES_TEXT_LOC + UIManager.Instance.activeCase.photoNotes;
            image.texture = UIManager.Instance.activeCase.photoTaken;
        }

        public void ProcessInfo()
        {
            print("<color=green>Submit to AWS </color>");
        }
    }
}
