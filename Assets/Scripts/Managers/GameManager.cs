using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : Singleton<GameManager>
{

    public static event Action OnServerLogin;

    #region DATA

    public string GameVersion;
    public EconomyModel ServerEconomy;

    #endregion

    private void Start()
    {
        ServerLogin();
    }

    private void Awake()
    {
        OnServerLogin += LoadServerData;
    }
    private void OnDestroy()
    {
        OnServerLogin -= LoadServerData;
    }

    #region LOGIN

    private void ServerLogin()
    {

        PlayfabManager.Instance.Login(
            resultadoLogin =>
            {
                Debug.Log("User login: " + resultadoLogin.PlayFabId);
                Debug.Log("User newly created: " + resultadoLogin.NewlyCreated);

                OnServerLogin?.Invoke();
            },
            resultadoError =>
            {
                Debug.Log("Login failed: " + resultadoError.ErrorMessage);

            }
            );

    }



    #endregion

    #region LOAD SERVER DATA

    public void LoadServerData()
    {
        PlayfabManager.Instance.GetTitleData(
            titleData =>
            {
                LoadGameSetup(titleData.Data);
            },
            error =>
            {
                Debug.Log("Get Title Data failed: " + error.ErrorMessage);
            }
            );
    }

    private void LoadGameSetup(Dictionary<string, string> data)
    {
        SetPlayfabVersion(data["ClientVersion"]);
        SetPlayfabEconomyModel(data["EconomySetup"]);
    }

    private void SetPlayfabVersion(string version)
    {
        GameVersion = version;
    }

    private void SetPlayfabEconomyModel(string economyJson)
    {
        JsonUtility.FromJsonOverwrite(economyJson, ServerEconomy);
    }

    #endregion

    #region UPDATE SERVER DATA

    public void UpdateStarsOfServer(int amount) {
        PlayfabManager.Instance.UpdateStars(amount, stars =>
        {
            Debug.Log("Se actualizo con exito");
        });
    }

    #endregion

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
        Time.timeScale = 1f;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    
}
