using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallsManager : MonoBehaviour
{
    public List<Ball> allBalls;
    [SerializeField] GameObject ballPrefab;

    public UnityEvent onLevelWin;
    public UnityEvent onLevelLose;

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
        float newY = Mathf.Sin(Time.time * (Ball.DEFAULT_BALL_SIZE-1 + (float)1 / ball.route));
        if (ball.needToAlterRoute && newY < -0.97f)
        {
            AlterBallMovement(ball);
        }
        ball.transform.position = new Vector3(pos.x + ball.xDir * (ball.route + 1) * Time.deltaTime, newY * ball.route + ball.route - Ball.DEFAULT_BALL_SIZE, pos.z);
    }

    private void ReduceBallSize(Ball ball)
    {
        ball.size--;
        if (ball.size > 0)
        {
            AddBall(ball, 1);
            AddBall(ball, -1);
        }
        allBalls.Remove(ball);
        Destroy(ball.gameObject);

        if (allBalls.Count == 0)
        {
            onLevelWin?.Invoke();
            onLevelWin.RemoveAllListeners();
        }
    }

    private void AddBall(Ball originalBall, int xDir)
    {
        GameObject newBall = Instantiate(ballPrefab, originalBall.transform.position, Quaternion.identity, transform);
        newBall.transform.localScale = new Vector3((float)originalBall.size / Ball.DEFAULT_BALL_SIZE, (float)originalBall.size / Ball.DEFAULT_BALL_SIZE, (float)originalBall.size / Ball.DEFAULT_BALL_SIZE);

        Ball ball = newBall.GetComponent<Ball>();
        ball.size = originalBall.size;
        ball.route = originalBall.route;
        ball.xDir = xDir;
        ball.needToAlterRoute = true;
        allBalls.Add(ball);
    }

    private void AlterBallMovement(Ball ball)
    {
        ball.needToAlterRoute = false;
        ball.route = ball.size;
    }
}
