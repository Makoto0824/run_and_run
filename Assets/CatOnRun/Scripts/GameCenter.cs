using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;


public class GameCenter : MonoBehaviour
{

    // リーダボードID itunesConnectで設定した値を入力
    private const string leaderBoardId = "runrun_leaderboard";

    GameObject highscore;
    GuiManager script;
    GuiManager script2;


    bool gameCenterLogin;

    void Start()
    {

        highscore = GameObject.Find("Main Camera");
        script = highscore.GetComponent<GuiManager>();

        // ProcessAuthentication コールバックを認証し登録します。
        // Social API の他の呼び出しを始める前に、この呼び出しが必要です。
        //Social.localUser.Authenticate(ProcessAuthentication);
    }

    // Authenticate が終了したときにこの関数が呼び出されます。
    // 操作が成功したら、Social.localUser はサーバーからのデータを格納します。
    void ProcessAuthentication(bool success)
    {
        if (success)
        {
            Debug.Log("認証成功");

            // loaded achievements を招集し、それを処理するためのコールバックを登録します
            Social.LoadAchievements(ProcessLoadedAchievements);
            gameCenterLogin = true;

            Social.ShowLeaderboardUI();
        }
        else
        {
            Debug.Log("認証失敗");
            gameCenterLogin = false;

            script.GameCenterLoginOpen();
        }


    }

    //  LoadAchievement コールが終了すると、関数が呼び出されます。
    void ProcessLoadedAchievements(IAchievement[] achievements)
    {
        if (achievements.Length == 0)
            Debug.Log("Error: no achievements found");
        else
            Debug.Log("Got " + achievements.Length + " achievements");
    }

    public void GameCenterLoginCheck()
    {
        // ProcessAuthentication コールバックを認証し登録します。
        // Social API の他の呼び出しを始める前に、この呼び出しが必要です。
        Social.localUser.Authenticate(ProcessAuthentication);
        // n秒後にゲームセンター起動
        //Invoke("ShowLeaderBoard", 1f);
    }

    // リーダーボードの表示
    public void ShowLeaderBoard()
    {
        if (gameCenterLogin == true)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            script.GameCenterLoginOpen();
        }
    }

    //// リーダーボードの表示
    //public void ShowLeaderBoard()
    //{
    //    Social.ShowLeaderboardUI();
    //}

    // ハイスコアの送信処理
    public void SendScore()
    {

        int score = script._highScore;

        {
            Social.ReportScore(score, leaderBoardId, success =>
            {
                if (success)
                {
                    // 送信が成功した時の処
                    script.ScoreSendSuccess();
                    Debug.Log("ハイスコア送信成功");
                }
                else
                {
                    // 送信が失敗した時の処理
                    script.GameCenterLoginOpen();
                    Debug.Log("ハイスコア送信失敗");
                }
            });
        }
    }
}