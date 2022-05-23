using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksManager : MonoBehaviour
{
    public static readonly int DEFAULT_ATTACK_HEIGHT_LIMIT = 11;
    public AudioClip spellCastingSFX;

    CharacterController characterController;
    InputManager inputManager;

    public List<Attack> activeAttacks;
    public List<Attack> disabledAttacks;

    public GameObject attackPrefab;
    float attackSpeed = 5;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();
        activeAttacks = new List<Attack>();
    }

    private void Update()
    {
        if (inputManager.GetAttackButtonStatus() && activeAttacks.Count<characterController.GetNumberOfAllowedAttacks())
        {
            FireAttack();
        }

        for(int i=0;i<activeAttacks.Count;i++)
        {
            UpdateAttackMovement(activeAttacks[i]);
        }

        RemoveDisabledAttacks();
    }

    private void UpdateAttackMovement(Attack attack)
    {
        attack.transform.Translate(Vector3.up * attackSpeed * Time.deltaTime);

        if (attack.transform.position.y > DEFAULT_ATTACK_HEIGHT_LIMIT)
        {
            disabledAttacks.Add(attack);
            activeAttacks.Remove(attack);
        }
    }

    private void RemoveDisabledAttacks()
    {
        while (disabledAttacks.Count > 0)
        {
            GameObject tmp = disabledAttacks[0].gameObject;
            disabledAttacks.RemoveAt(0);
            Destroy(tmp);
        }
    }

    private void FireAttack()
    {
        GameObject newAttack = Instantiate(attackPrefab, transform.position, Quaternion.identity);

        activeAttacks.Add(newAttack.GetComponent<Attack>());
    }

    public void DisableAllAttacks()
    {
        while (activeAttacks.Count > 0)
        {
            disabledAttacks.Add(activeAttacks[0]);
            activeAttacks.Remove(activeAttacks[0]);
        }

        RemoveDisabledAttacks();
    }
}
