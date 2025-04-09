//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GoogleMobileAds.Api;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class AdMobInterstitial : MonoBehaviour {

//	static AdMobInterstitial _instance = null;

//    // iOS テストインタースティシャルID ca-app-pub-3940256099942544/4411468910
//    // iOS 正規インタースティシャルID ca-app-pub-9057429779623786/9454088891

//    private string iosInterstitialId = "ca-app-pub-9057429779623786/9454088891";
// 	private InterstitialAd interstitial;
    
//	static AdMobInterstitial instance
//	{
//		get { return _instance ?? (_instance = FindObjectOfType<AdMobInterstitial>()); }
//	}

//	private void Awake()
//    {
//        // ※オブジェクトが重複していたらここで破棄される

//        // 自身がインスタンスでなければ自滅
//        if (this != instance)
//        {
//            Destroy(this.gameObject);
//            return;
//        }

//        // 以降破棄しない
//        DontDestroyOnLoad(this.gameObject);
//    }

//	void OnDestroy()
//    {

//        // ※破棄時に、登録した実体の解除を行なっている

//        // 自身がインスタンスなら登録を解除
//        if (this == instance) _instance = null;

//    }

//	//instance化してるのでリスタートしても"void Start"は呼ばれない！
//    void Start()
//    {
//        // Initialize the Google Mobile Ads SDK.
//        MobileAds.Initialize(initStatus => { });
//        //1番初めの1度だけのインタースティシャルリクエスト
//        RequestInterstitial();
//    }

//	public void HandleOnAdLoaded(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("インタースティシャル広告読み込み成功 HandleAdLoaded event received");
//    }

//    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        MonoBehaviour.print("インタースティシャル広告読み込み失敗 HandleFailedToReceiveAd event received with message: " + args.Message);
//    }

//    public void HandleOnAdOpened(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("インタースティシャル広告表示 HandleAdOpened event received");
//    }

//    public void HandleOnAdClosed(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("表示されたインタースティシャル広告閉じて削除 HandleAdClosed event received");
//        interstitial.Destroy();

//        //現在のシーン名を取得
//        string sceneName = SceneManager.GetActiveScene().name;
//        //現在のシーンをリロード
//        SceneManager.LoadScene(sceneName);

//        //2度目以降のインタースティシャルリクエスト
//        RequestInterstitial();
//    }

//    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleAdLeavingApplication event received");
//    }

//    // インタースティシャル広告リクエスト
//    public void RequestInterstitial()
//    {
//#if UNITY_ANDROID
//        string adUnitId = Android_Interstitial;
//#elif UNITY_IPHONE
//        string adUnitId = iosInterstitialId;
//#else
//        string adUnitId = "unexpected_platform";
//#endif

//        // インタースティシャル広告の初期化
//        interstitial = new InterstitialAd(adUnitId);

//        // インタースティシャル広告リクエストが正常に読み込まれたときに呼び出されます。
//        interstitial.OnAdLoaded += HandleOnAdLoaded;
//        // インタースティシャル広告リクエストの読み込みに失敗したときに呼び出されます。
//        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
//        // インタースティシャル広告が表示されたときに呼び出されます
//        interstitial.OnAdOpening += HandleOnAdOpened;
//        // インタースティシャル広告が閉じられたときに呼び出されます。
//        interstitial.OnAdClosed += HandleOnAdClosed;
//        // インタースティシャル広告のクリックによりユーザーがアプリケーションを終了したときに呼び出されます。
//        interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

//        // 空の広告リクエストを作成します
//        AdRequest request = new AdRequest.Builder().Build();
//        // インタースティシャルにリクエストを読み込みます
//        interstitial.LoadAd(request);
//    }

//    // インタースティシャル広告表示
//    public void InterstitialShow()
//    {
//        if (interstitial.IsLoaded())
//        {
//            interstitial.Show();
//        }
//    }

//    // インタースティシャル広告閉じる
//    public void InterstitialClose()
//    {
//        if (interstitial.IsLoaded())
//        {
//            interstitial.Destroy();
//            Debug.Log("ロード済みのインタースティシャル広告を削除");
//        }
//        else
//        {
//            Debug.Log("インタースティシャル広告はロードもされてないので何もしない");
//        }
//    }

//	void Update () {
		
//	}
//}
