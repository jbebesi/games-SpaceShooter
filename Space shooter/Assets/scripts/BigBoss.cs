using Assets.scripts.BaseClasses;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BigBoss : ShipBase
{
    float actHealth = 100;
    [SerializeField] float health = 100;
    [SerializeField] float[] minTimeBetweenShoots;
    [SerializeField] float[] maxTimeBetweenShotts;
    [SerializeField] GameObject[] lasers;
    [SerializeField] float[] laserSpeed;
    [SerializeField] AudioClip DeathSound;
    [SerializeField] GameObject DeathEffect;
    [SerializeField] float DurationOfExplosion = 10f;
    [SerializeField] [Range(0, 1)] float deathSundVolume = 0.7f;
    public static int Count=0;

    float[] timeUntilFire;

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
        for (int i = 0; i < lasers.Length; i++)
        {
            timeUntilFire[i] -= Time.deltaTime;
            if (timeUntilFire[i] < 0)
            {
                var l = Instantiate(
                    lasers[i],
                    transform.position,
                    Quaternion.identity);
                l.GetComponent<Rigidbody2D>().
                velocity = new Vector2(0, -laserSpeed[i]);
                timeUntilFire[i] = Random.Range(minTimeBetweenShoots[i], maxTimeBetweenShotts[i]);
            }
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
