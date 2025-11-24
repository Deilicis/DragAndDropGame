using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    public AdsInitializer adsInitializer;
    public AdsInterstitial adsInterstitial;
    [SerializeField] bool turnOffInterstitialAd = false;
    private bool firstAdShown = false;

    public AdsRewarded adsRewarded;
    [SerializeField] bool turnOffRewardedAds = false;

    public AdsBanner bannerAd;
    [SerializeField] bool turnOffBannerAds = false;

    public static AdManager Instance { get; private set; }


    private void Awake()
    {
        if (adsInitializer == null)
            adsInitializer = FindFirstObjectByType<AdsInitializer>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);


        adsInitializer.OnAdsInitialized += HandleAdsInitialized;

        if (adsInterstitial == null)
            adsInterstitial = FindFirstObjectByType<AdsInterstitial>();

        if (adsInterstitial != null)
            adsInterstitial.LoadAd();
    }


    private void HandleAdsInitialized()
    {
        if (!turnOffInterstitialAd)
        {
            adsInterstitial.OnInterstitialAdReady += HandleInterstitialReady;
            adsInterstitial.LoadAd();
        }

        if (!turnOffRewardedAds)
        {
            adsRewarded.LoadAd();
        }

        if(!turnOffBannerAds)
        {
            bannerAd.LoadBanner();
        }
    }

    private void HandleInterstitialReady()
    {
    
            Debug.Log("Next interstitial ad is ready for manual show!");
   
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private bool firstSceneLoad = false;
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (adsInterstitial == null)
            adsInterstitial = FindFirstObjectByType<AdsInterstitial>();


        if (adsRewarded == null)
            adsRewarded = FindFirstObjectByType<AdsRewarded>();
        if (bannerAd == null)
            bannerAd = FindFirstObjectByType<AdsBanner>();
        GameObject btnObj = GameObject.FindGameObjectWithTag("RewardedAdButton");
        if (btnObj != null)
        {
            Button rewardedAdButton = btnObj.GetComponent<Button>();
            adsRewarded.SetButton(rewardedAdButton);
        }
        else
        {
            Debug.LogWarning("Rewarded ad button not found in scene!");
        }
        if (!firstSceneLoad)
        {
            firstSceneLoad = true;
            Debug.Log("First time scene loaded!");
            return;
        }

        Debug.Log("Scene loaded!");
        if (scene.name != "TitleScene")
        {
            if (adsInterstitial.isReady)
                adsInterstitial.ShowAd();
        }


    }
}