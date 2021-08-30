using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Заголовок в инспекторе в префабе Player.
    [Header("Player Movement")]
    // создаем переменную для скорости движения префаба Player.
    [SerializeField] float moveSpeed = 10f;
    // создаем переменную для ограничения движения префаба Player за пределы экрана.
    [SerializeField] float padding = 0.5f;
    // количество жизней префаба Player.
    [SerializeField] int health = 300;

    // Заголовок в инспекторе в префабе Player.
    [Header("Projectile")]
    // создаем GameObject для того тобы поместить в него префаб Player laser в инспекторе.
    [SerializeField] GameObject laserPrefab;
    // скорость движения префаба лазера.
    [SerializeField] float projectileSpeed = 10f;
    // промежуток времени между выстрелами.
    [SerializeField] float projectileFiringPeriod = 0.1f;
    // создаем класс AudioClip для того тобы поместить в него аудио смерти префаба Player в инспекторе.
    [SerializeField] AudioClip deathSound;
    // громкость проигрывания аудио deathSoundVolume, сможем менять значения в инспекторе в диапазоне от 0 до 1.
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.25f;
    // создаем класс AudioClip для того тобы поместить в него аудио стрельбы префаба Player в инспекторе.
    [SerializeField] AudioClip shootSound;
    // громкость проигрывания аудио shootSoundVolume, сможем менять значения в инспекторе в диапазоне от 0 до 1.
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    // создаем курутину (таймер) для определения того активны будут выстрелы или нет.
    Coroutine firingCoroutine;

    // минимальная граница камеры по оси х.
    float xMin;
    // максимальная граница камеры по оси х.
    float xMax;
    // минимальная граница камеры по оси y.
    float yMin;
    // максимальная граница камеры по оси y.
    float yMax;

    void Start()
    {
        // Вызываем метод при старте игры.
        SetUpMoveBoundaries();
    }

    void Update()
    {
        // префаб Player стреляет каждый фрейм.
        Fire();
        // префаб Player двигается каждый фрейм.
        Move();
    }

    // создаем метод для создания стрельбы в префабе Player.
    private void Fire()
    {
        // если нажмиаем ctrl на клавиатуре.
        if (Input.GetButtonDown("Fire1"))
        {
            // активируется курутина (таймер) и выстрелы идут бесконечно.
            firingCoroutine = StartCoroutine(FireContinuously());   
        }
        // если не нажимаем ctrl на клавиатуре.
        if (Input.GetButtonUp("Fire1"))
        {
            // курутина (таймер) прекращает работу и выстрелы прекращаются.
            StopCoroutine(firingCoroutine);
        }
    }

    // создаем курутину/интерфейс FireContinuously() для того, чтобы использовать таймер постоянной атаки лазером.
    IEnumerator FireContinuously()
    {
        // пока это происходит.
        while (true)
        {
            // копируем префаб Player Laser, в координатах нахождения Player, поворот оставляем по умолчанию.
            // используем as GameObject чтобы быть на 100% увереным что создание копий распознается как игровой объект.
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            // Получаем доступ Rigidbody2D префаба Player Laser и его velocity (скорости), 
            // задаем скорость полета по оси х 0, по оси у 10, для того чтобы атака шла снизу вверх в игре.
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            // проигрываем аудио во время стрельбы префаба Player Laser в координатах префаба Main Camera с громкостью 0.25f.
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            // стрельба происходит каждые 0.1 секунду
            yield return new WaitForSeconds(projectileFiringPeriod);
        }     
    }

    // создаем метод для передвижения префаба Player.
    private void Move()
    {   // Создаем переменную для использования горизонтальных кнопок клавиатуры с одинаковой скоростью на любом пк.
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        // позиция движения Player по оси x будет вычисляться текущей позицей Player + deltaX, границы движения на экране от xMin до xMax.
        float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        // Создаем переменную для использования вертикальных кнопок клавиатуры с одинаковой скоростью на любом пк.
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        // позиция движения Player по оси y будет вычисляться текущей позицей Player + deltaY, границы движения на экране от yMin до yMax.
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        // Передвигаем Player по оси х и по оси y, старт игры происходит из начальных координат указанных в инспекторе. 
        transform.position = new Vector2(newXPos, newYPos);
    }

    // создаем метод для границы камеры по которым может передвигаться Player.
    private void SetUpMoveBoundaries()
    {
        // получаем доступ к Main Camera.
        Camera gameCamera = Camera.main;
        // Задаем минимальную границу камеры по оси х 0 + значение padding.
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        // Задаем максимальную границу камеры по оси х 1 - значение padding.
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        // Задаем минимальную границу камеры по оси y 0 + значение padding.
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        // Задаем максимальную границу камеры по оси y 1 - значение padding.
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    // Создаем метод для взаимодествия коллайдера префаба Player с коллайдером префаба Enemy Laser.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // получаем доступ к коллайдеру префаба Enemy laser.
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        // если коллайдер на префабе Enemy laser отсутствует.
        if (!damageDealer)
        {
            // урон префабу Player не наносится.
            return;
        }
        // вызываем метод нанесений урона с параметром данных из метода OnTriggerEnter2D(Collider2D other).
        ProcessHit(damageDealer);
    }

    // создаем метод для нанесения урона и уничтожения Player с параметром DamageDealer, данные берутся из метода OnTriggerEnter2D(Collider2D other).
    private void ProcessHit(DamageDealer damageDealer)
    {
        // убавляем здоровье префаба Player, нанося ему урон.
        health -= damageDealer.GetDamage();
        // попадаем в префаб Player, префаб Enemy Laser уничтожается.
        damageDealer.Hit();
        // Если жизней у префаба Player <= 0
        if (health <= 0)
        {
            // префаб Player уничтожается.
            Die();
        }
    }

    // создаем метод для уничтожения префаба Player, перехода на сцену конец игры и проигрывания аудио.
    private void Die()
    {
        // Получаем доступ к скрипту Level и запускаем сцену конец игры.
        FindObjectOfType<Level>().LoadGameOver();
        // уничтожаем префаб Player.
        Destroy(gameObject);      
        // проигрываем аудио в координатах камеры с громкостью 0.25f 
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }

    // создаем публичный метод для обновления здоровья, стартовое здоровье 300.
    public int GetHealth()
    {
        // Используем данные обновления здоровья в скрипте HealthDisplay.
        return health;
    }
}
