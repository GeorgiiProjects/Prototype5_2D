using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // запуск происходит в самом начале перед методом Start()
    void Awake()
    {
        // вызываем метод перед методом старт, убеждаемся что префаб Music Player в сцене только один.
        SetUpSingleton();
    }

    // создаем метод для проверки существует ли лишний префаб Music Player в сцене.
    private void SetUpSingleton()
    {
        // ищем объект/префаб Music Player через метод GetType() при помощи массива, если кол-во MusicPlayer в сцене > 1
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            // уничтожаем лишний префаб MusicPlayer в сцене.
            Destroy(gameObject);
        }
        // иначе
        else
        {
            // не уничтожаем префаб MusicPlayer в сцене.
            DontDestroyOnLoad(gameObject);
        }
    }
}
