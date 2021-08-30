using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    // скорость прокрутки префаба Background.
    [SerializeField] float backgroundScrollSpeed = 0.5f;
    // получаем доступ к материи префаба Background.
    Material myMaterial;
    // получаем доступ к смещению префаба Background.
    Vector2 offset;

    void Start()
    {
        // при старте игры получаем доступ к мэш рэндеру и материи префаба Background.
        myMaterial = GetComponent<Renderer>().material;
        // скорость передвижения префаба Background по оси x 0, по оси y 0.5.
        offset = new Vector2(0f, backgroundScrollSpeed);
    }

    void Update()
    {
        // заставляем префаб Background двигаться каждый фрейм со скоростью 0.5, движение происходит плавно на любом пк.
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
