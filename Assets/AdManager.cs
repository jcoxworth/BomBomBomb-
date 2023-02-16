using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    private string gameID = "4378566";
#elif UNITY_ANDROID
    private string gameID = "4378567";
#elif UNITY_EDITOR
    private string gameID = "4378567";
#endif

    private string placementID = "bannerAd";

    public static AdManager instance; 
    void Awake()
    {
        instance = this;
    }

    //did an error occur?
    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show("bannerAd");
    }

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, true);
    }
}
