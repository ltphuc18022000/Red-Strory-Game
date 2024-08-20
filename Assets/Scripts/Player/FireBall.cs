using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireBall : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;
    private int fireDame;

    private Animator anim;
    private BoxCollider2D boxCollider;
    public LayerMask enemyLayer;
    private void Awake()
    {
        // The higher the level, the higher the fire dame; 20% per level
        fireDame = (int)(50 * (1 + (SceneManager.GetActiveScene().buildIndex - 2) * 0.2));

        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
            enemy.takeDame(fireDame);

        boxCollider.enabled = false;
        anim.SetTrigger("explode");
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
