using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    InputManager inputManager;
    AttacksManager attacksManager;

    float movementSpeed = 6;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        attacksManager = GetComponent<AttacksManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.right * inputManager.GetHorizontalMovement() * movementSpeed * Time.deltaTime);
    }
}
