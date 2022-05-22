using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public Joystick joystick;
    public Button attackButton;

    float horizontalMovement;
    bool isPressingAttackButton;

    private void Update()
    {
        horizontalMovement = joystick.Horizontal;
    }

    public float GetHorizontalMovement()
    {
        return horizontalMovement;
    }

    public bool GetAttackButtonStatus()
    {
        return isPressingAttackButton;
    }

    public void SetAttackButtonToClicked()
    {
        isPressingAttackButton = true;
    }

    public void SetAttackButtonToNotClicked()
    {
        isPressingAttackButton = false;
    }
}
