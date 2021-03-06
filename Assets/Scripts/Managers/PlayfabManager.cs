using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class PlayfabManager : Singleton<PlayfabManager>
{

    [SerializeField] private bool _isTest = true;

    public static string PLAYFAB_TITLE_TEST = "21F3A";
    public static string PLAYFAB_TITLE_RELEASE = "B327B";


    private void Awake()
    {
        SetupPlayfabServer();
    }

    private void SetupPlayfabServer()
    {
        PlayFabSettings.TitleId = (_isTest ? PLAYFAB_TITLE_TEST : PLAYFAB_TITLE_RELEASE);
    }

    #region LOGIN

    public void Login(Action<LoginResult> onSuccess, Action<PlayFabError> onFail)
    {
        var request = new LoginWithCustomIDRequest
        {
            CreateAccount = true,
            CustomId = SystemInfo.deviceUniqueIdentifier
        };

        PlayFabClientAPI.LoginWithCustomID(request, onSuccess, onFail);
    }

    #endregion

    #region TITLE DATA

    public void GetTitleData(Action<GetTitleDataResult> onSuccess, Action<PlayFabError> onError)
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), onSuccess, onError);
    }

    #endregion

    #region CATALOG

    public void GetCatalogItems(Action<List<CatalogItem>> onSuccess, Action onError = null)
    {
        var request = new GetCatalogItemsRequest()
        {
            CatalogVersion = "SpaceShipShop"
        };

        PlayFabClientAPI.GetCatalogItems(request,
            result =>
            {
                onSuccess(result.Catalog);
            },
            error =>
            {
                onError();
            }
            );
    }

    #endregion

    #region STORE

    public void GetStoreItems(Action<List<StoreItem>> onSuccess, Action onError = null)
    {
        var request = new GetStoreItemsRequest()
        {
            StoreId = "mainstore",
            
        };

        PlayFabClientAPI.GetStoreItems(request,
            result =>
            {
                onSuccess(result.Store);
            },
            error =>
            {
                onError?.Invoke();
            }
            );
    }

    #endregion

    #region INVENTORY

    public void GetInventory(Action<GetUserInventoryResult> onSuccess, Action onError = null) 
    {
        var request = new GetUserInventoryRequest();

        PlayFabClientAPI.GetUserInventory(request,
            result =>
            {
                ShopManager.Instance.Stars = result.VirtualCurrency["JA"];
                onSuccess(result);
            },
            error =>
            {
                onError?.Invoke();
            }
            );
    } 

    #endregion

    public void UpdateStars(int amount, Action<ModifyUserVirtualCurrencyResult> onSuccess, Action<PlayFabError> onError = null)
    {
        var request = new AddUserVirtualCurrencyRequest()
        {
            Amount = amount,
            VirtualCurrency = "JA"
        };

        PlayFabClientAPI.AddUserVirtualCurrency(request, 
            result =>
            {
                ShopManager.Instance.Stars += amount;
                onSuccess(result);
            },
            error =>
            {
                Debug.Log("Not added stars");
                onError(error);
            }
            );
    }

    public void UpdateStars2(int amount, Action<ModifyUserVirtualCurrencyResult> onSuccess, Action<PlayFabError> onError = null)
    {
        var request = new SubtractUserVirtualCurrencyRequest()
        {
            Amount = amount,
            VirtualCurrency = "JA"
        };

        PlayFabClientAPI.SubtractUserVirtualCurrency(request,
            result =>
            {
                ShopManager.Instance.Stars -= amount;
                onSuccess(result);
            },
            error =>
            {
                Debug.Log("Not added stars");
                onError(error);
            }
            );
    }

}
