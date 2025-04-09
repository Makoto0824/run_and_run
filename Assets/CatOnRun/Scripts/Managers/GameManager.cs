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
    public bool isGameStartedFirstTime;
    [HideInInspector]
    public bool isMusicOn;
    public int bestScore;
    public bool[] skinUnlocked, themeUnlocked;
    public int selectedSkin, selectedTheme;
    public int points;
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

            data = new GameData();

            data.setIsGameStartedFirstTime(isGameStartedFirstTime);
            data.setMusicOn(isMusicOn);
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
            bestScore = data.getBestScore();
            points = data.getPoints();
            selectedSkin = data.getSelectedSkin();
            skinUnlocked = data.getSkinUnlocked();
            selectedTheme = data.getSelectedTheme();
            themeUnlocked = data.getThemeUnlocked();
        }
    }

    public void Save()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + "/GameData.dat");

            if (data != null)
            {
                data.setIsGameStartedFirstTime(isGameStartedFirstTime);
                data.setMusicOn(isMusicOn);
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
    }

    public void ResetGameManager()
    {
        isGameStartedFirstTime = false;
        isMusicOn = true;
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

        Save();
    }
}

[Serializable]
class GameData
{
    private bool isGameStartedFirstTime;
    private bool isMusicOn;
    private int bestScore;
    private bool[] skinUnlocked, themeUnlocked;
    private int selectedSkin, selectedTheme;
    private int points;

    public void setIsGameStartedFirstTime(bool isGameStartedFirstTime)
    {
        this.isGameStartedFirstTime = isGameStartedFirstTime;
    }

    public bool getIsGameStartedFirstTime()
    {
        return isGameStartedFirstTime;
    }

    public void setMusicOn(bool isMusicOn)
    {
        this.isMusicOn = isMusicOn;
    }

    public bool getMusicOn()
    {
        return isMusicOn;
    }

    public void setBestScore(int bestScore)
    {
        this.bestScore = bestScore;
    }

    public int getBestScore()
    {
        return bestScore;
    }

    public void setPoints(int points)
    {
        this.points = points;
    }

    public int getPoints()
    {
        return points;
    }

    public void setSkinUnlocked(bool[] skinUnlocked)
    {
        this.skinUnlocked = skinUnlocked;
    }

    public bool[] getSkinUnlocked()
    {
        return skinUnlocked;
    }

    public void setSelectedSkin(int selectedSkin)
    {
        this.selectedSkin = selectedSkin;
    }

    public int getSelectedSkin()
    {
        return selectedSkin;
    }

    public void setThemeUnlocked(bool[] themeUnlocked)
    {
        this.themeUnlocked = themeUnlocked;
    }

    public bool[] getThemeUnlocked()
    {
        return themeUnlocked;
    }

    public void setSelectedTheme(int selectedTheme)
    {
        this.selectedTheme = selectedTheme;
    }

    public int getSelectedTheme()
    {
        return selectedTheme;
    }
}
