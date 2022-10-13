using Assets.scripts.BaseClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ShipBase
{
    [Header("Player")]
    [SerializeField] float Speed = 1;
    [SerializeField] int Health = 700;
    [Header("Projectile")]
    [SerializeField] GameObject Laser;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    

    float padding = 1f;
    [SerializeField]float MaxX = 5.4f;
    [SerializeField]float MinX = -5.4f;
    [SerializeField]float MaxY = 5.4f;
    [SerializeField]float MinY = -5.4f;
    Coroutine fireCorutine;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    internal int GetHealth() => Health;

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }



    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            fireCorutine = StartCoroutine(ContiniousFire());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCorutine);
        }
    }

    private IEnumerator ContiniousFire()
    {
        while (true)
        {
            GameObject laser = Instantiate(Laser, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }


    private void Move()
    {

        var deltaX = Input.GetAxis("Horizontal");
        var newXPos = transform.position.x + (deltaX *Time.deltaTime*Speed);
        newXPos = Mathf.Clamp(newXPos, MinX, MaxX);

        var deltaY = Input.GetAxis("Vertical");
        var newYPos = transform.position.y + (deltaY * Time.deltaTime *Speed);
        newYPos = Mathf.Clamp(newYPos, MinY, MaxY);

        transform.position = new Vector2(newXPos, newYPos);
    }

    void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        MinX = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + padding;
        MaxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x-padding;
        MinY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y+padding;
        MaxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y-padding;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var damage = other.gameObject.GetComponent<DamageDealer>();
        CheckHealth(damage);
    }

    private void CheckHealth(DamageDealer damage)
    {
        Debug.Log("Damaged");
        Health -= damage?.GetDamage ?? 0;
        if (Health <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<Level>().LoadGameOver();
        }
    }

}
