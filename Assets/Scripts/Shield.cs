
using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private GameObject shieldPrefab; // Префаб щита

    public float minSpawnTime = 30.0f; // Минимальное время до спавна
    public float maxSpawnTime = 70.0f; // Максимальное время до спавна
    private float destroyDelay = 5.0f;

    private void Start()
    {
        // Вызываем метод для начала создания щитов
        StartCoroutine(ShieldSpawn());
    }

    private IEnumerator ShieldSpawn()
    {
        while (true) // Бесконечный цикл для создания щитов
        {
            // Ждем случайное время от 10 до 25 секунд
            float spawnDelay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnDelay);

            SpawnShieldObject();
        }
    }

    private void SpawnShieldObject()
    {
        if (shieldPrefab != null)
        {
            // Создаем щит на случайной позиции по X
            float leftLimit = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            float rightLimit = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            float randX = Random.Range(leftLimit, rightLimit);
            Vector2 spawnPosition = new Vector2(randX, transform.position.y);

            GameObject shieldInstance = Instantiate(shieldPrefab, spawnPosition, Quaternion.identity);

            // Устанавливаем задержку перед удалением (это необязательно, если используется пул объектов)
            Destroy(shieldInstance, destroyDelay);
        }
    }
}