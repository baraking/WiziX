using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    public List<Ball> allBalls;
    [SerializeField] GameObject ballPrefab;

    private void Start()
    {
        allBalls = new List<Ball>();
        foreach(Transform child in transform)
        {
            if (child.GetComponent<Ball>() != null && !allBalls.Contains(child.GetComponent<Ball>()))
            {
                allBalls.Add(child.GetComponent<Ball>());
            }
        }
    }

    //runs the balls update function from here instead of using multiple update functions
    private void Update()
    {
        foreach(Ball ball in allBalls)
        {
            UpdateBallMovement(ball);
        }

        //as a tmp testing for now
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReduceBallSize(allBalls[0]);
        }
    }

    //using a Sin function instead of physics bouncing, just like the original game. also, would be much more easing on the device.
    private void UpdateBallMovement(Ball ball)
    {
        Vector3 pos = ball.transform.position;
        float newY = Mathf.Sin(Time.time * (3 + (float)1 / ball.route));
        if (ball.needToAlterRoute && newY < -0.97f)
        {
            AlterBallMovement(ball);
        }
        ball.transform.position = new Vector3(pos.x + ball.xDir * (ball.route + 1) * Time.deltaTime, newY * ball.route + ball.route - 4, pos.z);
    }

    private void ReduceBallSize(Ball ball)
    {

    }

    private void AddBall(Ball originalBall, int xDir)
    {

    }

    private void AlterBallMovement(Ball ball)
    {

    }
}
