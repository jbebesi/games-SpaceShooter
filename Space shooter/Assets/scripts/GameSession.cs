using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int mScore = 0;
    public int Score {  get =>mScore; private set => mScore=value; }
    // Start is called before the first frame update
    void Awake()
    {
        if(FindObjectsOfType(GetType()).Length>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddScore(int score)
    {
        Score += score;
    }

    internal void ResetGame()
    {
        Score = 0;
    }

    void ResetScore() => Score = 0;
        

}
