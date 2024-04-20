using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Purchasing;

public class DonateController : MonoBehaviour
{
    [SerializeField] Text moneyText;
    private int money;

    // Start is called before the first frame update
    void Start()
    {
        PurchaseManager.OnPurchaseNonConsumable += PurchaseManager_OnPurchaseNonConsumable;
        PurchaseManager.OnPurchaseConsumable += PurchaseManager_OnPurchaseConsumable;
        UpdateMoneyText(); // Переносим вызов функции обновления сразу в Start
    }

    private void PurchaseManager_OnPurchaseConsumable(PurchaseEventArgs args)
    {
        // Получаем ID приобретенного товара
        string productId = args.purchasedProduct.definition.id;

        if (productId == "special_noads")
        {
            money = PlayerPrefs.GetInt("MoneyScore");
            money += 2000; // Первый товар дает 2 000 монет
            PlayerPrefs.SetInt("MoneyScore", money); // Сохраняем измененное количество денег
            UpdateMoneyText();
            Debug.Log("AdBlock On"); // Шестой товар выводит текст в консоль
        }
    }

    private void PurchaseManager_OnPurchaseNonConsumable(PurchaseEventArgs args)
    {
        // Получаем ID приобретенного товара
        string productId = args.purchasedProduct.definition.id;

        if (productId == "little_money")
        {
            money = PlayerPrefs.GetInt("MoneyScore");
            money += 2000; // Первый товар дает 2 000 монет
            PlayerPrefs.SetInt("MoneyScore", money); // Сохраняем измененное количество денег
            UpdateMoneyText();
            Debug.Log("You purchase:" + args.purchasedProduct.definition.id + "- NonConsumable");
        }
        else if (productId == "medium_money")
        {
            money = PlayerPrefs.GetInt("MoneyScore");
            money += 5000; // Первый товар дает 5 000 монет
            PlayerPrefs.SetInt("MoneyScore", money); // Сохраняем измененное количество денег
            UpdateMoneyText();
            Debug.Log("You purchase:" + args.purchasedProduct.definition.id + "- NonConsumable");
        }
        else if (productId == "cart_money")
        {
            money = PlayerPrefs.GetInt("MoneyScore");
            money += 20000; // Первый товар дает 20 000 монет
            PlayerPrefs.SetInt("MoneyScore", money); // Сохраняем измененное количество денег
            UpdateMoneyText();
            Debug.Log("You purchase:" + args.purchasedProduct.definition.id + "- NonConsumable");
        }
        else if (productId == "carriage_money")
        {
            money = PlayerPrefs.GetInt("MoneyScore");
            money += 50000; // Первый товар дает 50 000 монет
            PlayerPrefs.SetInt("MoneyScore", money); // Сохраняем измененное количество денег
            UpdateMoneyText();
            Debug.Log("You purchase:" + args.purchasedProduct.definition.id + "- NonConsumable");
        }
        else if (productId == "magnate_money")
        {
            money = PlayerPrefs.GetInt("MoneyScore");
            money += 200000; // Первый товар дает 200 000 монет
            PlayerPrefs.SetInt("MoneyScore", money); // Сохраняем измененное количество денег
            UpdateMoneyText();
            Debug.Log("You purchase:" + args.purchasedProduct.definition.id + "- NonConsumable");
        }
        else if (productId == "donate_ads")
        {
            UpdateMoneyText();
            Debug.Log("AdBlock On" + args.purchasedProduct.definition.id + "- NonConsumable");
        }
        UpdateMoneyText();
        Debug.Log("You purchased: " + productId + " - Consumable");
    }

    void Update()
    {
        money = PlayerPrefs.GetInt("MoneyScore");

        if (moneyText != null)
        {
            moneyText.text = money.ToString();
        }
    }

    void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = money.ToString();
        }
    }
}
