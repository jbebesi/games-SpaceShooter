using Assets.scripts.BaseClasses;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : ShipBase
{
    float actHealth = 100;
    [SerializeField] float health = 100;
    [SerializeField] float minTimeBetweenShoots = 3;
    [SerializeField] float maxTimeBetweenShotts = 10f;
    [SerializeField] GameObject laser;
    [SerializeField] float laserSpeed = 6;
    [SerializeField] AudioClip DeathSound;
    [SerializeField] GameObject DeathEffect;
    [SerializeField] float DurationOfExplosion = 10f;
    [SerializeField] [Range(0, 1)] float deathSundVolume = 0.7f;
    public static int Count=0;

    float timeUntilFire = 0;

    // Start is called before the first frame update
    void Start()
    {
        actHealth = health;
        timeUntilFire = minTimeBetweenShoots;
        Count++;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }


    private void Fire()
    {
        timeUntilFire -= Time.deltaTime;
        if (timeUntilFire < 0)
        {
            var l = Instantiate(
                laser,
                transform.position,
                Quaternion.identity);
            l.GetComponent<Rigidbody2D>().
            velocity = new Vector2(0, -laserSpeed);
            timeUntilFire = Random.Range(minTimeBetweenShoots, maxTimeBetweenShotts);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        if (damageDealer != null)
        {
            actHealth -= damageDealer.GetDamage;
            if (actHealth <= 0)
            {
                FindObjectOfType<GameSession>().AddScore((int)health);
                Count--;
                Destroy(gameObject);
                var obj = Instantiate(
                    DeathEffect,
                    gameObject.transform.position,
                    Quaternion.identity);
                Destroy(obj, DurationOfExplosion);
                
            }
        }
    }
}
