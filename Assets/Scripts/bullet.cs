using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float damage;
    public float range;
    public float speed;
    public float spread;
    //public Transform firePoint;

    private Rigidbody2D rb;
    void Start()
    {
        Destroy(gameObject, range);
        rb = GetComponent<Rigidbody2D>();    
        transform.Rotate(0, 0, Random.Range(-spread, spread), Space.Self);
        rb.velocity = transform.up * speed;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obejct") Destroy(this.gameObject);
    }
}
