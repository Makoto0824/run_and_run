using UnityEngine;
using System.Collections.Generic;

//キャラクター詳細を削除
[System.Serializable]
public class shopCharacterData
{
    public string characterName;
    public Sprite characterSprite;
    public int characterPrice;
}

//ステージ詳細
[System.Serializable]
public class shopThemeData
{
    //名前
    public string themeName;
    //選択メニュー表示画像
    public Sprite shopThemeSprite;
    //値段
    public int themePrice;
}

//ステージデフォルト値段
//Sci Fi 0
//Forest 100
//Desert 200
//Graveyard 300

[System.Serializable]
public class themeData
{
    public Texture backgroundTexture;
    public Sprite topTile, bottomTile;
}

public class managerVars : ScriptableObject
{
    [SerializeField]
    public List<shopCharacterData> characters = new List<shopCharacterData>();
    [SerializeField]
    public List<shopThemeData> themes = new List<shopThemeData>();
    [SerializeField]
    public List<themeData> themeData = new List<themeData>();

    [SerializeField]
    [Header("UI Options")]
    public Sprite soundOnButton;
    [SerializeField]
    public Sprite soundOffButton, playButton, homeButton, starImg, retryBtnImg, pauseImg;

    [SerializeField]
    public Color32 gameOverScoreTextColor, gameOverBestScoreTextColor, inGameScoreTextColor, starTextColor;

    //[SerializeField]
    //public Font mainFont, secondFont;

    [SerializeField]
    [Header("Sound Options")]
    public AudioClip buttonSound;
    [SerializeField]
    public AudioClip starSound, backgroundMusic, deathSound;
}
