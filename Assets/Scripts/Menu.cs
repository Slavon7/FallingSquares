using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class Menu : MonoBehaviour
{
    public Text bestScoreText;
    public Text moneyText;

    private const string Achievement_1 = "CgkI0tbC_e4CEAIQAw";
    private const string Achievement_2 = "CgkI0tbC_e4CEAIQAg";
    private const string Achievement_3 = "CgkI0tbC_e4CEAIQBA";

    private const string leaderboard = "CgkI0tbC_e4CEAIQAQ";

    void Start()
    {       
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) => { 
            if(success){
                print("Success");
            }
            else {
                print("Unsuccess");
            }
        });

        // Получаем значение сохраненного рекорда
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        // Выводим значение на экран
        bestScoreText.text = "Best Score\n" + bestScore.ToString();

        // Получаем значение сохраненного количества денег
        int moneyScore = PlayerPrefs.GetInt("MoneyScore", 0);
        // Выводим значение на экран
        moneyText.text = moneyScore.ToString();
    }
}