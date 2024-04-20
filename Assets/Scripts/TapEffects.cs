using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapEffects : MonoBehaviour
{
    public GameObject TapEffect;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickEffector(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    public void ClickEffector(Vector2 position)
    {
        GameObject effectInstance = Instantiate(TapEffect, position, Quaternion.identity, transform);

        // Запускаем корутину для удаления объекта через 2 секунды
        StartCoroutine(DestroyEffectAfterDelay(effectInstance, 1.0f));
    }

    private IEnumerator DestroyEffectAfterDelay(GameObject effect, float delay)
    {
        // Ждем заданное количество секунд
        yield return new WaitForSeconds(delay);

        // Удаляем объект
        Destroy(effect);
    }
}
