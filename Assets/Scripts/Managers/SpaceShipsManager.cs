using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpaceShipsManager : MonoBehaviour
{
    public GameObject LeftShipPrefab;
    public GameObject RightShipPrefab;

    public string shipID;

    public GameObject shipToCharge;

    private bool isPurchased = false;
    private bool isInInventory = false;

    private void Update()
    {
        if (ShopManager.Instance.SearchInventory(shipID) && !isInInventory)
        {
            Debug.Log("Lo tengo comprado");
            isInInventory = true;
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
            gameObject.GetComponentsInChildren<Image>()[1].color = new Color(0f, 0f, 0f, 0f);
        }
    }

    public void CheckPurchase(int shipAmount)
    {
        if (!isPurchased)
        {
            //int coinStars = PlayerPrefs.GetInt("Stored score"); //Almacenamos las estrellas acumuladas
            int coinStars = ShopManager.Instance.Stars;
            if (coinStars >= shipAmount) //Comprobamos si tienes mas monedas o las mismas que vale la nave
            {

                GetID();
                ShipPurchased();
            }
            else
            {
                Debug.Log("No tienes estrellas suficientes.");
            }
        }
        else
        {
            ChargeShipInPrefab();
        }
    }

    public void ChargeShipInPrefab()
    {
        LeftShipPrefab.GetComponent<SpriteRenderer>().sprite = shipToCharge.GetComponent<Image>().sprite;
        RightShipPrefab.GetComponent<SpriteRenderer>().sprite = shipToCharge.GetComponent<Image>().sprite;
    }

    private void ShipPurchased()
    {
        isPurchased = true;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
        gameObject.GetComponentsInChildren<Image>()[1].color = new Color(0f, 0f, 0f, 0f);
        ChargeShipInPrefab();
    }

    private void GetID()
    {
        ShopManager.Instance.PurchaseItem(ShopManager.Instance.SearchID(shipID),
            result =>
            {
                Debug.Log("Funcionó");
            });
        ShopManager.Instance.LoadCatalog();
        ShopManager.Instance.LoadInventory();
        ShopManager.Instance.LoadStore();
    }

}
