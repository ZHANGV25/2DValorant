using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxHealth = 100f;
    //[SerializeField] private FieldOfView fv;
    [SerializeField] private GameObject[] attacks;
    //[SerializeField] private Transform firePoint;
    //[SerializeField] private GameObject ball;

    private Vector2 movement;
    private float health;

    private Rigidbody2D rb;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) Destroy(this);
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        health = maxHealth;
        //cam = Camera.main;
        //fv.SetFoV(360);
        //fv.SetViewDistance(7);
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(
        mousePosition.x - transform.position.x,
        mousePosition.y - transform.position.y
        );

        transform.up = direction;

        if (movement.x != 0 || movement.y != 0)
        {
            gameObject.GetComponent<Animator>().SetBool("isRunning", true);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("isRunning", false);
        }
        //fv.SetOrigin(transform.position);
        //fv.SetAimDirection(direction);
    }

    void FixedUpdate()
    {
        //rb.velocity += movement.normalized * (moveSpeed * Time.deltaTime);
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        rb.velocity = Vector2.zero;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        CheckIfDead();
    }

    void CheckIfDead()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
    }
    /*void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            SceneManager.LoadScene(0);
        }
        if (collision.tag == "End")
        {
            SceneManager.LoadScene(1);
        }
    }*/
}
