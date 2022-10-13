using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        var audio  = gameObject.GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Play();
            Destroy(audio, 3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

}
