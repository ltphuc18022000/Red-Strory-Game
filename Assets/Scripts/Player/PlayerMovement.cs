using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private GameController gameController;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private CircleCollider2D circleCollider;

    private float moveX;
    private float attk1;

    // Player Information
    public int attackDamage;
    public float speedRun;
    public float jumbHeight;
    public LayerMask groundLayer;

    private float attackClipTime;
    private bool canAttack = true;
    private float coolDownTimeAttack = 0.5f;
    private float coolDownTimer = Mathf.Infinity;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public Transform firepoint;
    public GameObject[] fireballs;
    public int timesFire { get; set; }

    // audio
    private GameObject playerAudioObject;
    private AudioSource audioFootStep;
    private AudioSource audioJump;
    private AudioSource audioAttack;
    private AudioSource audioRangeAttack;

    private AudioSource audioPlay;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        UpdateAnimClipTimes();

        // get player data
        PlayerData playerData = SaveSystem.loadPlayerData();
        if (playerData != null)
        {
            speedRun = playerData.speedRun;
            jumbHeight = playerData.jumbHeight;
            attackDamage = playerData.attackDamage;
        } 
        else
        {
            SceneManager.LoadScene(0);
        }

        // audio
        playerAudioObject = GameObject.FindGameObjectWithTag("PlayerAudioSource");
        audioFootStep = playerAudioObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        audioAttack = playerAudioObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>();
        audioJump = playerAudioObject.transform.GetChild(2).gameObject.GetComponent<AudioSource>();
        audioRangeAttack = playerAudioObject.transform.GetChild(3).gameObject.GetComponent<AudioSource>();

        audioPlay = audioFootStep;
    }

    private void FixedUpdate()
    {
        moveX = Input.GetAxis("Horizontal");
        attk1 = Input.GetAxis("Attack");
 

        //flip player
        if (moveX > 0)
            transform.localScale = Vector3.one;
        if (moveX < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // move
        rigidbody.velocity = new Vector2(moveX * speedRun, rigidbody.velocity.y);

        //Set animator parameters
        animator.SetFloat("speed", Mathf.Abs(moveX));
        animator.SetBool("isJumb", !isGrounded());
        animator.SetFloat("attack", Mathf.Abs(attk1));

        if (isGrounded())
        {
            if (moveX != 0)
                StartAudio(audioFootStep);

            if (Input.GetKey(KeyCode.Space))
                Jump();
        }

        // attack 
        if (attk1 > 0.5)
        {
            Attack();
        }

        if (Input.GetKey(KeyCode.F) && coolDownTimer> coolDownTimeAttack && timesFire>0)
        {
            rangeAttack();
            /*fireballs[0].transform.position = firepoint.position;
            Debug.Log(Mathf.Sign(transform.localScale.x));
            fireballs[0].GetComponent<FireBall>().SetDirection(Mathf.Sign(transform.localScale.x));*/
            timesFire -= 1;
            gameController.SetSkillsNumberText(timesFire);

        }
        coolDownTimer += Time.deltaTime;

        // thiết lập khi đang chạy bấm đánh sẽ ko bị lướt 
        if (attk1 > 0.01)
        {
            rigidbody.velocity = Vector2.zero;
        }
        /*if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Attack());
        }*/

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(circleCollider.bounds.center, circleCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private void rangeAttack()
    {
        animator.SetTrigger("rangeAttack");
        audioRangeAttack.Play(0);
        coolDownTimer = 0;
        StartCoroutine(InitFireBall());

    }

    IEnumerator InitFireBall()
    {
        yield return new WaitForSeconds(0.25f);
        fireballs[FindFireball()].transform.position = firepoint.position;
        fireballs[FindFireball()].GetComponent<FireBall>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Attack()
    {
        if (canAttack)
        {
            audioAttack.Play(0);
            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            foreach (Collider2D enemy in hitEnemy)
            {
                enemy.GetComponent<Enemy>().takeDame(attackDamage);
            }
            canAttack = false;
            StartCoroutine(WaitNextAttack());
        }
    }

    IEnumerator WaitNextAttack()
    {
        yield return new WaitForSeconds(attackClipTime);
        canAttack = true;
    }

    // thiết lập điểm sẽ gây sát thương khi đánh quái
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Jump()
    {
        StartAudio(audioJump);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumbHeight);
    }

    public void CustomJump(float jumbHeight)
    {
        StartAudio(audioJump);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumbHeight);
    }

    private void StartAudio(AudioSource audio)
    {
        if (audioPlay != audio)
        {
            audioPlay.Stop();
            audioPlay = audio;
            audioPlay.Play();
        }
        else
        {
            if (!audioPlay.isPlaying)
                audioPlay.Play();
        }
    }

    // get time of animetions clip
    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "attack":
                    attackClipTime = clip.length;
                    break;
            }
        }
    }

    public void StopMovement()
    {
        rigidbody.velocity = new Vector2(2, 0);
    }

}
