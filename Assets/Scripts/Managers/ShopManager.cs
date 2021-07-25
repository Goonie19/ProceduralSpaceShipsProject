using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public List<CatalogItem> CatalogItems;
    public List<StoreItem> StoreItems;


    private void Awake()
    {
        GameManager.OnServerLogin += LoadCatalog;
        GameManager.OnServerLogin += LoadStore;

    }

    private void OnDestroy()
    {
        GameManager.OnServerLogin -= LoadCatalog;
        GameManager.OnServerLogin -= LoadStore;

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

    private void ShowCatalog()
    {
        foreach (CatalogItem item in CatalogItems)
            Debug.Log("[Catalog]: " + item.DisplayName);
    }

}
