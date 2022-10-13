using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig mWaveConfig;
    float Speed = 1;
    Transform[] mWayPoints;
     int WayPointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        mWayPoints = mWaveConfig.GetWaypoints();
        Speed = mWaveConfig.GetMoveSpeed();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        mWaveConfig = waveConfig;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        try
        {
            if(WayPointIndex < mWayPoints.Length)
            {
                   
            }
            if (mWayPoints[WayPointIndex].transform.position == transform.position)
            {
                WayPointIndex++;
            }
            if (WayPointIndex > mWayPoints.Length - 1)
                WayPointIndex = 0;
            var goal = mWayPoints[WayPointIndex].position;
            transform.position = Vector2.MoveTowards(transform.position, goal, Speed * Time.deltaTime);
            
        }
        catch
        {
            WayPointIndex = 0;
        }
    }
}
