using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("Collide");
        Destroy(collider.gameObject);
    }

    
}
