using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private FieldOfView fv;
    [SerializeField] private GameObject[] attacks;
    //[SerializeField] private Transform firePoint;
    //[SerializeField] private GameObject ball;

    private Vector2 movement;

    private Rigidbody2D rb;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        //cam = Camera.main;
        fv.SetFoV(360);
        fv.SetViewDistance(7);
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
        fv.SetOrigin(transform.position);
        fv.SetAimDirection(direction);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        rb.velocity = Vector2.zero;
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
