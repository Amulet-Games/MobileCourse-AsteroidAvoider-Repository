using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace AG
{
    public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [Header("Debug.")]
        public bool testMode = true;

        [Header("Refs.")]
        [ReadOnlyInspector] public GameOverHandler gameoverHandler;

        [Header("Static")]
        public static AdManager singleton;

#if UNITY_ANDROID
    private string gameId = "4676729";
#endif

        #region Callbacks.
        private void Awake()
        {
            // Set Singleton
            if (singleton != null)
                Destroy(gameObject);
            else
                singleton = this;

            // Don't Destroy when scene changed
            DontDestroyOnLoad(gameObject);

            // Initialize Adevertisement
            Advertisement.Initialize(gameId, testMode, this);
        }
        #endregion

        #region UI Button.
        public void UIButton_Continue()
        {
            Advertisement.Show("rewardedVideo", this);
        }
        #endregion

        #region Ads Init.
        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads initialization Failed: {error} - {message}");
        }
        #endregion

        #region Ads Loaded.
        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log($"Ad Loaded: {placementId}");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit {placementId}: {error} - {message}");
        }
        #endregion

        #region Ads Showed.
        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
        }

        public void OnUnityAdsShowStart(string placementId)
        {
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.UNKNOWN:
                    Debug.LogWarning("Ad Failed");
                    break;
                case UnityAdsShowCompletionState.SKIPPED:
                    // Ad was skipped
                    break;
                case UnityAdsShowCompletionState.COMPLETED:
                    gameoverHandler.ContinueGame();
                    break;
            }
        }
        #endregion
    }
}