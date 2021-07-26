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

    private void LoadCatalog()
    {
        PlayfabManager.Instance.GetCatalogItems(
            catalog =>
            {
                CatalogItems = catalog;
                ShowCatalog();
            }
            );
    }

    private void LoadStore()
    {
        PlayfabManager.Instance.GetStoreItems(
            store =>
            {
                StoreItems = store;
            });
    }

    private void LoadInventory()
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

}
