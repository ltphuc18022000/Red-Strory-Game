using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public Text Price;
    public GameObject ShopManager;

    private void FixedUpdate()
    {
        Price.text = ShopManager.GetComponent<ShopManagerScript>().shopItems[2, ItemID].ToString();
    }
}