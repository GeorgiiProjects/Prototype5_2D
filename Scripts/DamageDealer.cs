using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    // создаем переменную для нанесения урона префабами Enemy Laser и Player Laser.
    [SerializeField] int damage = 100;

    // создаем публичный метод для нанесения урона префабами Enemy Laser и Player Laser.
    public int GetDamage()
    {
        // Используем данные урона в скриптах Enemy и Player.
        return damage;
    }

    // создаем публичный метод при котором попадем в хит префабу Enemy и префабу Player, и при этом префаб Enemy Laser и Player Laser уничтожаются.
    public void Hit()
    {
        // Используем данные уничтожения префаба Enemy Laser, либо префаба Player Laser в скриптах Enemy и Player.
        Destroy(gameObject);
    }
}
