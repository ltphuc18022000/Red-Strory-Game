using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public bool isBoss = false;
    public int maxhealth;
    public int currenthealth;
    public Slider slider;

    // hiệu ứng rớt tiền
    public GameObject rewardDrop;

    private Animator anim;
    private float dieClipTime;

    // destoy all enemy include patrol
    public GameObject enemyNeedToDestroy;

    private GameController gameController;
    private GameObject winObjects;

    void Start()
    {
        // qua mỗi level thì máu quái + 25
        if (!isBoss)
            maxhealth = 50 + (SceneManager.GetActiveScene().buildIndex - 2) * 25;
        // thiết lập máu cơ bản
        currenthealth = maxhealth;
        slider.value = currenthealth;
        slider.maxValue = maxhealth;
        // kết thúc thiết lập máu

        anim = GetComponent<Animator>();
        UpdateAnimClipTimes();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void takeDame(int damage)
    {
        currenthealth -= damage;
        slider.value = currenthealth;
        anim.SetTrigger("hurt");
        if (currenthealth <= 0)
        {
            anim.SetTrigger("die");

            StartCoroutine(Dead());

        }
    }
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(dieClipTime);
        // active WinObjects if boss die
        if (isBoss)
        {
            winObjects = GameObject.FindGameObjectWithTag("WinObjects");
            for (int a = 0; a < winObjects.transform.childCount; a++)
            {
                winObjects.transform.GetChild(a).gameObject.SetActive(true);
            }
        }
        Destroy(this.gameObject.transform.parent.gameObject);
        // code rớt vàng
        Instantiate(rewardDrop, transform.position, transform.rotation);
    }

    // get time of animetions clip
    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name.ToLower().Contains("die"))
                dieClipTime = clip.length;
        }
    }
}
