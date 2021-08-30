using UnityEngine;

public class Shredder : MonoBehaviour
{  
    // Создаем метод уничтожения префабов Player Laser или Enemy Laser при соприкосновениий их коллайдера с коллайдером префаба Shredder.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // уничтожаем префаб Player Laser или Enemy Laser при соприкосновении с коллайдером префаба Shredder.
        Destroy(other.gameObject);
    }
}
