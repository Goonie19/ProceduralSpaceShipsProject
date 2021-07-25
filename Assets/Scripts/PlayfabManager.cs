﻿using PlayFab;
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

}
