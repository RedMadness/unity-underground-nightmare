using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D body;

    [SerializeField, Tooltip("Скорость передвижения")]
    private float speed = 5f;

    [SerializeField, Tooltip("Скорость замедления")]
    private float deceleration = 5f;

    [SerializeField, Tooltip("Компонент направления")]
    private DirectionComponent directionComponent;

    // Сгенерированный класс, на основе конструктора ./InputSystem_Actions.inputactions
    private InputSystem input;


    // Вызывается один раз, сразу при создании объекта в сцене, ещё до того, как игра начнётся (даже если объект выключен)
    private void Awake()
    {
        input = new InputSystem();

        /* 
            Что тут происходит:
            input.Player.Move - конкретное действие Move. полученное из InputSystem.performed - событие, срабатывающее когда действие совершается
            += payload => direction = payload.ReadValue<Vector2>():
                += - подписка на событие лямбда функцией
                payload приходящее вместе с событием значение
                все что дальше - тело лямбда функции. Мы присваиваем своей переменной direction то что пришло в payload

        */
        input.Player.Move.performed += payload => directionComponent.Direction = payload.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => directionComponent.Direction = Vector2.zero;
    }


    // Вызывается каждый раз, когда объект включается (в Unity можно включать\выключать объекты)
    // Нужно чтобы включить логику, которая зависит от активного объекта
    private void OnEnable()
    {
        input.Enable(); // Включаем Input System
    }


    // Аналогично OnEnable, но вызывается при выключении объекта
    private void OnDisable()
    {
        input.Disable(); // Выключаем Input System
    }


    // Вызывается каждые X миллисекунд (по умолчанию 50 раз в секунду), не зависимо от FPS.
    void FixedUpdate()
    {
        if (directionComponent.Direction == Vector2.zero && body.linearVelocity.magnitude > 0.03f)
        {
            // Постепенно замедляем движение, если ввода нет
            body.linearVelocity = Vector2.Lerp(body.linearVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Двигаем игрока с постоянной скоростью
            body.linearVelocity = directionComponent.Direction * speed;
        }
    }
}
