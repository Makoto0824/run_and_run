//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GoogleMobileAds.Api;
//using System;
//using UnityEngine.UI;

//public class AdMobRectangle : MonoBehaviour
//{
//    // 広告非表示購入用ボタン
//    public Button disableAdsButton;
//    // リストアボタン
//    public Button restoreButton;
//    // 課金有無確認用 0=未課金 1=課金済
//    static int AdsFalse;


//    // iOS テストレクタングルID ca-app-pub-3940256099942544/2934735716
//    // iOS 正規レクタングルID ca-app-pub-9057429779623786/1410688161

// //   private string _iosRectangleId = "ca-app-pub-9057429779623786/1410688161";
//	//public BannerView bannerView;

//    void Start()
//    {
//        // Initialize the Google Mobile Ads SDK.
//        //MobileAds.Initialize(initStatus => { });
//        //Debug.Log("Mobile Ads SDK を初期化する");

//        //Debug.Log("AdMobRectangle AdsFalse" + AdsFalse);

//        // 課金情報を取得
//        //AdsFalse = PlayerPrefs.GetInt("AdsFalse", 0);

//        //if (AdsFalse == 1)
//        //{
//        //    disableAdsButton.interactable = false;
//        //    restoreButton.interactable = false;
//        //}
//        //else
//        //{
//        //    disableAdsButton.interactable = true;
//        //    restoreButton.interactable = true;
//        //}
//    }

//    //public static int GetAdsFalse()
//    //{
//    //    return AdsFalse;
//    //}


// //   public void HandleOnAdLoaded(object sender, EventArgs args)
//	//{
//	//	MonoBehaviour.print("レクタングル読み込み成功 HandleAdLoaded event received");
//	//}

////	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
////	{
////		MonoBehaviour.print("レクタングル読み込み失敗 HandleFailedToReceiveAd event received with message: " + args.Message);
////	}

////	public void HandleOnAdOpened(object sender, EventArgs args)
////	{
////		MonoBehaviour.print("レクタングル表示 HandleAdOpened event received");
////	}

////	// 広告のクリック後にユーザーがアプリから戻ったとき
////	public void HandleOnAdClosed(object sender, EventArgs args)
////	{
////		MonoBehaviour.print("レクタングルクリックして戻ってきた HandleAdClosed event received");
////	}

////	public void HandleOnAdLeavingApplication(object sender, EventArgs args)
////	{
////		MonoBehaviour.print("HandleAdLeavingApplication event received");
////	}

////    // レクタングル広告リクエスト
////    public void RequestBanner()
////	{
////#if UNITY_ANDROID
////        string adUnitId = _androidRectangleId;
////#elif UNITY_IPHONE
////		string adUnitId = _iosRectangleId;
////#else
////        string adUnitId = "unexpected_platform";
////#endif

////        Debug.Log(_iosRectangleId);

////        bannerView = new BannerView(adUnitId, AdSize.MediumRectangle, AdPosition.Center);

////        // レクタングル広告リクエストが正常に読み込まれたときに呼び出されます
////        bannerView.OnAdLoaded += HandleOnAdLoaded;
////        // レクタングル広告リクエストの読み込みに失敗したときに呼び出されます
////        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
////        // レクタングル広告がクリックされたときに呼び出されます
////        bannerView.OnAdOpening += HandleOnAdOpened;
////        // レクタングル広告のクリック後にユーザーがアプリから戻ったときに呼び出されます。
////        bannerView.OnAdClosed += HandleOnAdClosed;
////        // レクタングル広告のクリックによりユーザーがアプリケーションを終了したときに呼び出されます
////        bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;

////        // 空の広告リクエストを作成します。Create an empty ad request.
////        AdRequest request = new AdRequest.Builder().Build();
////        // リクエストとともにバナーをロードします。Load the banner with the request.
////        bannerView.LoadAd(request);

////        // バナー広告が表示されてしまうので非表示に
////        bannerView.Hide();
////    }

////    // レクタングル広告表示
////    public void BannerShow()
////    {

////        bannerView.Show();

//        //if (AdsFalse == 0)
//        //{
//        //    bannerView.Show();
//        //}
//        //else
//        //{
//        //    BannerDestroy();
//        //    disableAdsButton.gameObject.SetActive(false);
//        //}
//    }

//	// レクタングル広告削除
//	//public void BannerDestroy()
//	//{
//	//	bannerView.Destroy();
//	//	Debug.Log("レクタングル削除");
//	//}


//    // 課金処理
//    //public void PurchaseOK()
//    //{
//    //    //課金情報を記録
//    //    PlayerPrefs.SetInt("AdsFalse", 1);
//    //    PlayerPrefs.Save();

//    //    // 課金情報を取得
//    //    AdsFalse = PlayerPrefs.GetInt("AdsFalse", 0);

         
//    //    // Admod非表示
//    //    // bannerView.Hide();

//    //    // 購入ボタン非表示
//    //    if (disableAdsButton.interactable == true)
//    //    {
//    //        disableAdsButton.interactable = false;
//    //    }

//    //    // リストアボタン非表示
//    //    if (restoreButton.interactable == true)
//    //    {
//    //        restoreButton.interactable = false;
//    //    }
            
//    //    Debug.Log("PurchaseOK AdsFalse" + AdsFalse);

//    //}

//    //public void DeletePurchase()
//    //{
//    //    PlayerPrefs.DeleteAll();
//    //}




//}
