using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounderies : GenericSingleton<Bounderies>
{
    public GameObject bottomBoundery;
    [SerializeField] GameObject leftBoundery;
    [SerializeField] GameObject rightBoundery;

    //may want an awake function to change the level's layout according to the resolution or something.
}
