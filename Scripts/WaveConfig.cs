using System.Collections.Generic;
using UnityEngine;

// Создаем новый ScriptableObject, для того чтобы в Unity появилось меню Enemy Wave Config и можно было создавать новые файлы
// конфигурации статуса enemy в файлах с расширением asset.
[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    // создаем GameObject для того тобы поместить в него префаб Enemy в инспекторе.
    [SerializeField] GameObject enemyPrefab;
    // создаем GameObject для того тобы поместить в префаб Path в инспекторе.
    [SerializeField] GameObject pathPrefab;
    // время между появлением очередной волны.
    [SerializeField] float timeBetweenSpawns = 1f;
    // количество врагов в каждой волне.
    [SerializeField] int numberOfEnemies = 5;
    // скорость передвижения enemy
    [SerializeField] float moveSpeed = 2f;

    // создаем публичный метод так как скрипт EnemySpawner будет получать информацию из этого метода.
    public GameObject GetEnemyPrefab()
    {
        // Используем данные префаба Enemy.
        return enemyPrefab;
    }

    // создаем публичный метод так как скрипты EnemyPathing и EnemySpawner будут получать информацию из этого метода.
    // метод будет содежать в себе лист с координатами waveWaypoints.
    public List<Transform> GetWaypoints()
    {
        // создаем переменную которая будет содержать в себе координаты waveWaypoints, они находятся в листе и содержат координаты.
        var waveWaypoints = new List<Transform>();
        // создаем цикл который содержит в себе координаты waveWaypoints (Transform child)
        // являющимся дочерним объектом в префабе Path.
        foreach (Transform child in pathPrefab.transform)
        {
            // добавляем в лист waveWaypoints объекты child (Waypoints в префабе Path в инспекторе).
            waveWaypoints.Add(child);
        }
        // используем координаты передвижения префабов Enemy в скрипте Enemy Pathing.
        return waveWaypoints;
    }

    // создаем публичный метод так как скрипт EnemySpawner будет получать информацию из этого метода.
    public float GetTimeBetweenSpawns()
    {
        // Используем данные спавна между волнами.
        return timeBetweenSpawns;
    }

    // создаем публичный метод так как скрипт EnemySpawner будет получать информацию из этого метода.
    public int GetNumberOfEnemies()
    {
        // Используем данные количества Enemy.
        return numberOfEnemies;
    }

    // создаем публичный метод так как скрипт EnemyPathing будет получать информацию из этого метода.
    public float GetMoveSpeed()
    {
        // Используем данные скорости передвижения Enemy.
        return moveSpeed;
    }
}
