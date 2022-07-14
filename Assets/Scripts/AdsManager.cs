// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Advertisements;

// public class AdsManager : MonoBehaviour
// {
//   string RewardedVideoAdID = "rewardedVideo";
//   string showOptionsGamerSId;
//   public void ShowRewardedAd()
//   {
//     if (Advertisement.isInitialized)
//     {
//       var options = new ShowOptions();
//       Advertisement.Show("rewardedVideo", options);
//     }
//   }

//   public void HandleShowResult(ShowResult result)
//   {
//     switch (result)
//     {
//       case ShowResult.Finished:
//         Debug.Log("The ad was successfully shown.");
//         break;
//       case ShowResult.Skipped:
//         Debug.Log("The ad was skipped before reaching the end.");
//         break;
//       case ShowResult.Failed:
//         Debug.LogError("The ad failed to be shown.");
//         break;
//     }
//   }
// }

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
  [SerializeField] Button _showAdButton;
  [SerializeField] string _androidAdUnitId = "Rewarded_Android";
  [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
  string _adUnitId = null; // This will remain null for unsupported platforms

  void Awake()
  {
    // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
    _adUnitId = _androidAdUnitId;
#endif

    //Disable the button until the ad is ready to show:
    _showAdButton.interactable = false;
  }

  // Load content to the Ad Unit:
  public void LoadAd()
  {
    // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
    Debug.Log("1 Loading Ad: " + _adUnitId);
    Advertisement.Load(_adUnitId, this);
    Debug.Log("2 Loading Ad: " + _adUnitId);
  }

  // If the ad successfully loads, add a listener to the button and enable it:
  public void OnUnityAdsAdLoaded(string adUnitId)
  {
    Debug.Log("Ad Loaded: " + adUnitId);

    if (adUnitId.Equals(_adUnitId))
    {
      // Configure the button to call the ShowAd() method when clicked:
      _showAdButton.onClick.AddListener(ShowAd);
      // Enable the button for users to click:
      _showAdButton.interactable = true;
    }
  }

  // Implement a method to execute when the user clicks the button:
  public void ShowAd()
  {
    Debug.Log("1 Showing Ad: " + _adUnitId);
    // Disable the button:
    _showAdButton.interactable = false;
    // Then show the ad:
    Advertisement.Show(_adUnitId, this);
    Debug.Log("2 Showing Ad: " + _adUnitId);

  }

  // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
  public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
  {
    if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
    {
      Debug.Log("Unity Ads Rewarded Ad Completed");
      // Grant a reward.

      // Load another ad:
      Advertisement.Load(_adUnitId, this);
    }
  }

  // Implement Load and Show Listener error callbacks:
  public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
  {
    Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    // Use the error details to determine whether to try to load another ad.
  }

  public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
  {
    Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    // Use the error details to determine whether to try to load another ad.
  }

  public void OnUnityAdsShowStart(string adUnitId)
  {
    Debug.Log("Ad Started: " + adUnitId);
  }
  public void OnUnityAdsShowClick(string adUnitId)
  {
    Debug.Log("Ad Clicked: " + adUnitId);
  }

  void OnDestroy()
  {
    // Clean up the button listeners:
    _showAdButton.onClick.RemoveAllListeners();
  }
}