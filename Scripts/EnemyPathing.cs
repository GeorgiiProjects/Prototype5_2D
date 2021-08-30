using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    // создаем WaveConfig для того чтобы использовать его в текущем скрипте.
    WaveConfig waveConfig;
    // создаем лист типа Transform так как используем координаты нахождения по x и y waypoints.
    List<Transform> waypoints;
    // начинаем с нулевой точки так как лист начинает отсчет как и массив с 0.
    int waypointIndex = 0;

    void Start()
    {
        // вызываем при старте игры метод содержащий в себе лист с координатами waveWaypoints по которым будут двигаться префабы Enemy.
        waypoints = waveConfig.GetWaypoints();
        // позиция путевых точек(waypoints) начинается с нулевого массива и далее 1, 2 итд.
        transform.position = waypoints[waypointIndex].transform.position;
    }

    void Update()
    {
        // движение врага будет выполняться каждый фрейм.
        EnemyMove();
    }

    // создаем публичный метод с параметром WaveConfig для получения данных из WaveConfig скрипта EnemySpawner 
    // и его метода SpawnAllEnemiesInWave(WaveConfig waveConfig), метод будет вызываться в скрипте EnemySpawner.
    public void SetWaveConfig(WaveConfig waveConfigReceived)
    {
        // данные из waveConfig поступают в this.waveConfig которое принадлежит только классу/скрипту EnemyPathing.
        this.waveConfig = waveConfigReceived;
    }

    // Создаем метод передвижения префаба Enemy между позициями.
    private void EnemyMove()
    {
        // если значение индекса пути (т.е. еще есть точки пути в запасе) <= значения финальной точки пути - 1 
        // используем - 1 так как используется оператор <=, а не просто <, используем .count так как используем лист, а не массив.
        if (waypointIndex <= waypoints.Count - 1)
        {
            // начинаем двигаться от 0 позиции к 1,2 и.т.д.
            var targetPosition = waypoints[waypointIndex].transform.position;
            // скорость с которой будет передвигаться префаб Enemy между позициями (координатами), скорость одинаковая для всех пк.
            float movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            // движение просходит от текущей позиции, к координатам цели, с заданной скоростью.
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            // если координаты текущей позиции равны координатам нахождения цели.
            if (transform.position == targetPosition)
            {
                // двигаемся к следующей позиции, от 0 к 1, от 1 ко 2 итд.
                waypointIndex++;
            }
        }
        // иначе
        else
        {
            // префаб Enemy самоуничтожается в финальной точке (позиции).
            Destroy(gameObject);
        }
    }
}
