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
        if (!firstAdShown)
        {
            Debug.Log("Showing first time interstitial ad automatically!");
            adsInterstitial.ShowAd();
            firstAdShown = true;

        }
        else
        {
            Debug.Log("Next interstitial ad is ready for manual show!");
        }
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

        Button interstitialButton =
            GameObject.FindGameObjectWithTag("InterstitialAdButton").GetComponent<Button>();

        if (adsInterstitial != null && interstitialButton != null)
        {
            adsInterstitial.SetButton(interstitialButton);
        }


        if (adsRewarded == null)
            adsRewarded = FindFirstObjectByType<AdsRewarded>();
        if (bannerAd == null)
            bannerAd = FindFirstObjectByType<AdsBanner>();
        Button rewardedAdButton =
            GameObject.FindGameObjectWithTag("RewardedAdButton").GetComponent<Button>();

        if (adsRewarded != null && rewardedAdButton != null)
            adsRewarded.SetButton(rewardedAdButton);

        Button bannerButton = GameObject.FindGameObjectWithTag("BannerAdButton").GetComponent<Button>();
        if(bannerAd != null && bannerButton != null)
        {
            bannerAd.SetButton(bannerButton);
        }
        if (!firstSceneLoad)
        {
            firstSceneLoad = true;
            Debug.Log("First time scene loaded!");
            return;
        }

        Debug.Log("Scene loaded!");
        HandleAdsInitialized();

    }
}