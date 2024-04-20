using System.Collections;
using UnityEngine;

public class takeShield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("PowerUp picked up by player.");
            Heartsystem heartsSystem = collision.GetComponent<Heartsystem>();
            if (heartsSystem != null)
            {
                // Генерируем случайное число от 0 до 1
                float randomValue = Random.value;

                // Вероятность активации щита (например, 50%)
                float shieldActivationChance = 0.5f;

                if (randomValue < shieldActivationChance)
                {
                    heartsSystem.ActivateShield(); // Активация щита при поднятии
                }
                else
                {
                    heartsSystem.ActivateSpeed(); // Активация увеличения скорости при поднятии
                }
            }
            Destroy(gameObject);
        }
    }
}