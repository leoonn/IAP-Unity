using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;   

public class IAP : MonoBehaviour, IStoreListener
{
    private static IStoreController storeController;
    private static IExtensionProvider StoreExtension;
    public static string Coins100 = "coins100";
    public static string Coins500 = "coins500";
    public static string Coins1000 = "coins1000";
    [header("References")]
    CoinsManager coinsScript; //Script manipulation of coins, the repository was made
    SoundsMenu soundmenu; //Script for feedback to player

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions) //initialize
    {
        storeController = controller;
        StoreExtension = extensions; 
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("failed: " + error); //do something when will have a error 
    }

    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log("Failed to buy" + p); //do something when will have a error in purchased fail
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        //Coins100 must be equals Coins100 string value
        if (string.Equals(args.purchasedProduct.definition.id, Coins100, StringComparison.Ordinal)) //purchase done 
        {
            coinsScript.coins += 100; //coins purchased for player
            coinsScript.SetCoins(); //methods to add coins the player
            coinsScript.GetCoins();
            soundmenu.AudioCoins(); //feedback sound not needed this script 
        }
        else
        {
            Debug.Log("fail");
        }
        //Coins500 must be equals Coins500 string value
        if (string.Equals(args.purchasedProduct.definition.id, Coins500, StringComparison.Ordinal))
        {
            coinsScript.coins += 500; //coins purchased for player
            coinsScript.SetCoins(); //methods to add coins the player
            coinsScript.GetCoins();
            soundmenu.AudioCoins(); //feedback sound not needed this script 
        }
        else
        {
            Debug.Log("fail");
        }
         //Coins1000 must be equals Coins1000 string value
        if (string.Equals(args.purchasedProduct.definition.id, Coins1000, StringComparison.Ordinal))
        {
            coinsScript.coins += 1000; //coins purchased for player
            coinsScript.SetCoins(); //methods to add coins the player
            coinsScript.GetCoins();
            soundmenu.AudioCoins(); //feedback sound not needed this script 
        }
        else
        {
            Debug.Log("fail");
        }

        return PurchaseProcessingResult.Complete; 
    }

    private bool IsInitialized() // check if is initialize 
    {
        return storeController != null && StoreExtension != null;
    }

    public void InitializePurchase() //start purchase script
    {
        if (IsInitialized())
        {
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(Coins100, ProductType.Consumable); //string and type of item that is purchased
        builder.AddProduct(Coins500, ProductType.Consumable);
        builder.AddProduct(Coins1000, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }

    // Start is called before the first frame update
    void Start()
    {
        soundmenu = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundsMenu>();
        coinsScript = GameObject.Find("CoinsManager").GetComponent<CoinsManager>();
        if (storeController == null)
        {
            InitializePurchase();   
        }
    }
   
    public void BuyProduct(string productID) //method to be called in UI button of product that is being purchased 
    {
        if (IsInitialized())
        {
            Product product = storeController.products.WithID(productID);
            if (product != null && product.availableToPurchase)
            {
                storeController.InitiatePurchase(product); //open screen google play purchase
                soundmenu.AudioSelect();
            }
            else
            {
                Debug.Log("fail in operation");
            }
        }
        else
        {
            Debug.Log("fail initialize");
        }
    }
   
}
