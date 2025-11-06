using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    public AdsInitializer adsInitializer;
    public AdsInterstitial adsInterstitial;
    [SerializeField] bool turnOffInterstitialAd = false;
    private bool firstAdShow = false;

    //....

    public static AdManager Instance { get; private set; }

    private void Awake()
    {
        if (adsInitializer = null)
            adsInitializer = FindFirstObjectByType<AdsInitializer>();
        if(Instance != null && Instance != this)
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
    }

    private void HandleInterstitialReady()
    {
        if (!firstAdShow)
        {
            Debug.Log("Showing first time interstitial ad automatically.");
            adsInterstitial.ShowAd();
            firstAdShow = true;
        }
        else
        {
            Debug.Log("Next interstitial ad is ready for manual show.");
        }
    }

    private void onEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void onDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private bool firstSceneLoad = false;
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (adsInitializer == null)
            adsInterstitial = FindFirstObjectByType<AdsInterstitial>();

        Button interstitialButton = GameObject.FindGameObjectWithTag("InterstitialAdButton").GetComponent<Button>();
    
        if(adsInterstitial != null && interstitialButton != null)
        {
            adsInterstitial.SetButton(interstitialButton);
        }

        if (!firstSceneLoad)
        {
            firstSceneLoad = true;
            Debug.Log("First time scene loaded.");
            return;
        }

        Debug.Log("Scene loaded.");
        HandleAdsInitialized();
    }


}
