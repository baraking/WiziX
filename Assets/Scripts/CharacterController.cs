using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    InputManager inputManager;

    float movementSpeed = 6;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.right * inputManager.GetHorizontalMovement() * movementSpeed * Time.deltaTime);
    }
}
