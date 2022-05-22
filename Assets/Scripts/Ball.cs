using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static readonly int DEFAULT_BALL_SIZE=4;

    public int size;
    public int route;
    public int xDir;

    public bool needToAlterRoute;

    private void Awake()
    {
        if (size == 0)
        {
            size = DEFAULT_BALL_SIZE;
        }

        if (route != size)
        {
            route = size;
        }

        if (xDir == 0)
        {
            xDir = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            xDir = -xDir;
        }

        if (other.tag == "Player")
        {
            //Debug.Log("Player Hit!");
        }

        if(other.tag == "Attack")
        {
            BallsManager.instance.ReduceBallSize(this);
        }
    }
}
