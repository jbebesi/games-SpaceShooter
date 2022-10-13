using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{

    //Text _displayScore;
    [SerializeField]Text displayScore;
    //{
    //    get
    //    {
    //        if(!_displayScore)
    //        {
    //            _displayScore = GetComponent<Text>();
    //        }
    //        ret
    //    }
    //}
    GameSession _session;
    GameSession session
    {
        get
        {
            if (!_session)
            {
                Debug.Log("no _session!");
                _session = FindObjectOfType <GameSession>();
            }
            return _session;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        displayScore.text = session.Score.ToString("000000");
    }
}
