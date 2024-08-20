using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[5, 5];
    public Text Coins;
    private int gold;

    private GameController gameController;
    private Health playerHealth;
    private PlayerMovement playerMovement;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        //ID
        shopItems[1, 1] = 1; // HP
        shopItems[1, 2] = 2; // Power
        shopItems[1, 3] = 3; // Speed

        //Price
        shopItems[2, 1] = 150;
        shopItems[2, 2] = 200;
        shopItems[2, 3] = 150;

        //Buy times
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;

    }

    public void SetGoldText(int g)
    {
        Coins.text = "Gold: " + g.ToString();
    }

    private void Payment(int price)
    {
        gold -= price;
        gameController.SubtractGold(price);
        SetGoldText(gold);
    }

    public void Buy()
    {
        // gọi ở đây để mỗi lần bấm buy sẽ lấy được trạng thái mới nhất của gold, health, dame, speed 
        // nếu gọi ở start chỉ lấy được trạng thái đầu tiên khi start game ( ko lấy được các trạng thái sau khi điều chỉnh
        
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        gold = gameController.GetGold();
        SetGoldText(gold);
        int itemPrice = shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
        if (gold >= itemPrice)
        {

            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;
            // thằng này để hiện text vì đã xóa text đó nên comment lại
            //ButtonRef.GetComponent<ButtonInfo>().Buy.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();

            // nâng cấp máu
            if (ButtonRef.GetComponent<ButtonInfo>().ItemID == 1)
            {
                if (SaveSystem.playerData.health < 10)
                {
                    Payment(itemPrice);

                    // cộng chỉ số cho nhân vật
                    SaveSystem.playerData.health += 1;
                    playerHealth.startingHealth += 1;
                    playerHealth.currentHealth += 1;
                }
                else
                {
                    Debug.Log("ban da mua toi da mau");
                }

            }

            // nâng cấp sức mạnh
            if (ButtonRef.GetComponent<ButtonInfo>().ItemID == 2)
            {
                if (SaveSystem.playerData.attackDamage < 100)
                {
                    Payment(itemPrice);

                    // cộng chỉ số cho nhân vật
                    SaveSystem.playerData.attackDamage += 10;
                    playerMovement.attackDamage += 10;
                }
                else
                {
                    Debug.Log("da mua max dame");
                }
         
            }

            // nâng cấp tốc độ di chuyển
            if (ButtonRef.GetComponent<ButtonInfo>().ItemID == 3)
            {
                if(SaveSystem.playerData.speedRun < 15)
                {
                    Payment(itemPrice);

                    // cộng chỉ số cho nhân vật
                    SaveSystem.playerData.speedRun += 0.5f;
                    playerMovement.speedRun += 0.5f;
                }
                else
                {
                    Debug.Log("da mua het so luong ");
                }
            }
        }
        

    }
}
