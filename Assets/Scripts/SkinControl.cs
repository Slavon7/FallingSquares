using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinControl : MonoBehaviour
{
    public int skinNum;
    public Button buyButton;
    public Image iLock;
    public int price;

    public Sprite buySkin;
    public Sprite equipped;
    public Sprite equip;
    public Sprite falseLock;
    public Sprite trueLock;

    public Image[] skins;

    // Start is called before the first frame update
    void Start()
    {
         if (PlayerPrefs.GetInt("skin1" + "buy") == 0)
        {
            foreach (Image img in skins)
            {
                if ("skin1" == img.name)
                {
                    PlayerPrefs.SetInt("skin1" + "buy", 1);
                    PlayerPrefs.SetInt("skin1" + "equip", 1);
                }
                else
                {
                    PlayerPrefs.SetInt(GetComponent<Image>().name + "buy", 0);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
          if (PlayerPrefs.GetInt(GetComponent<Image>().name + "buy") == 0)
        {
            iLock.GetComponent<Image>().sprite = falseLock;
            buyButton.GetComponent<Image>().sprite = buySkin;
        }
        else if(PlayerPrefs.GetInt(GetComponent<Image>().name + "buy") == 1)
        {
            iLock.GetComponent<Image>().sprite = trueLock;
            if (PlayerPrefs.GetInt(GetComponent<Image>().name + "equip") == 1)
            {
                buyButton.GetComponent<Image>().sprite = equipped;
            }
            else if (PlayerPrefs.GetInt(GetComponent<Image>().name + "equip") == 0)
            {
                buyButton.GetComponent<Image>().sprite = equip;
            }
        }
    }
     public void buy()
    {
        if(PlayerPrefs.GetInt(GetComponent<Image>().name + "buy") == 0) {
            if (Money.money >= price)
            {
                iLock.GetComponent<Image>().sprite = trueLock;
                buyButton.GetComponent<Image>().sprite = equipped;
                PlayerPrefs.SetInt("MoneyScore", PlayerPrefs.GetInt("MoneyScore") - price);
                PlayerPrefs.SetInt(GetComponent<Image>().name + "buy", 1);
                PlayerPrefs.SetInt("skinNum", skinNum);

                foreach(Image img in skins)
                {
                    if (GetComponent<Image>().name == img.name)
                    {
                        PlayerPrefs.SetInt(GetComponent<Image>().name + "equip", 1);
                    }
                    else
                    {
                        PlayerPrefs.SetInt(img.name + "equip", 0);
                    }
                } 
            }
        }
        else if (PlayerPrefs.GetInt(GetComponent<Image>().name + "buy") == 1)
        {
            iLock.GetComponent<Image>().sprite = trueLock;
            buyButton.GetComponent<Image>().sprite = equipped;
            PlayerPrefs.SetInt(GetComponent<Image>().name + "equip", 1);
            PlayerPrefs.SetInt("skinNum", skinNum);

            foreach (Image img in skins)
            {
                if (GetComponent<Image>().name == img.name)
                {
                    PlayerPrefs.SetInt(GetComponent<Image>().name + "equip", 1);
                }
                else
                {
                    PlayerPrefs.SetInt(img.name + "equip", 0);
                }
            }
        }
    }
}
