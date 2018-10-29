using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsTest : MonoBehaviour {

	public void OnClick()
    {
        ShowRewardedAd();
    }

    private void ShowRewardedAd()
    {
        if (!Advertisement.IsReady())
        {
            Debug.Log("Not yet.");
            return;
        }

        var options = new ShowOptions { resultCallback = OnResult, };
        Advertisement.Show(options);
    }

    private void OnResult(ShowResult result)
    {
        switch(result)
        {
            case ShowResult.Finished:
                Debug.Log("Finished.");
                break;
            case ShowResult.Skipped:
                Debug.Log("Skipped.");
                break;
            case ShowResult.Failed:
                Debug.Log("Failed.");
                break;
        }
    }
}
