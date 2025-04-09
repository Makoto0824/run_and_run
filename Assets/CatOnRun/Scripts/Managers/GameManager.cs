using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private GameData data;

    #region Variables not saved on device
    [HideInInspector]
    public bool gameOver = false, gameRestart = false, tileSetChanged = false;
    [HideInInspector]
    public int gamesPlayed, lastScore, currentPoints;
    #endregion

    #region Variables saved on device
    //variables which are saved on the device
    public bool isGameStartedFirstTime;
    [HideInInspector]
    public bool canShowAds;
    [HideInInspector]
    public bool isMusicOn;
    [HideInInspector]
    public bool rateBtnClicked;
    public int bestScore;
    //[HideInInspector]
    public bool[] skinUnlocked, themeUnlocked;
    //[HideInInspector]
    public int selectedSkin, selectedTheme;
    //[HideInInspector]
    public int points; //to buy new skins and theme
    #endregion

    public managerVars vars;

    void OnEnable()
    {
        vars = Resources.Load<managerVars>("managerVarsContainer");
    }

    void Awake()
    {
        MakeSingleton();
        InitializeGameVariables();
    }

    void MakeSingleton()
    {
        //this state that if the gameobject to which this script is attached , if it is present in scene then destroy the new one , and if its not present
        //then create new 
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void InitializeGameVariables()
    {
        Load();
        if (data != null)
        {
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
        }
        else
        {
            isGameStartedFirstTime = true;
        }

        if (isGameStartedFirstTime)
        {
            isGameStartedFirstTime = false;
            isMusicOn = true;
            canShowAds = true;
            bestScore = 0;
            points = 0;

            skinUnlocked = new bool[vars.characters.Count];
            skinUnlocked[0] = true;
            for (int i = 1; i < skinUnlocked.Length; i++)
            {
                skinUnlocked[i] = false;
            }
            selectedSkin = 0;

            themeUnlocked = new bool[vars.themes.Count];
            themeUnlocked[0] = true;
            for (int i = 1; i < themeUnlocked.Length; i++)
            {
                themeUnlocked[i] = false;
            }
            selectedTheme = 0;

            rateBtnClicked = false;
                

            data = new GameData();

            data.setIsGameStartedFirstTime(isGameStartedFirstTime);
            data.setMusicOn(isMusicOn);
            data.setCanShowAds(canShowAds);
            data.setRateClick(rateBtnClicked);
            data.setBestScore(bestScore);
            data.setSkinUnlocked(skinUnlocked);
            data.setPoints(points);
            data.setSelectedSkin(selectedSkin);
            data.setThemeUnlocked(themeUnlocked);
            data.setSelectedTheme(selectedTheme);
            Save();

            Load();
        }
        else
        {
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
            isMusicOn = data.getMusicOn();
            canShowAds = data.getCanShowAds();
            rateBtnClicked = data.getRateClick();
            bestScore = data.getBestScore();
            points = data.getPoints();
            selectedSkin = data.getSelectedSkin();
            skinUnlocked = data.getSkinUnlocked();
            selectedTheme = data.getSelectedTheme();
            themeUnlocked = data.getThemeUnlocked();
        }
    }


    //                              .........this function take care of all saving data like score , current player , current weapon , etc
    public void Save()
    {
        FileStream file = null;
        //whicle working with input and output we use try and catch
        //入出力を扱う場合、try と catch を使用します。
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Create(Application.persistentDataPath + "/GameData.dat");

            if (data != null)
            {
                data.setIsGameStartedFirstTime(isGameStartedFirstTime);
                data.setMusicOn(isMusicOn);
                data.setCanShowAds(canShowAds);
                data.setRateClick(rateBtnClicked);
                data.setBestScore(bestScore);
                data.setSkinUnlocked(skinUnlocked);
                data.setPoints(points);
                data.setSelectedSkin(selectedSkin);
                data.setThemeUnlocked(themeUnlocked);
                data.setSelectedTheme(selectedTheme);

                bf.Serialize(file, data);
            }
        }
        catch (Exception e)
        {
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }

        Debug.Log("SAVE!");

    }
    //                            .............here we get data from save
    public void Load()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);
            data = (GameData)bf.Deserialize(file);

        }
        catch (Exception e)
        {
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }

        Debug.Log("LOAD!");

    }

    //for resetting the gameManager

    public void ResetGameManager()
    {
        isGameStartedFirstTime = false;
        isMusicOn = true;
        canShowAds = true;
        bestScore  = 0;
        points = 0;

        skinUnlocked = new bool[vars.characters.Count];
        skinUnlocked[0] = true;
        for (int i = 1; i < skinUnlocked.Length; i++)
        {
            skinUnlocked[i] = false;
        }
        selectedSkin = 0;

        themeUnlocked = new bool[vars.themes.Count];
        themeUnlocked[0] = true;
        for (int i = 1; i < themeUnlocked.Length; i++)
        {
            themeUnlocked[i] = false;
        }
        selectedTheme = 0;

        rateBtnClicked = false;


        data = new GameData();

        data.setIsGameStartedFirstTime(isGameStartedFirstTime);
        data.setMusicOn(isMusicOn);
        data.setCanShowAds(canShowAds);
        data.setRateClick(rateBtnClicked);
        data.setBestScore(bestScore);
        data.setSkinUnlocked(skinUnlocked);
        data.setPoints(points);
        data.setSelectedSkin(selectedSkin);
        data.setThemeUnlocked(themeUnlocked);
        data.setSelectedTheme(selectedTheme);

        Save();
        Load();

        Debug.Log("GameManager Reset");
    }


}

[Serializable]
class GameData
{
    private bool isGameStartedFirstTime;
    private bool isMusicOn;
    private bool canShowAds;
    private bool rateBtnClicked;
    private int bestScore;
    private bool[] skinUnlocked, themeUnlocked;
    private int selectedSkin, selectedTheme;
    private int points; //to buy new skins

    public void setCanShowAds(bool canShowAds)
    {
        this.canShowAds = canShowAds;
    }

    public bool getCanShowAds()
    {
        return this.canShowAds;
    }

    public void setIsGameStartedFirstTime(bool isGameStartedFirstTime)
    {
        this.isGameStartedFirstTime = isGameStartedFirstTime;

    }

    public bool getIsGameStartedFirstTime()
    {
        return this.isGameStartedFirstTime;

    }
    //                                                                    ...............music
    public void setMusicOn(bool isMusicOn)
    {
        this.isMusicOn = isMusicOn;

    }

    public bool getMusicOn()
    {
        return this.isMusicOn;

    }
    //                                                                      .......music
        
    //....................................................for rate btn
    public void setRateClick(bool rateBtnClicked)
    {
        this.rateBtnClicked = rateBtnClicked;

    }

    public bool getRateClick()
    {
        return this.rateBtnClicked;

    }

    //best score
    public void setBestScore(int bestScore)
    {
        this.bestScore = bestScore;
    }

    public int getBestScore()
    {
        return this.bestScore;
    }

    //points
    public void setPoints(int points)
    {
        this.points = points;
    }

    public int getPoints()
    {
        return this.points;
    }

    //skin unlocked
    public void setSkinUnlocked(bool[] skinUnlocked)
    {
        this.skinUnlocked = skinUnlocked;
    }

    public bool[] getSkinUnlocked()
    {
        return this.skinUnlocked;
    }

    //selectedSkin
    public void setSelectedSkin(int selectedSkin)
    {
        this.selectedSkin = selectedSkin;
    }

    public int getSelectedSkin()
    {
        return this.selectedSkin;
    }

    //Theme unlocked
    public void setThemeUnlocked(bool[] themeUnlocked)
    {
        this.themeUnlocked = themeUnlocked;
    }

    public bool[] getThemeUnlocked()
    {
        return this.themeUnlocked;
    }

    //selectedTheme
    public void setSelectedTheme(int selectedTheme)
    {
        this.selectedTheme = selectedTheme;
    }

    public int getSelectedTheme()
    {
        return this.selectedTheme;
    }

}
