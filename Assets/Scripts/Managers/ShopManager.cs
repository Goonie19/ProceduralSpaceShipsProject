using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ShopManager : Singleton<ShopManager>
{

    public List<CatalogItem> CatalogItems;
    public List<StoreItem> StoreItems;
    public List<ItemInstance> Inventory;
    public int Stars;


    private void Awake()
    {
        GameManager.OnServerLogin += LoadCatalog;
        GameManager.OnServerLogin += LoadStore;
        GameManager.OnServerLogin += LoadInventory;


    }

    private void OnDestroy()
    {
        GameManager.OnServerLogin -= LoadCatalog;
        GameManager.OnServerLogin -= LoadStore;
        GameManager.OnServerLogin -= LoadInventory;

    }

    public void LoadCatalog()
    {
        PlayfabManager.Instance.GetCatalogItems(
            catalog =>
            {
                CatalogItems = catalog;
                ShowCatalog();
            }
            );
    }

    public void LoadStore()
    {
        PlayfabManager.Instance.GetStoreItems(
            store =>
            {
                StoreItems = store;
            });
    }

    public void LoadInventory()
    {
        PlayfabManager.Instance.GetInventory(
            inventory =>
            {
                Inventory = inventory.Inventory;
                Stars = inventory.VirtualCurrency["JA"];
            }
            );
    }

    private void ShowCatalog()
    {
        foreach (CatalogItem item in CatalogItems)
            Debug.Log("[Catalog]: " + item.DisplayName);
    }

    public void PurchaseItem(StoreItem item, Action<List<ItemInstance>> onSuccess, Action onError = null)
    {
        var request = new PurchaseItemRequest()
        {
            CatalogVersion = "SpaceShipShop",
            StoreId = "mainstore",
            VirtualCurrency = "JA",
            Price = (int)item.VirtualCurrencyPrices["JA"],
            ItemId = item.ItemId
        };

        PlayFabClientAPI.PurchaseItem(request,
            (result) =>
            {
                onSuccess(result.Items);
            },
            (error) =>
            {
                onError?.Invoke();
            });
    }

    public StoreItem SearchID(string ID)
    {
        int i = 0;
        StoreItem item = null;

        while (ID != StoreItems[i].ItemId && i<StoreItems.Count) {
            if(ID == StoreItems[i].ItemId)
            {
                item = StoreItems[i];
            }

            i++;
        }

        if (ID == StoreItems[i].ItemId)
        {
            item = StoreItems[i];
        }

        return item;
    }

    public bool SearchInventory(string ID)
    {
        bool isPurchased = false;
        int i = 0;

        Debug.Log("Inventory count: " + Inventory.Count);
        Debug.Log("ID: " + ID);

        while (i < Inventory.Count && !isPurchased)
        {
            
            Debug.Log("Inventory: " + Inventory[i].ItemId);
            if (ID == Inventory[i].ItemId)
            {
                isPurchased = true;
            }
            Debug.Log(isPurchased);
            i++;
        }

        return isPurchased;
    }

}
