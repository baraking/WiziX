using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Joystick joystick;

    float horizontalMovement;

    private void Update()
    {
        horizontalMovement = joystick.Horizontal;
    }

    public float GetHorizontalMovement()
    {
        return horizontalMovement;
    }
}
