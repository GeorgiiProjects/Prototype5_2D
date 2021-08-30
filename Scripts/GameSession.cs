using UnityEngine;

public class GameSession : MonoBehaviour
{   
    // начальное количество очков.
    int score = 0;

    // запуск происходит в самом начале перед методом Start()
    void Awake()
    {
        // вызываем метод перед методом старт, убеждаемся что префаб GameSession в сцене только один.
        SetUpSingleton();
    }

    // создаем метод для проверки существует ли лишний префаб Game Session в сцене.
    private void SetUpSingleton()
    {
        // ищем количество префабов GameSession в сцене при помощи массива.
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        // если кол-во игровых сессий > 1
        if(numberGameSessions > 1)
        {
            // уничтожаем лишний префаб GameSession.
            Destroy(gameObject);
        }
        else
        {
            // иначе не уничтожаем.
            DontDestroyOnLoad(gameObject);
        }      
    }

    // создаем публичный метод для обновления счета (счет по прежнему остается 0).
    public int GetScore()
    {
        // Используем данные обновления счета в скрипте ScoreDisplay.
        return score;
    }

    // создаем публичный метод для обновления счета с параметром количества очков за уничтожение префаба Enemy.
    public void AddToScore(int scoreValue)
    {
        // Используем данные обновления счета в скрипте Enemy, текущий счет 0 + 150 за уничтожение префаба Enemy, затем 150 + 150, 300 + 150 итд.
        score += scoreValue;
    }

    // создаем публичный метод для сброса данных (очков, здоровья, итд.).
    public void ResetGame()
    {
        // Используем данные для сброса очков, здоровья итд. в скрипте Level, уничтожаем префаб Game Session.
        Destroy(gameObject);
    }
}
