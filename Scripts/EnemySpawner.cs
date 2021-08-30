using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // создаем лист с настройками волн Enemy, помещаем в инспекторе в него волны с расширением .asset
    [SerializeField] List<WaveConfig> waveConfigs;
    // запускаем волны с нулевой.
    [SerializeField] int startingWave = 0;
    // волны по умолчанию не респавнятся.
    [SerializeField] bool looping = false;

    // создаем интерфейс/курутину для спавна всех волн при старте игры.
    IEnumerator Start()
    {
        // делать до тех пор
        do
        {
            // Запускаем курутину чтобы спавнить все волны, повторяться это будет бесконечно, пока игра активна, если хотим отключить
            // в инспекторе отключаем галочку looping.
            yield return StartCoroutine(SpawnAllWaves());
        }
        // пока цикл активен.
        while (looping);
    }

    // создаем интерфейс (корутину) для спавна всех волн.
    private IEnumerator SpawnAllWaves()
    {
        // начальный индекс волны 0, до тех пор пока индекс волны < конечной волны, волны будут сменяться.
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            // текущая волна - это лист, начинающийся с 0 волны.
            var currentWave = waveConfigs[waveIndex];
            // запускаем таймер спавна всех префабов Enemy в текущей волне.
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }      
    }

    // создаем интерфейс/курутину для спавна всех префабов Enemy в волне.
    // параметр WaveConfig нужен для того чтобы понимать откуда именно берутся настройки волн enemy.
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        // начальное количество префабов Enemy 0, до тех пор пока количество префабов Enemy < 5, префаб Enemy будет респавниться.
        for(int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            // Создаем копии префаба Enemy которые будут newEnemy, используем настройки путевых точек позиции из листа, начиная с 0 индекса, 
            // настройки поворота оставляем по умолчанию.   
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            // получаем доступ к скрипту EnemyPathing, для того чтобы передать значения
            // из текущего метода SpawnAllEnemiesInWave(WaveConfig waveConfig) в метод SetWaveConfig(waveConfig).
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            // очередная волна будет спавниться с интервалом в 1 секунду.
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }      
    }
}
