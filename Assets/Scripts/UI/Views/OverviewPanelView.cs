using System;
using Managers;
using Models;
using TMPro;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class OverviewPanelView: MonoBehaviour
    {
        private const string CASE_NUMBER_TEXT_LOC = "CASE NUMBER: ";
        private const string LOCATION_NOTES_TEXT_LOC = "LOCATION NOTES: \n";
        private const string IMAGE_NOTES_TEXT_LOC = "PHOTO NOTES: \n";

        [Header("Controller")] 
        [SerializeField] private OverviewPanel overviewPanel;
        [Header("View elements")]
        [SerializeField] private TMP_Text caseNumberText;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text dateText;
        [SerializeField] private TMP_Text locationText;
        [SerializeField] private TMP_Text locationNotesText;
        [SerializeField] private TMP_Text imageNotesText;
        [SerializeField] private RawImage image;
        [SerializeField] private Button submitButton;

        private ICallbacks _callbacks;

        private void Start()
        {
            _callbacks = overviewPanel;
        }

        private void OnEnable()
        {
            caseNumberText.text = CASE_NUMBER_TEXT_LOC + UIManager.Instance.activeCase.id;
            nameText.text = UIManager.Instance.activeCase.name;
            dateText.text = DateTime.Today.ToString();
            locationNotesText.text = LOCATION_NOTES_TEXT_LOC + UIManager.Instance.activeCase.locationNotes;
            imageNotesText.text = IMAGE_NOTES_TEXT_LOC + UIManager.Instance.activeCase.photoNotes;
            Texture2D reconstructedImage = new Texture2D(1, 1);
            reconstructedImage.LoadImage(UIManager.Instance.activeCase.photoTaken);
            image.texture = reconstructedImage;   
            submitButton.onClick.AddListener(() =>
            {
                _callbacks.OnProcessInfo(UIManager.Instance.activeCase);
            });
        }

        private void OnDisable()
        {
            submitButton.onClick.RemoveAllListeners();
        }


        public interface ICallbacks
        {
            void OnProcessInfo(Case newCase);
        }
    }
}