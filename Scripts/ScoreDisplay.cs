using UnityEngine;
using UnityEngine.UI;


public class ScoreDisplay : MonoBehaviour
{
    // создаем класс Text, для того чтобы получить доступ к компоненту текст в префабе Canvas - Score Text.
    Text scoreText;
    // создаем класс GameSession, для того чтобы получить доступ к скрипту GameSession в текущем скрипте.
    GameSession gameSession;

    void Start()
    {
        // получаем доступ к компоненту текст в префабе Canvas - Score Text.
        scoreText = GetComponent<Text>();
        // получаем доступ к скрипту GameSession в текущем скрипте.
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        // обновляем счет каждый кадр (счет по прежнему остается 0), конвертируя число в строку.
        scoreText.text = gameSession.GetScore().ToString();
    }
}
