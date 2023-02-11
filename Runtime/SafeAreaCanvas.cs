//source: https://forum.unity.com/threads/canvashelper-resizes-a-recttransform-to-iphone-xs-safe-area.521107

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TKLibs
{
    [RequireComponent(typeof(Canvas))]
    public class SafeAreaCanvas : MonoBehaviour
    {
        static readonly List<SafeAreaCanvas> Helpers = new();

        public static readonly UnityEvent OnResolutionOrOrientationChanged = new();

        static bool _screenChangeVarsInitialized;
        static ScreenOrientation _lastOrientation = ScreenOrientation.LandscapeLeft;
        static Vector2 _lastResolution = Vector2.zero;
        static Rect _lastSafeArea = Rect.zero;

        Canvas _canvas;
        [SerializeField] RectTransform safeAreaTransform;

        void Awake()
        {
            if (!Helpers.Contains(this))
                Helpers.Add(this);

            _canvas = GetComponent<Canvas>();

            if (!_screenChangeVarsInitialized)
            {
                _lastOrientation = Screen.orientation;
                _lastResolution.x = Screen.width;
                _lastResolution.y = Screen.height;
                _lastSafeArea = Screen.safeArea;

                _screenChangeVarsInitialized = true;
            }

            ApplySafeArea();
        }

        void Update()
        {
            if (Helpers[0] != this)
                return;

            if (Application.isMobilePlatform && Screen.orientation != _lastOrientation)
                OrientationChanged();

            if (Screen.safeArea != _lastSafeArea)
                SafeAreaChanged();

            if (Screen.width != _lastResolution.x || Screen.height != _lastResolution.y)
                ResolutionChanged();
        }

        void ApplySafeArea()
        {
            if (safeAreaTransform == null)
                return;

            var safeArea = Screen.safeArea;

            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;
            var pixelRect = _canvas.pixelRect;
            anchorMin.x /= pixelRect.width;
            anchorMin.y /= pixelRect.height;
            anchorMax.x /= pixelRect.width;
            anchorMax.y /= pixelRect.height;

            safeAreaTransform.anchorMin = anchorMin;
            safeAreaTransform.anchorMax = anchorMax;
        }

        void OnDestroy()
        {
            if (Helpers != null && Helpers.Contains(this))
                Helpers.Remove(this);
        }

        static void OrientationChanged()
        {
            _lastOrientation = Screen.orientation;
            _lastResolution.x = Screen.width;
            _lastResolution.y = Screen.height;

            OnResolutionOrOrientationChanged.Invoke();
        }

        static void ResolutionChanged()
        {
            _lastResolution.x = Screen.width;
            _lastResolution.y = Screen.height;

            OnResolutionOrOrientationChanged.Invoke();
        }

        static void SafeAreaChanged()
        {
            _lastSafeArea = Screen.safeArea;

            foreach (var helper in Helpers) helper.ApplySafeArea();
        }
    }
}