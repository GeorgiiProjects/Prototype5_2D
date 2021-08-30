using UnityEngine;

public class Spinner : MonoBehaviour
{
    // скорость вращения префаба Big Enemy Laser.
    [SerializeField] float speedOfRotateSpin = 1f;

    void Update()
    {
        // задаем скорость вращения префаба Big Enemy Laser каждый кадр по оси x и y = 0, по оси z при помощи формулы, с одинаковой скоростью на всех пк.
        transform.Rotate(0, 0, speedOfRotateSpin * Time.deltaTime);
    }
}
