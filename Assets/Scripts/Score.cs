using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;


public class Score : MonoBehaviour
{
    public int score;
    public int money;
    public static int bestScoreStatic;
    public static int moneyStatic;
    [SerializeField] Text scoreText;
    [SerializeField] Text bestScoreText;
    [SerializeField] Text moneyText;
    private int lastScore;
    private int lastMoney;
    private int currentGameMoney;

    private const string Achievement_1 = "CgkI0tbC_e4CEAIQAw";
    private const string Achievement_2 = "CgkI0tbC_e4CEAIQAg";
    private const string Achievement_3 = "CgkI0tbC_e4CEAIQBA";
    private const string Achievement_4 = "CgkI0tbC_e4CEAIQBQ";
    private const string Achievement_5 = "CgkI0tbC_e4CEAIQBg";
    private const string Achievement_6 = "CgkI0tbC_e4CEAIQBw";
    private const string Achievement_7 = "CgkI0tbC_e4CEAIQCA";

    private const string leaderboard = "CgkI0tbC_e4CEAIQAQ";

    void Start(){
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => { 
            if(success){
                print("Success");
            }
            else {
                print("Unsuccess");
            }
        });
    }


    void OnEnable()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreStatic = bestScore;
        bestScoreText.text = "Best Score\n" + bestScore.ToString();

        int moneyScore = PlayerPrefs.GetInt("MoneyScore", 0);
        moneyStatic = moneyScore;
        moneyText.text = "Money Score\n" + moneyScore.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            score++;
        }
    }

    void Update()
    {
        scoreText.text = score.ToString();

        if (score != lastScore) {
            lastScore = score;
            money = Mathf.FloorToInt(score / 4f) - lastMoney;
            lastMoney += money;
            currentGameMoney += money; // Увеличиваем текущую игровую сумму
            MoneyScore();
        }

        moneyText.text = currentGameMoney.ToString(); // Отображаем текущую игровую сумму
    }

    public void BestScore()
    {
        if(score > bestScoreStatic)
        {
            bestScoreStatic = score;
            PlayerPrefs.SetInt("BestScore", bestScoreStatic);
            bestScoreText.text = "Best Score\n" + bestScoreStatic.ToString();
        }   
        if(bestScoreStatic >= 10) {
            GetTheAchievement(Achievement_1);
        }
        if(bestScoreStatic >= 50) {
            GetTheAchievement(Achievement_2);
        }
        if(bestScoreStatic >= 100) {
            GetTheAchievement(Achievement_3);
        }
        if(bestScoreStatic >= 200) {
            GetTheAchievement(Achievement_4);
        }
        if(bestScoreStatic >= 500) {
            GetTheAchievement(Achievement_5);
        }
        if(bestScoreStatic >= 1000) {
            GetTheAchievement(Achievement_6);
        }
        if(bestScoreStatic >= 2000) {
            GetTheAchievement(Achievement_7);
        }
        Social.ReportScore(bestScoreStatic, leaderboard,(bool success) => {});
        if(bestScoreStatic > PlayerPrefs.GetInt("BestScore")){
            PlayerPrefs.SetInt("BestScore", bestScoreStatic);
            Social.ReportScore(bestScoreStatic, leaderboard, (bool success) => {
                if (success) print("SuccessLeader");
            });
        }
    }

    public void MoneyScore()
    {
        moneyStatic += money;
        PlayerPrefs.SetInt("MoneyScore", moneyStatic);
    }

    private void GetTheAchievement(string id){
        Social.ReportProgress(id, 100.0f, (bool success) =>
        {
            if (success) print("Отримано досягнення " + id);
        });
    }
}
