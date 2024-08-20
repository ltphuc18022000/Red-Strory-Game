using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Heartbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
   [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image curentHealtBar;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>(); // lấy giá trị máu hiện tại của player để mỗi khi chuyển cảnh ko cần sử dụng dontdestroy
        totalHealthBar.fillAmount = playerHealth.currentHealth / 10;
        
    }

    // Update is called once per frame
    void Update()
    {
        curentHealtBar.fillAmount = playerHealth.currentHealth / 10;
    }

}
