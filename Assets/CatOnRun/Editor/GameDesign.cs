//using UnityEngine;
//using UnityEditor;
//using System;
//using UnityEngine.UI;

//using GL = UnityEngine.GUILayout;
//using EGL = UnityEditor.EditorGUILayout;

//[System.Serializable]
//public class GameDesign : EditorWindow {
//	// Editor
//	Texture2D book;
//	Texture2D GDbanner;
//	bool[] toggles;
//	string[] buttons;
//	private static Texture2D bgColor;
//	public GUISkin editorSkin;
//	Vector2 scrollPosition = new Vector2(0,0);
//	string[] bannerPositionTexts = new string[] {"Bottom", "Bottom Left", "Bottom Right", "Top", "Top Left", "Top Right"};
//	public managerVars vars;
//	public GuiManager guiManager;
//	public string listType;
//    public GameObject gameUI;
//    // Game	
//    public shopCharacterData[] characters;
//    public shopThemeData[] themes;
//    [SerializeField]
//	public string[] deathStrings;

//	[MenuItem("Editor/GAME DESIGN")]
//	static void Initialize(){
//		GameDesign window = EditorWindow.GetWindow<GameDesign>(true, "GAME DESIGN");
//		window.maxSize = new Vector2 (500f, 615f);
//		window.minSize = window.maxSize;
//	}

//	void OnEnable(){
//		toggles = new bool[]{false, false, false, false, false, false};
//        buttons = new string[] { "Open", "Open", "Open", "Open", "Open", "Open" };
//		vars = (managerVars) AssetDatabase.LoadAssetAtPath("Assets/CatOnRun/Resources/managerVarsContainer.asset", typeof(managerVars));
//		book = Resources.Load("question", typeof(Texture2D)) as Texture2D;
//		GDbanner = Resources.Load("GDbanner", typeof(Texture2D)) as Texture2D;

//        gameUI = GameObject.FindGameObjectWithTag("MainCamera");
//        guiManager = gameUI.GetComponent<GuiManager>();
//        liveUpdate();

//        //try{
//  //          gameUI = GameObject.FindGameObjectWithTag("MainCamera");
//		//	guiManager = gameUI.GetComponent<GuiManager> ();
//		//}catch(Exception e){}

//		//try{
//		//	liveUpdate ();
//		//}catch(Exception e){}

//	}

//	void OnGUI(){
//		// Settings
//		bgColor = (Texture2D)Resources.Load ("editorBgColor");
//		GUI.DrawTexture(new Rect(0, 0, maxSize.x, maxSize.y), bgColor, ScaleMode.StretchToFill);
//		GUI.skin = editorSkin;
//		GL.Label (GDbanner);
//		scrollPosition = GL.BeginScrollView(scrollPosition);

//        #region Shop Options
//        // Start Block
//        blockHeader("Shop Character Options", "Shop items options.", 0);
//        if (toggles[0])
//        {
//            buttons[0] = "Close";
//            BVS("GroupBox");
//            // Content Start
//            shopCharaterCountController();
//            updateShopCharacters();
//            // Content End
//            EditorUtility.SetDirty(vars);
//            EV();
//        }
//        else buttons[0] = "Open";
//        EV();
//        // End Block
//        #endregion

//        #region Shop Theme Options
//        // Start Block
//        blockHeader("Shop Themes Options", "Shop Theme options.", 1);
//        if (toggles[1])
//        {
//            buttons[1] = "Close";
//            BVS("GroupBox");
//            // Content Start
//            shopThemesCountController();
//            updateShopThemes();
//            // Content End
//            EditorUtility.SetDirty(vars);
//            EV();
//        }
//        else buttons[1] = "Open";
//        EV();
//        // End Block
//        #endregion

//        #region UI Options
//        // Start Block
//        blockHeader("UI Options", "All UI options.", 2);
//		if(toggles[2]){
//			buttons[2] = "Close";
//			BVS("GroupBox");
//			// Content Start
//			GL.Label ("UI Images", "centerBoldLabel");
//			GL.Space (10);
//			BV ();

//			BH ();
//            vars.facebookLikeImg = EGL.ObjectField("Facebook Image", vars.facebookLikeImg, typeof(Sprite), false) as Sprite;
//            vars.twitterImg = EGL.ObjectField("Twitter Image", vars.twitterImg, typeof(Sprite), false) as Sprite;
//            EH ();
//			BH ();
//			vars.leaderboardButton = EGL.ObjectField ("Leaderboard Button", vars.leaderboardButton, typeof(Sprite), false) as Sprite;
//            vars.playButton = EGL.ObjectField("Play Button", vars.playButton, typeof(Sprite), false) as Sprite;
//            EH ();
//			BH ();
//			vars.homeButton = EGL.ObjectField ("Home Button", vars.homeButton, typeof(Sprite), false) as Sprite;
//            vars.starImg = EGL.ObjectField("Star Image", vars.starImg, typeof(Sprite), false) as Sprite;
//            EH ();

//			BH ();
//			vars.soundOnButton = EGL.ObjectField ("Sound On Button", vars.soundOnButton, typeof(Sprite), false) as Sprite;
//			vars.soundOffButton = EGL.ObjectField ("Sound Off Button", vars.soundOffButton, typeof(Sprite), false) as Sprite;
//			EH ();

//            BH();
//            vars.retryBtnImg = EGL.ObjectField("Retry Button", vars.retryBtnImg, typeof(Sprite), false) as Sprite;
//            vars.adsBtnImg = EGL.ObjectField("Ads Btn", vars.adsBtnImg, typeof(Sprite), false) as Sprite;
//            EH();

//            BH();
//            vars.shareImage = EGL.ObjectField("Share Image", vars.shareImage, typeof(Sprite), false) as Sprite;
//            vars.noAdsImage = EGL.ObjectField("NoAds Image", vars.noAdsImage, typeof(Sprite), false) as Sprite;
//            EH();

//            BH();
//            vars.rateImg = EGL.ObjectField("Rate Image", vars.rateImg, typeof(Sprite), false) as Sprite;
//            vars.pauseImg = EGL.ObjectField("Pause Image", vars.pauseImg, typeof(Sprite), false) as Sprite;
//            EH();

//            BH();
//            vars.moreGamesImg = EGL.ObjectField("MoreGames Image", vars.moreGamesImg, typeof(Sprite), false) as Sprite;
//            EH();

//            EV ();
//			separator ();
//			GL.Label ("UI Texts", "centerBoldLabel");
//			GL.Space (10);
//			BVS("GroupBox");
//			GL.Label ("Game Over Score Text :");
//			vars.gameOverScoreTextColor = EGL.ColorField ("Color", vars.gameOverScoreTextColor);
//            GL.Label("Game Over Best Score Text :");
//            vars.gameOverBestScoreTextColor = EGL.ColorField("Color", vars.gameOverBestScoreTextColor);
//            EV ();
//            GL.Space (5);
//			BVS("GroupBox");
//			GL.Label ("In Game Score Text :");
//			vars.inGameScoreTextColor = EGL.ColorField ("Color", vars.inGameScoreTextColor);
//            GL.Label("Star Count Text :");
//            vars.starTextColor = EGL.ColorField("Color", vars.starTextColor);
//            EV ();
//			separator ();
//			GL.Label ("UI Fonts", "centerBoldLabel");
//			GL.Space (10);
//			vars.mainFont = EGL.ObjectField ("Main Font", vars.mainFont, typeof(Font), false) as Font;
//			vars.secondFont = EGL.ObjectField ("Second Font", vars.secondFont, typeof(Font), false) as Font;
//            // Content End
//            EditorUtility.SetDirty (vars);
//			EV();
//		}else buttons[2] = "Open";
//		EV();
//        // End Block
//        #endregion

//        #region Game Theme Option
//        // Start Block
//        blockHeader("Theme Options", "Theme options.", 3);
//        if (toggles[3])
//        {
//            buttons[3] = "Close";
//            BVS("GroupBox");
//            // Content Start
//            themesCountController();
//            updateThemes();
//            // Content End
//            EditorUtility.SetDirty(vars);
//            EV();
//        }
//        else buttons[3] = "Open";
//        EV();
//        // End Block
//        #endregion

//        #region Sound Options
//        // Start Block
//        blockHeader("Sound Options", "Sound & Music options.", 4);
//		if (toggles [4]){
//			buttons [4] = "Close";
//			BVS ("GroupBox");
//			// Content Start
//			vars.buttonSound = EGL.ObjectField ("Button Sound",vars.buttonSound,typeof(AudioClip),false) as AudioClip;
//			vars.starSound = EGL.ObjectField ("Star Sound",vars.starSound, typeof(AudioClip),false) as AudioClip;
//			vars.backgroundMusic = EGL.ObjectField ("Background Music",vars.backgroundMusic, typeof(AudioClip),false) as AudioClip;
//			//vars.jumpSound = EGL.ObjectField ("Jump Sound",vars.jumpSound, typeof(AudioClip),false) as AudioClip;
//            //vars.slideSound = EGL.ObjectField("Slide Sound", vars.slideSound, typeof(AudioClip), false) as AudioClip;
//            vars.deathSound = EGL.ObjectField("Death Sound", vars.deathSound, typeof(AudioClip), false) as AudioClip;
//            // Content End
//            EditorUtility.SetDirty (vars);
//			EV ();
//		}else buttons[4] = "Open";
//		EV();
//        // End Block
//        #endregion   

//        #region Other Options
//        // Start Block
//        blockHeader("Other Options", "AdMob, Google Play Services and etc. options.", 5);
//		if(toggles [5]){
//			buttons[5] = "Close";
//			GL.BeginVertical("GroupBox");
//            //Admob
//            if (GUILayout.Button("Download Admob SDK"))
//            {
//                Application.OpenURL("https://github.com/googleads/googleads-mobile-unity/releases");
//            }
//            GL.Label("AdMob Options", EditorStyles.boldLabel);
//            vars.admobActive = EGL.Toggle("Use Admob Ads", vars.admobActive, "Toggle");
//            if (vars.admobActive)
//            {
//                AssetDefineManager.AddCompileDefine("AdmobDef", 
//                    new BuildTargetGroup[] { BuildTargetGroup.Android, BuildTargetGroup.iOS });

//                GL.Space(5f);
//                //Admob App ID
//                vars.admobAppID = EGL.TextField("AdMob App ID", vars.admobAppID);
//                separator();
//                //Banner
//                vars.adMobBannerID = EGL.TextField("AdMob Banner ID", vars.adMobBannerID);
//                GL.BeginHorizontal();
//                GL.Label("Banner Position");
//                vars.bannerAdPoisiton = GL.SelectionGrid(vars.bannerAdPoisiton, bannerPositionTexts, 3, "Radio");
//                GL.EndHorizontal();
//                separator();

//                //Interstitial
//                vars.adMobInterstitialID = EGL.TextField("AdMob Interstitial ID", vars.adMobInterstitialID);
//                GL.BeginHorizontal();
//                GL.Label("Show Interstitial After Death Times");
//                vars.showInterstitialAfter = EGL.IntSlider(vars.showInterstitialAfter, 1, 25);
//                GL.EndHorizontal();
//            }
//            else if (!vars.admobActive)
//            {
//                AssetDefineManager.RemoveCompileDefine("AdmobDef",
//                    new BuildTargetGroup[] { BuildTargetGroup.Android, BuildTargetGroup.iOS });
//            }

//            separator();

//            //Google Play Service
//            if (GUILayout.Button("Download Google Play SDK"))
//            {
//                Application.OpenURL("https://github.com/playgameservices/play-games-plugin-for-unity");
//            }
//            GL.Label("Google Play Or Game Center", EditorStyles.boldLabel);
//            vars.googlePlayActive = EGL.Toggle("Use Leaderboard", vars.googlePlayActive, "Toggle");
//            if (vars.googlePlayActive)
//            {
//#if UNITY_ANDROID
//                AssetDefineManager.AddCompileDefine("GooglePlayDef",
//                    new BuildTargetGroup[] { BuildTargetGroup.Android });
//#endif

//                vars.leaderBoardID = EGL.TextField("Leaderboard ID", vars.leaderBoardID);
//            }
//            else if (!vars.googlePlayActive)
//            {
//#if UNITY_ANDROID
//                AssetDefineManager.RemoveCompileDefine("GooglePlayDef",
//                    new BuildTargetGroup[] { BuildTargetGroup.Android });
//#endif
//            }

//            separator();

//            GL.Label("Other Options", EditorStyles.boldLabel);
//            //facebook page
//            GL.BeginHorizontal();
//            GL.Label("Facebook Page", GL.Width(100f));
//            vars.facebookPage = EGL.TextArea(vars.facebookPage, GL.Height(25f));
//            GL.EndHorizontal();
//            GL.Space(15f);
//            //twitter
//            GL.BeginHorizontal();
//            GL.Label("Twitter Page", GL.Width(100f));
//            vars.twitterPage = EGL.TextArea(vars.twitterPage, GL.Height(25f));
//            GL.EndHorizontal();
//            GL.Space(15f);
//            //Rate Url
//            GL.BeginHorizontal();
//            GL.Label("Rate Button Url", GL.Width(100f));
//            vars.rateButtonUrl = EGL.TextArea(vars.rateButtonUrl, GL.Height(25f));
//            GL.EndHorizontal();
//            GL.Space(15f);
//            //MoreGames Url
//            GL.BeginHorizontal();
//            GL.Label("More Games Btn Url", GL.Width(100f));
//            vars.moreGamesUrl = EGL.TextArea(vars.moreGamesUrl, GL.Height(25f));
//            GL.EndHorizontal();
//            GL.Space(15f);
//            separator();
//			//
//			EditorUtility.SetDirty (vars);
//			GL.EndVertical();
//		}else buttons[5] = "Open";
//		GL.EndVertical();
//        // End Block
//#endregion

//        GL.EndScrollView();
//		EditorUtility.SetDirty (vars);

//        liveUpdate();

//  //      try{
//		//	liveUpdate ();
//		//}catch(Exception e){}
//	}

//	void liveUpdate(){
//#region mainMenu
//		guiManager.mainMenu.facebookBtn.image.sprite = vars.facebookLikeImg;
//        guiManager.mainMenu.twitterBtn.image.sprite = vars.twitterImg;
//        guiManager.mainMenu.playBtn.image.sprite = vars.playButton;
//        guiManager.mainMenu.leaderboardBtn.image.sprite = vars.leaderboardButton;
//		guiManager.mainMenu.noAdsBtn.image.sprite = vars.noAdsImage;
//		guiManager.mainMenu.soundBtn.image.sprite = vars.soundOnButton;
//        guiManager.mainMenu.rateBtn.image.sprite = vars.rateImg;
//        guiManager.mainMenu.moreGamesBtn.image.sprite = vars.moreGamesImg;
//        #endregion

//#region GameMenu
//        //guiManager.gameMenu.pauseBtn.image.sprite = vars.pauseImg;
//        guiManager.gameMenu.resumeBtn.image.sprite = vars.btnHolderImg;
//        guiManager.gameMenu.homeBtn.image.sprite = vars.homeButton;
//        guiManager.gameMenu.retryBtn.image.sprite = vars.retryBtnImg;
//        guiManager.gameMenu.starImage.sprite = vars.starImg;

//        guiManager.gameMenu.starText.color = vars.starTextColor;
//        guiManager.gameMenu.scoreText.color = vars.inGameScoreTextColor;
//#endregion

//#region GameOverMenuUI
//        guiManager.gameOverMenu.shareBtn.image.sprite = vars.shareImage;
//        guiManager.gameOverMenu.homeBtn.image.sprite = vars.homeButton;
//        guiManager.gameOverMenu.retryBtn.image.sprite = vars.retryBtnImg;
//        guiManager.gameOverMenu.starImage.sprite = vars.starImg;

//        guiManager.gameOverMenu.starText.color = vars.starTextColor;
//        guiManager.gameOverMenu.goBestText.color = vars.gameOverBestScoreTextColor;
//		guiManager.gameOverMenu.goScoreText.color = vars.inGameScoreTextColor;
//#endregion

//        guiManager.shopStartImage.sprite = vars.starImg;
//        guiManager.worldStarImage.sprite = vars.starImg;

//        guiManager.shopStarText.color = vars.starTextColor;
//        guiManager.worldStarText.color = vars.starTextColor;

//        foreach (Text texts1 in guiManager.mainFont){
//			texts1.font = vars.mainFont;
//		}
//		foreach(Text texts2 in guiManager.secondFont){
//			texts2.font = vars.secondFont;
//		}
//	}

//	void OnDestroy(){
//		EditorUtility.SetDirty (vars);
//	}

//    void shopCharaterCountController()
//    {
//        BH();
//        GL.Label("", GL.Width(250));
//        GL.Label("Characters Count : " + (vars.characters.Count));
//        if ((vars.characters.Count) != 1)
//        {
//            if (GL.Button("-"))
//            {
//                vars.characters.Remove(vars.characters[vars.characters.Count - 1]);
//                EditorUtility.SetDirty(vars);
//            }
//        }
//        if (GL.Button("+"))
//        {
//            vars.characters.Add(new shopCharacterData());
//            EditorUtility.SetDirty(vars);
//        }
//        EH();
//    }

//    void updateShopCharacters()
//    {
//        for (int i = 0; i <= (vars.characters.Count - 1); i++)
//        {
//            GL.Label("Character " + (i + 1) + " options:", EditorStyles.boldLabel);
//            BV();
//            BH();
//            vars.characters[i].characterSprite = EGL.ObjectField("Character sprite", vars.characters[i].characterSprite, typeof(Sprite), false) as Sprite;
//            EH();
//            BH();
//            vars.characters[i].characterName = EGL.TextField("Character name", vars.characters[i].characterName);
//            if (i != 0)
//            {
//                vars.characters[i].characterPrice = EGL.IntField("Character price", vars.characters[i].characterPrice);
//            }
//            EH();
//            EV();
//            separator();
//        }
//    }

//    void shopThemesCountController()
//    {
//        BH();
//        GL.Label("", GL.Width(250));
//        GL.Label("themes Count : " + (vars.themes.Count));
//        if ((vars.themes.Count) != 1)
//        {
//            if (GL.Button("-"))
//            {
//                vars.themes.Remove(vars.themes[vars.themes.Count - 1]);
//                EditorUtility.SetDirty(vars);
//            }
//        }
//        if (GL.Button("+"))
//        {
//            vars.themes.Add(new shopThemeData());
//            EditorUtility.SetDirty(vars);
//        }
//        EH();
//    }

//    void updateShopThemes()
//    {
//        for (int i = 0; i <= (vars.themes.Count - 1); i++)
//        {
//            GL.Label("Themes " + (i + 1) + " options:", EditorStyles.boldLabel);
//            BV();
//            BH();
//            vars.themes[i].shopThemeSprite = EGL.ObjectField("Theme sprite", vars.themes[i].shopThemeSprite, typeof(Sprite), false) as Sprite;
//            EH();
//            BH();
//            vars.themes[i].themeName = EGL.TextField("Theme name", vars.themes[i].themeName);
//            if (i != 0)
//            {
//                vars.themes[i].themePrice = EGL.IntField("Theme price", vars.themes[i].themePrice);
//            }
//            EH();
//            EV();
//            separator();
//        }
//    }

//    void themesCountController()
//    {
//        BH();
//        GL.Label("", GL.Width(250));
//        GL.Label("Themes Count : " + (vars.themeData.Count));
//        if ((vars.themeData.Count) != 1)
//        {
//            if (GL.Button("-"))
//            {
//                vars.themeData.Remove(vars.themeData[vars.themeData.Count - 1]);
//                EditorUtility.SetDirty(vars);
//            }
//        }
//        if (GL.Button("+"))
//        {
//            vars.themeData.Add(new themeData());
//            EditorUtility.SetDirty(vars);
//        }
//        EH();
//    }

//    void updateThemes()
//    {
//        for (int i = 0; i <= (vars.themeData.Count - 1); i++)
//        {
//            GL.Label("Theme " + (i + 1) + " options:", EditorStyles.boldLabel);
//            BV();

//            BH();
//            vars.themeData[i].backgroundTexture = EGL.ObjectField("Background Texture", vars.themeData[i].backgroundTexture, typeof(Texture), false) as Texture;
//            EH();

//            BH();
//            vars.themeData[i].topTile = EGL.ObjectField("TopTile Sprite", vars.themeData[i].topTile, typeof(Sprite), false) as Sprite;
//            vars.themeData[i].bottomTile = EGL.ObjectField("BottomTile Sprite", vars.themeData[i].bottomTile, typeof(Sprite), false) as Sprite;
//            EH();

//            EV();
//            separator();
//        }
//    }

//    void drawArray(string arrayName){
//		SerializedObject so = new SerializedObject(this);
//		SerializedProperty stringsProperty = so.FindProperty (arrayName);
//		EGL.PropertyField(stringsProperty, true);
//		so.ApplyModifiedProperties();
//	}

//	void blockHeader(string mainHeader, string secondHeader, int blockIdex){
//		BV ();
//		GL.Label (mainHeader, "TL Selection H2");
//		BH ();
//		if (GL.Button (buttons[blockIdex], GL.Height(25f) , GL.Width(50f))) toggles[blockIdex] = !toggles[blockIdex];
//		BHS ("HelpBox");
//		GL.Label (secondHeader, "infoHelpBoxText");
//		GL.Label (book , GL.Height(18f), GL.Width(20f));
//		EH ();
//		EH ();
//		GL.Space (3);
//	}

//	void separator(){
//		GL.Space(10f);
//		GL.Label("", "separator", GL.Height(1f));
//		GL.Space(10f);
//	}

//	void BH(){
//		GL.BeginHorizontal ();
//	}

//	void BHS(string style){
//		GL.BeginHorizontal (style);
//	}

//	void EH(){
//		GL.EndHorizontal ();
//	}

//	void BVS(string style){
//		GL.BeginVertical (style);
//	}

//	void BV(){
//		GL.BeginVertical ();
//	}

//	void EV(){
//		GL.EndVertical ();
//	}
//}
