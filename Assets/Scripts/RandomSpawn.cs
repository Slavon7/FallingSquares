using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;
    Vector2 whereToSpawn;
    public float initialSpawnRate = 1.5f;
    public float acceleration = 0.5f;
    private int spawnCount = 0;

    private List<SpawnRateRange> spawnRateRanges = new List<SpawnRateRange>
    {
        new SpawnRateRange(5, 1.3f),
        new SpawnRateRange(15, 1.0f),
        new SpawnRateRange(30, 0.8f),
        new SpawnRateRange(60, 0.7f),
        new SpawnRateRange(90, 0.6f),
        new SpawnRateRange(120, 0.5f),
        new SpawnRateRange(180, 0.4f),
        new SpawnRateRange(300, 0.35f)
    };

    private float nextSpawnTime;

    private void Start()
    {
        nextSpawnTime = Time.time + initialSpawnRate;
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnObject();
            spawnCount++;
            nextSpawnTime = Time.time + GetSpawnRate();
        }
    }

    private float GetSpawnRate()
    {
        foreach (SpawnRateRange range in spawnRateRanges)
        {
            if (spawnCount < range.maxCount)
            {
                return range.spawnRate;
            }
        }

        // Если spawnCount стал очень большим, возвращаем последнее значение
        return spawnRateRanges[spawnRateRanges.Count - 1].spawnRate;
    }

    public void SpawnObject()
    {
        float leftLimit = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0, 0)).x; // Оставляем 10% от левого края
        float rightLimit = Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0, 0)).x; // Оставляем 10% от правого края;
        
        float RandX = Random.Range(leftLimit, rightLimit);
        float RandY = transform.position.y; // Фиксируем Y-координату на уровне объекта
        float RandScale = Random.Range(0.5f, 0.7f);
        
        // Ограничиваем RandX в пределах экрана
        RandX = Mathf.Clamp(RandX, leftLimit, rightLimit);

        whereToSpawn = new Vector2(RandX, RandY);
        GameObject objInstance = Instantiate(obj, whereToSpawn, Quaternion.identity);
        objInstance.transform.localScale = new Vector3(RandScale, RandScale, 1.0f);


        // Добавляем Rigidbody2D и задаем случайную скорость для объекта
        Rigidbody2D rb2d = objInstance.GetComponent<Rigidbody2D>();
        if (rb2d != null)
        {
            // Генерируем случайную скорость в пределах [-2, 1] по обоим осям
            float randomSpeedX = Random.Range(-1.0f, 1f);
            float randomSpeedY = Random.Range(-1.0f, 1f);

            rb2d.velocity = new Vector2(randomSpeedX, randomSpeedY);

            // Вычисляем угол между вектором скорости и горизонтальной осью
            float angle = Vector2.Angle(Vector2.right, rb2d.velocity);

            Debug.Log("Угол падения: " + angle);
        }

        Destroy(objInstance, 4.0f);
    }
}

public class SpawnRateRange
{
    public int maxCount;
    public float spawnRate;

    public SpawnRateRange(int maxCount, float spawnRate)
    {
        this.maxCount = maxCount;
        this.spawnRate = spawnRate;
    }
}