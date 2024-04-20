using System.Collections;
using UnityEngine;

public class MaskingObject : MonoBehaviour
{
    public float duration = 5.0f; // Время, за которое маска будет уменьшаться
    private RectTransform maskRectTransform;

    private void Start()
    {
        maskRectTransform = GetComponent<RectTransform>();
        StartCoroutine(ShrinkMask());
    }

    private IEnumerator ShrinkMask()
    {
        float startTime = Time.time;
        Vector2 originalSize = maskRectTransform.sizeDelta;
        Vector2 targetSize = new Vector2(0, originalSize.y);

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            maskRectTransform.sizeDelta = Vector2.Lerp(originalSize, targetSize, t);
            yield return null;
        }

        // Убедитесь, что маска полностью скрыта
        maskRectTransform.sizeDelta = targetSize;
    }
}
