using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    float playerWidth;

    public float moveSpeed = 10.0f; // Увеличил скорость перемещения игрока
    public float acceleration = 10.0f; // Увеличил ускорение
    public float arrivalThreshold = 0.1f; // Порог прибытия
    public float mouseSmoothing = 0.2f;
    private Vector3 targetPosition;

    float screenWidth; // Переместили сюда
    float screenHeight; // Переместили сюда

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerWidth = GetComponent<SpriteRenderer>().bounds.size.x;

        screenWidth = Screen.width; // Инициализируем здесь
        screenHeight = Screen.height; // Инициализируем здесь
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.y = transform.position.y;
        }
    }

    void FixedUpdate()
    {
        float screenRatio = screenWidth / screenHeight;
        float cameraHeight = Camera.main.orthographicSize * 2f;
        float cameraWidth = cameraHeight * screenRatio;

        float maxX = cameraWidth / 2 - playerWidth / 2;
        float minX = -maxX;

        float distanceToTarget = targetPosition.x - transform.position.x;

        float direction = (distanceToTarget > 0) ? 1.0f : -1.0f;
        float targetSpeed = direction * moveSpeed;

        float currentSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, Time.fixedDeltaTime * acceleration);

        if (Mathf.Abs(distanceToTarget) < arrivalThreshold)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
        }

        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
