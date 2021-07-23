using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipsManager : MonoBehaviour
{
    public GameObject LeftShipPrefab;
    public GameObject RightShipPrefab;

    public GameObject shipToCharge;

    public void CheckPurchase(int shipAmount)
    {
        int coinStars = PlayerPrefs.GetInt("Stored score"); //Almacenamos las estrellas acumuladas 
        if (coinStars >= shipAmount) //Comprobamos si tienes mas monedas o las mismas que vale la nave
        {
            coinStars -= shipAmount; //restamos las estrellas;
            PlayerPrefs.SetInt("Stored score", coinStars);
            ChargeShipInPrefab();
        }
        else
        {
            Debug.Log("No tienes estrellas suficientes.");
        }
    }

    public void ChargeShipInPrefab()
    {
        LeftShipPrefab.GetComponent<SpriteRenderer>().sprite = shipToCharge.GetComponent<Image>().sprite;
        RightShipPrefab.GetComponent<SpriteRenderer>().sprite = shipToCharge.GetComponent<Image>().sprite;
    }
}
