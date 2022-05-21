using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int size;
    public int route;
    public int xDir;

    public bool needToAlterRoute;

    private void Awake()
    {
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
    }
}
