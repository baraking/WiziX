using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static readonly int DEFAULT_BALL_SIZE = 4;
    public static readonly float BALL_X_SPAWN_THERSHOLD = .5f;
    public AudioClip portalBounceSFX;

    public int size;
    public int route;
    public int xDir;
    public int yDir;

    public float lastDistance;
    public float creationTime;

    public bool needToAlterRouteDueToGround;

    AudioSource audioSource;

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

        if (yDir == 0)
        {
            yDir = 1;
        }

        transform.localScale = new Vector3((float)size / DEFAULT_BALL_SIZE, (float)size / DEFAULT_BALL_SIZE, (float)size / DEFAULT_BALL_SIZE);

        creationTime = Time.time;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBounceSFX()
    {
        audioSource.clip = portalBounceSFX;
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            xDir = -xDir;
            PlayBounceSFX();
        }

        if (other.tag == "Player")
        {
            other.GetComponent<CharacterController>().Die();
            LevelManager.instance.CheckForLose();
        }

        if(other.tag == "Attack")
        {
            other.GetComponent<Attack>().PlayHitBySpellSFX();
            BallsManager.instance.ReduceBallSize(this);
        }
    }
}
