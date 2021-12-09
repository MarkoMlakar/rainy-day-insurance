using System;
using System.Data;
using UnityEngine;

namespace UI
{
    public enum UiAnimationTypes
    {
        Move,
        Fade,
        Hover,
        Rotate
    }

    public enum UiStartSide
    {
        Left,
        Top,
        Right,
        Bottom,
        Center
    }
    public class UiTweener : MonoBehaviour
    {
        [SerializeField] private GameObject objectToTween;
        [SerializeField] private UiAnimationTypes animationType;
        [SerializeField] private UiStartSide startSideOffset;
        [SerializeField] private LeanTweenType easeType;
        [SerializeField] private AnimationCurve easeCurve;
        [SerializeField] private float duration;
        [SerializeField] private float delay;
        [SerializeField] private Vector3 to;

        private LTDescr _tweenObject;
        private Resolution _screenResolution;
        private Vector2 _startTransformPosition;
        private Vector2 _endTransformPosition;
        private RectTransform _targetObjectRect;

        public void Show()
        {
            if (objectToTween == null)
                objectToTween = gameObject;

            SetOffset();
            switch(animationType)
            {
                case UiAnimationTypes.Move:
                    Move(false);
                    break;
                case UiAnimationTypes.Fade:
                    break;
                case UiAnimationTypes.Hover:
                    Hover();
                    break;
                case UiAnimationTypes.Rotate:
                    Rotate();
                    break;
            }

            _tweenObject.setDelay(delay);
            _tweenObject.setEase(easeType);
        }

        public void Hide()
        {
            SetOffset();
            switch(animationType)
            {
                case UiAnimationTypes.Move:
                    Move(true);
                    break;
                case UiAnimationTypes.Fade:
                    break;
            }
        }

        private void Awake()
        {
            _screenResolution = Screen.currentResolution;
            _targetObjectRect = objectToTween.GetComponent<RectTransform>();
        }
        private void OnEnable()
        {
            Show();
        }

        private void SetOffset()
        {
            Vector2 transformPosition = _targetObjectRect.anchoredPosition;
            switch (startSideOffset)
            {
                case UiStartSide.Left:
                    transformPosition.x -= _screenResolution.width;
                    break;
                case UiStartSide.Right:
                    transformPosition.x += _screenResolution.width;
                    break;
                case UiStartSide.Top:
                    transformPosition.y += _screenResolution.height;
                    break;
                case UiStartSide.Bottom:
                    transformPosition.y -= _screenResolution.height;
                    break;
                case UiStartSide.Center:
                    break;
            }
            _startTransformPosition = transformPosition;
        }

        private void Move(bool isClosing)
        {
            if (isClosing)
            {
                _tweenObject = LeanTween.move(_targetObjectRect, _startTransformPosition, duration).setOnComplete(() =>
                {
                    objectToTween.SetActive(false);
                });   

            }
            else
            {
                _targetObjectRect.anchoredPosition = _startTransformPosition;
                _tweenObject = LeanTween.move(_targetObjectRect, to, duration);   
            }
        }

        private void Fade()
        {
            throw new NotImplementedException();
        }

        private void Hover()
        {
            _tweenObject = LeanTween.scale(_targetObjectRect, to, duration).setLoopPingPong();
        }

        private void Rotate()
        {
            _tweenObject = LeanTween.rotateAround(gameObject, Vector3.forward, to.z, duration).setLoopClamp();
        }
    }
}
