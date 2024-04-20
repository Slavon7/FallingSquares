using System;
using UnityEngine;
using UnityEngine.Purchasing;
 
public class PurchaseManager : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;
    private int currentProductIndex;
 
    [Tooltip("Не многоразовые товары. Больше подходит для отключения рекламы и т.п.")]
    public string[] NC_PRODUCTS;
    [Tooltip("Многоразовые товары. Больше подходит для покупки игровой валюты и т.п.")]
    public string[] C_PRODUCTS;
 
    /// <summary>
    /// Событие, которое запускается при удачной покупке многоразового товара.
    /// </summary>
    public static event OnSuccessConsumable OnPurchaseConsumable;
    /// <summary>
    /// Событие, которое запускается при удачной покупке не многоразового товара.
    /// </summary>
    public static event OnSuccessNonConsumable OnPurchaseNonConsumable;
    /// <summary>
    /// Событие, которое запускается при неудачной покупке какого-либо товара.
    /// </summary>
    public static event OnFailedPurchase PurchaseFailed;
 
    private void Awake()
    {
        InitializePurchasing();
    }
    /// <summary>
    /// Проверить, куплен ли товар.
    /// </summary>
    /// <param name="id">Индекс товара в списке.</param>
    /// <returns></returns>
    public static bool CheckBuyState(string id)
    {
        Product product = m_StoreController.products.WithID(id);
        if (product.hasReceipt) { return true; }
        else { return false; }
    }
 
    public void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        foreach (string s in C_PRODUCTS) builder.AddProduct(s, ProductType.Consumable);
        foreach (string s in NC_PRODUCTS) builder.AddProduct(s, ProductType.NonConsumable);
        UnityPurchasing.Initialize(this, builder);
    }
 
    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }
 
    public void BuyConsumable(int index)
    {
        currentProductIndex = index;
        if (index >= 0 && index < C_PRODUCTS.Length)
        {
            BuyProductID(C_PRODUCTS[index]);
        }
    }

    public void BuyNonConsumable(int index)
    {
        currentProductIndex = index;
        if (index >= 0 && index < NC_PRODUCTS.Length)
        {
            BuyProductID(NC_PRODUCTS[index]);
        }
    }
 
    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                OnPurchaseFailed(product, PurchaseFailureReason.ProductUnavailable);
            }
        }
    }
 
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
 
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }
 
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }
 
    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error + ", Message: " + message);
    }
 
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        string purchasedProductId = args.purchasedProduct.definition.id;

        for (int i = 0; i < C_PRODUCTS.Length; i++)
        {
            if (String.Equals(purchasedProductId, C_PRODUCTS[i], StringComparison.Ordinal))
            {
                OnSuccessC(args);
                return PurchaseProcessingResult.Complete;
            }
        }

        for (int i = 0; i < NC_PRODUCTS.Length; i++)
        {
            if (String.Equals(purchasedProductId, NC_PRODUCTS[i], StringComparison.Ordinal))
            {
                OnSuccessNC(args);
                return PurchaseProcessingResult.Complete;
            }
        }

        Debug.Log($"ProcessPurchase: FAIL. Unrecognized product: '{purchasedProductId}'");
        return PurchaseProcessingResult.Complete;
    }
    public delegate void OnSuccessConsumable(PurchaseEventArgs args);
    protected virtual void OnSuccessC(PurchaseEventArgs args)
    {
        if (OnPurchaseConsumable != null) OnPurchaseConsumable(args);
        Debug.Log(C_PRODUCTS[currentProductIndex] + " Buyed!");
    }
 
    public delegate void OnSuccessNonConsumable(PurchaseEventArgs args);
    protected virtual void OnSuccessNC(PurchaseEventArgs args)
    {
        if (OnPurchaseNonConsumable != null) OnPurchaseNonConsumable(args);
        Debug.Log(NC_PRODUCTS[currentProductIndex] + " Buyed!");
    }
 
    public delegate void OnFailedPurchase(Product product, PurchaseFailureReason failureReason);
    protected virtual void OnFailedP(Product product, PurchaseFailureReason failureReason)
    {
        if (PurchaseFailed != null) PurchaseFailed(product,failureReason);
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
 
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        OnFailedP(product,failureReason);
    }
}
