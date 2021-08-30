using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    // переменная для задержки загрузки следующего уровня после смерти.
    [SerializeField] float delayInSeconds = 2f;

    // метод для загрузки начального уровня (меню).
    private void LoadStartMenu()
    {
        // Загружаем начальный уровень (меню).
        SceneManager.LoadScene(0);
    }

    // метод для загрузки первого уровня.
    private void LoadFirstLevel()
    {
        // Загружаем первый уровень.
        SceneManager.LoadScene(1);
        // получаем доступ к скрипту GameSession в текущем скрипте и сбрасываем количество очков, здоровья итд. на ноль.
        FindObjectOfType<GameSession>().ResetGame();
    }

    // публичный метод для загрузки уровня конец игры, вызывается в скрипте Player.
    public void LoadGameOver()
    {
        // запускаем курутину и через 2 секунды загружается уровень конец игры.
        StartCoroutine(WaitAndLoad());         
    }

    // создаем курутину/интерефейс для загрузки уровня конец игры через 2 секунды после смерти.
    IEnumerator WaitAndLoad()
    {
        // запускаем уровень конец игры через 2 секунды после смерти.
        yield return new WaitForSeconds(delayInSeconds);
        // Загружаем уровень конец игры.
        SceneManager.LoadScene(2);
    }

    // Метод для выхода из игры.
    private void QuitGame()
    {
        // выходим из игры.
        Application.Quit();
    }
}
