using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireRate;
    [SerializeField] private float reloadSpeed;
    [SerializeField] private float fireSpread;
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    private float timeUntilShot;

    void Start()
    {
        timeUntilShot = fireRate;   
    }
    void Update()
    {
        timeUntilShot -= Time.deltaTime;
        if (timeUntilShot < 0 && Input.GetMouseButton(0))
        {
            Shoot();
            timeUntilShot = fireRate;
        }
    }
    void Shoot()
    {
        GameObject _bullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        _bullet.GetComponent<bullet>().damage = damage;
        _bullet.GetComponent<bullet>().range = range;
        _bullet.GetComponent<bullet>().speed = speed;
        _bullet.GetComponent<bullet>().spread = fireSpread;
        //_bullet.GetComponent<Rigidbody2D>().velocity = firePoint.up * speed;
        //_bullet.GetComponent<bullet>().firePoint = firePoint;


    }
}

