using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] Text leaderboardText;

    void Start()
    {
        // Вызываем метод для отображения лидерборда
        DisplayLeaderboard();
    }

    public void DisplayLeaderboard()
    {
        // Проверяем, авторизован ли игрок в Play Games
        if (Social.localUser.authenticated)
        {
            // Получаем лидерборд
            ILeaderboard leaderboard = Social.CreateLeaderboard();
            leaderboard.id = "CgkI0tbC_e4CEAIQAQ"; // ID вашего лидерборда

            // Загружаем лидерборд
            leaderboard.LoadScores(success =>
            {
                if (success)
                {
                    // Получаем список результатов
                    IScore[] scores = leaderboard.scores;
                    string leaderboardInfo = "Leaderboard:\n";

                    // Формируем текст для отображения
                    foreach (IScore score in scores)
                    {
                        leaderboardInfo += $"{score.rank}. {score.userID}: {score.value}\n";
                    }

                    // Обновляем текстовое поле с информацией о лидерборде
                    leaderboardText.text = leaderboardInfo;
                }
                else
                {
                    // Обработка ошибки загрузки лидерборда
                    leaderboardText.text = "Failed to load leaderboard";
                }
            });
        }
        else
        {
            // Игрок не авторизован в Play Games
            leaderboardText.text = "Not authenticated";
        }
    }
}
