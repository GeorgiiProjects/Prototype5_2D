using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    // создаем класс Text, для того чтобы получить доступ к компоненту текст в префабе Canvas - Health Text.
    Text healthText;
    // создаем класс Player, для того чтобы получить доступ к скрипту Player в текущем скрипте.
    Player player;

    void Start()
    {
        // получаем доступ к компоненту текст в префабе Canvas - Health Text.
        healthText = GetComponent<Text>();
        // получаем доступ к скрипту Player в текущем скрипте.
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        // обновляем здоровье каждый кадр, конвертируя число в строку.
        healthText.text = player.GetHealth().ToString();
    }
}
