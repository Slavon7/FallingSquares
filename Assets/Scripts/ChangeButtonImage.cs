using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonImage : MonoBehaviour
{
    public Sprite normalSprite; // Спрайт для обычного состояния кнопки
    public Sprite pressedSprite; // Спрайт для нажатого состояния кнопки

    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    public void OnPointerDown()
    {
        // Измените спрайт на нажатый спрайт
        buttonImage.sprite = pressedSprite;
    }

    public void OnPointerUp()
    {
        // Измените спрайт на обычный спрайт
        buttonImage.sprite = normalSprite;
    }
}
