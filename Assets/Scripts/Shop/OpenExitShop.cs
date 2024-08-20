using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenExitShop : MonoBehaviour
{
    public GameObject shopCanvas;

    private ShopManagerScript shopManager;
    private GameController gameController;
    private PlayerMovement playerMovement;

    void Start()
    {
        shopManager = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopManagerScript>();

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public void Exit()
    {
        shopCanvas.SetActive(false);
        // enable PlayerMovement
        playerMovement.enabled = true;
    }

    public void Open()
    {
        shopCanvas.SetActive(true);
        // disable PlayerMovement
        playerMovement.enabled = false;
        playerMovement.StopMovement();

        // set gold text of shop canvas
        shopManager.SetGoldText(gameController.GetGold());
    }
}
