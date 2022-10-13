using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth: MonoBehaviour
{

    [SerializeField]Text displayHealth;
    Player _player;
    Player player
    {
        get
        {
            if (!_player)
            {
                Debug.Log("no player!");
                _player = FindObjectOfType <Player>();
            }
            return _player;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        displayHealth.text = player?.GetHealth().ToString() ?? "0";
    }
}
