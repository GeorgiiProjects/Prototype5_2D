using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Заголовок в инспекторе в префабе Enemy.
    [Header("Enemy Stats")] 
    // количество жизней префаба Enemy.
    [SerializeField] float health = 100f;
    // количество очков за уничтоженный префаб Enemy.
    [SerializeField] int scoreValue = 150;

    // Заголовок в инспекторе в префабе Enemy.
    [Header("Enemy Shooting")]
    // счетчик выстрелов для рандомизации выстрелов, сериализируем для дебага.
    [SerializeField] float shotCounter;
    // минимальное время между выстрелами.
    [SerializeField] float minTimeBetweenShots = 0.2f;
    // максимальное время между выстрелами.
    [SerializeField] float maxTimeBetweenShots = 3f;
    // создаем GameObject для того тобы поместить в него префаб Enemy Laser в инспекторе.
    [SerializeField] GameObject projectile;
    // создаем GameObject для того тобы поместить в него префаб Explosion Particle (эффект) в инспекторе.
    [SerializeField] GameObject deathVFX;
    // длина времени префаба Explosion Particle после уничтожения префаба Enemy.
    [SerializeField] float durationOfExplosion = 1f;
    // скорость стрельбы лазера.
    [SerializeField] float projectileSpeed = 10f;

    // Заголовок в инспекторе в префабе Enemy.
    [Header("Enemy Sound Effects")]
    // создаем класс AudioClip для того тобы поместить в него аудио смерти префаба Enemy в инспекторе.
    [SerializeField] AudioClip deathSound;
    // громкость проигрывания аудио deathSoundVolume, сможем менять значения в инспекторе в диапазоне от 0 до 1.
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.25f;
    // создаем класс AudioClip для того тобы поместить в него аудио стрельбы префаба Enemy в инспекторе.
    [SerializeField] AudioClip shootSound;
    // громкость проигрывания аудио shootSoundVolume, сможем менять значения в инспекторе в диапазоне от 0 до 1.
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    void Start()
    {
        // рандомизируем скорость стрельбы префаба Enemy между минимумом и максимумом при старте игры.
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

    }

    void Update()
    {
        // Вызываем метод каждый фрейм.
        CountDownAndShoot();
    }

    // создаем метод для рандомизации и плавности стрельбы на всех пк.
    private void CountDownAndShoot()
    {
        // придаем плавность выстрелам на всех пк, иначе будет один сплошной выстрел.
        shotCounter -= Time.deltaTime;
        // если рандомизация выстрелов <= 0
        if(shotCounter <= 0f)
        {
            // префаб Enemy стреляет.
            Fire();
            // рандомизируем скорость стрельбы префаба Enemy, иначе будет один сплошной выстрел.
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    // Создаем метод для того чтобы префаб Enemy мог атаковать и проигрывать звук выстрела.
    private void Fire()
    {
        // создаем копии префаба Enemy Laser, в координатах нахождения префаба Enemy, поворот оставляем по умолчанию.
        // используем as GameObject чтобы быть на 100% увереным что создание копий распознается как игровой объект.
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        // Получаем доступ Rigidbody2D префаба Enemy Laser и его velocity (скорости), 
        // задаем скорость полета по оси х 0, по оси у -10, для того чтобы атака шла сверху вниз в игре.
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        // проигрываем аудио во время стрельбы префаба Enemy Laser в координатах префаба Main Camera с громкостью 0.25f.
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    // Создаем метод для взаимодествия коллайдера префаба Enemy с коллайдером префаба Player Laser.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Получаем доступ к коллайдеру префаба Player Laser.
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        // если коллайдер на префабе Player Laser отсутствует.
        if (!damageDealer)
        {
            // урон префабу Enemy не наносится.
            return;
        }
        // вызываем метод нанесений урона с параметром данных из метода OnTriggerEnter2D(Collider2D other).
        ProcessHit(damageDealer);
    }

    // создаем метод для нанесения урона и уничтожения префаба Enemy с параметром DamageDealer, данные берем из метода OnTriggerEnter2D(Collider2D other).
    private void ProcessHit(DamageDealer damageDealer)
    {
        // убавляем здоровье префаба Enemy, нанося ему урон.
        health -= damageDealer.GetDamage();
        // попадаем в префаб Enemy, префаб Player Laser уничтожается.
        damageDealer.Hit();
        // Если жизней у префаба Enemy <= 0
        if (health <= 0)
        {
            // префаб Enemy уничтожается.
            Die();        
        }
    }

    // создаем метод для уничтожения префаба Enemy, прекращения анимации и проигрывания аудио.
    private void Die()
    {
        // получаем доступ к скрипту GameSession в текущем скрипте, прибавляем 150 очков за каждое уничтожение префаба Enemy.
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        // уничтожаем префаб Enemy.
        Destroy(gameObject);
        // копируем эффект взрыва, в координатах нахождения Enemy, поворот оставляем по умолчанию. 
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        // прекращаем взрыв через 1 секунду.
        Destroy(explosion, durationOfExplosion);
        // проигрываем аудио в координатах камеры с громкостью 0.25f 
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
}
