using System.Collections.Generic;
using UnityEngine;

public class AssignSkin : MonoBehaviour
{
    public Sprite[] skins;
    public GameObject ball;
    public Sprite standart;
    
    void Start()
    {
        int skinNum = PlayerPrefs.GetInt("skinNum", 0); // Значение по умолчанию 1

        if (skinNum >= 1 && skinNum <= skins.Length)
        {
            ball.GetComponent<SpriteRenderer>().sprite = skins[skinNum - 1];
        }
        else
        {
            // Номер скина недопустим, используем стандартный спрайт
            ball.GetComponent<SpriteRenderer>().sprite = standart;
        }
    }
}
