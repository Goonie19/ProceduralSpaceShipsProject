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
            CatalogVersion = "mainstore"
        };

        PlayFabClientAPI.GetStoreItems(request,
            result =>
            {
                onSuccess(result.Store);
            },
            error =>
            {
                onError();
            }
            );
    }

    #endregion

}
