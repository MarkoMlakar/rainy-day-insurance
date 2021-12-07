using Managers;
using UI.Views;
using UnityEngine;

namespace UI.Controllers
{
    public class SearchPanel : MonoBehaviour, SearchPanelView.ICallbacks
    {
        [SerializeField] private SelectPanel selectPanel;
        [SerializeField] private LeanTweenType easeType;
        [SerializeField] private AnimationCurve easeCurve;
        [SerializeField] private float openTime;

        private Vector3 startTransformPosition;
        private float screenWidth;
        private void Awake()
        {
            ResetPosition();
            transform.position = startTransformPosition;
        }

        private void OnEnable()
        {
            if (easeType == LeanTweenType.animationCurve)
            {
                LeanTween
                    .moveX(gameObject, startTransformPosition.x - screenWidth, openTime)
                    .setEase(easeCurve);   
            }
            else
            {
                LeanTween
                    .moveX(gameObject, startTransformPosition.x - screenWidth, openTime)
                    .setEase(easeType);   
            }
        }
        
        ///<summary>
        /// Snaps the panel to the right side of the screen. With this we can create a swipe tween effect
        ///</summary>
        private void ResetPosition()
        {
            screenWidth = Screen.currentResolution.width;
            
            Vector3 transformPosition = gameObject.transform.position;
            transformPosition.x += Screen.currentResolution.width;
            
            startTransformPosition = transformPosition;
        }
        
        void SearchPanelView.ICallbacks.OnProcessInfo(string caseNumber)
        {
            UIManager.Instance.RetrieveCase(caseNumber,
                () => { selectPanel.gameObject.SetActive(true); });
        }

        void SearchPanelView.ICallbacks.OnBack()
        {
            ResetPosition();
            if (easeType == LeanTweenType.animationCurve)
            {
                LeanTween
                    .moveX(gameObject, startTransformPosition.x, openTime)
                    .setEase(easeCurve)
                    .setOnComplete(() => gameObject.SetActive(false));
            }
            else
            {
                LeanTween
                    .moveX(gameObject, startTransformPosition.x, openTime)
                    .setEase(easeType)
                    .setOnComplete(() => gameObject.SetActive(false));
            }
        }
    }
}
