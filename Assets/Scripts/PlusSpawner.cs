using System.Collections;
using UnityEngine;

public class PlusSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject heartPrefab; // Префаб сердечка

    private Heartsystem heartSystem; // Ссылка на скрипт Heartsystem

    private float minSpawnTime = 10.0f; // Минимальное время до спавна
    private float maxSpawnTime = 25.0f; // Максимальное время до спавна
    private float destroyDelay = 5.0f;

    private void Start()
    {
        // Получаем доступ к скрипту Heartsystem на этом же объекте
        heartSystem = GetComponent<Heartsystem>();

        // Вызываем метод для начала создания сердечка
        StartCoroutine(SpawnHeart());
    }

    private IEnumerator SpawnHeart()
    {
        while (true) // Infinite loop for respawning hearts
        {
            // Check the current player's health using the class name (Heartsystem)
            int playerHealth = Heartsystem.health; // Use the class name to access the static variable

            // If the player has less than 3 hearts, spawn a new heart
            if (playerHealth < 3)
            {
                // Wait for a random time between 5 and 20 seconds
                float spawnDelay = Random.Range(minSpawnTime, maxSpawnTime);
                yield return new WaitForSeconds(spawnDelay);

                SpawnHeartObject();
            }
            if (playerHealth == 3)
            {
                // If the player already has 3 hearts, wait for some time and check again
                yield return new WaitForSeconds(5.0f); // Wait for 5 seconds before the next check
            }
        }
    }

    private void SpawnHeartObject()
    {
        if (heartPrefab != null)
        {
            // Создаем сердечко на случайной позиции по X
            float leftLimit = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0, 0)).x; // Оставляем 10% от левого края
            float rightLimit = Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0, 0)).x; // Оставляем 10% от правого края
            float randX = Random.Range(leftLimit, rightLimit);
            Vector2 spawnPosition = new Vector2(randX, transform.position.y);

            GameObject heartInstance = Instantiate(heartPrefab, spawnPosition, Quaternion.identity);

            // После уничтожения сердечка, возвращаем его в пул объектов (может потребоваться настройка пула объектов)
            // Если пул объектов не используется, вы можете просто уничтожать сердечко как ранее.

            // Устанавливаем задержку перед удалением (это необязательно, если используется пул объектов)
            Destroy(heartInstance, destroyDelay);
        }
    }
}
