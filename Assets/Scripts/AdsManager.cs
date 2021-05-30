using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    string gameID = "4147431";
    bool testMode = true;

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, testMode);
    }

    public void ShowRewardedVideo()
    {
        if(Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo");
        }
        else
        {
            Debug.Log("Rewarded video not ready at the moment");
        }
    }

    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            GameManager.Instance.Player.AddDiamond(100);
            UIManager.Instance.OpenShop(GameManager.Instance.Player.diamonds);
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.LogError("You have skipped the video, no bonus diamonds for you");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogError("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady(string surfacingId)
    {
        // If the ready Ad Unit or legacy Placement is rewarded, show the ad:
        if (surfacingId == "rewardedVideo")
        {
            // Optional actions to take when theAd Unit or legacy Placement becomes ready (for example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

    /*
    public void ShowRewardedAd()
    {
        Debug.Log("Rewarded Ad");

        if(Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions
            {
                resultCallback = HandleShowResult
            };
            Advertisement.Show("rewardedVideo", options);
        }
    }
    

    void HandleShowResult(ShowResult result)
    {
        switch(result)
        {
            case ShowResult.Finished:
                GameManager.Instance.Player.AddDiamond(100);
                UIManager.Instance.OpenShop(GameManager.Instance.Player.diamonds);
                break;
            case ShowResult.Skipped:
                Debug.Log("You Skipped the Ad. No Reward");
                break;
            case ShowResult.Failed:
                Debug.Log("The video failed");
                break;
        }
    }
    */
}
