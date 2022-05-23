using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallsManager : GenericSingleton<BallsManager>
{
    public List<Ball> allBalls;
    [SerializeField] GameObject ballPrefab;

    Collider bottomCollider;

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

        bottomCollider = Bounderies.instance.bottomBoundery.GetComponent<Collider>();
    }

    //runs the balls update function from here instead of using multiple update functions
    private void Update()
    {
        foreach(Ball ball in allBalls)
        {
            UpdateBallMovement(ball);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReduceBallSize(allBalls[0]);
        }
    }

    //using a Cos function instead of physics bouncing, just like the original game. also, would be much more easing on the device.
    private void UpdateBallMovement(Ball ball)
    {
        Vector3 pos = ball.transform.position;
        float lastFrameDistance;

        if (!ball.needToAlterRouteDueToGround)
        {
            float newY = Mathf.Abs(Mathf.Cos((Time.time - ball.creationTime) * (Ball.DEFAULT_BALL_SIZE - 1 + (float)1 / ball.route)));
            lastFrameDistance = newY * ball.route * 2 - 0.1f - pos.y;
            Debug.Log(lastFrameDistance);
            ball.transform.position = new Vector3(pos.x + ball.xDir * (ball.route + 1) * Time.deltaTime, newY * ball.route * 2 - 0.1f, pos.z);
        }
        else
        {
            float newY = Mathf.Abs(Mathf.Cos((Time.time - ball.creationTime - Mathf.PI / 10) * (Ball.DEFAULT_BALL_SIZE - 1 + (float)1 / ball.route))) / Mathf.Cos(Mathf.PI / 10);
            lastFrameDistance = newY * ball.route * 2 - 0.1f - pos.y;
            ball.transform.position = new Vector3(pos.x + ball.xDir * (ball.route + 1) * Time.deltaTime, newY * ball.route * 2 - 0.1f, pos.z);
        }

        DetectHittingTheGround(ball, Mathf.Abs(lastFrameDistance + ball.size / 2));
    }

    private void DetectHittingTheGround(Ball ball, float lastFrameDistance)
    {
        Ray groundDetectionRay = new Ray(ball.transform.position, transform.TransformDirection(Vector3.down * lastFrameDistance));
        Debug.DrawRay(ball.transform.position, transform.TransformDirection(Vector3.down * lastFrameDistance));
        if(Physics.Raycast(groundDetectionRay,out RaycastHit hit, lastFrameDistance))
        {
            if (hit.collider.tag == "Ground")
            {
                ball.PlayBounceSFX();
                if (ball.needToAlterRouteDueToGround)
                {
                    AlterBallMovement(ball);
                }
            }
        }
    }

    public void ReduceBallSize(Ball ball)
    {
        int newBallSize = ball.size-1;
        ball.size--;
        if (ball.size > 0)
        {
            AddBall(ball, 1);
            AddBall(ball, -1);
        }
        allBalls.Remove(ball);
        Destroy(ball.gameObject);

        if (newBallSize < 1)
        {
            LevelManager.instance.CheckForWin();
        }
    }

    private void AddBall(Ball originalBall, int xDir)
    {
        //need to change the transform position to a little bit to the left and the right. if is out of the screen to put just before the border
        GameObject newBall = Instantiate(ballPrefab, originalBall.transform.position, Quaternion.identity, transform);
        newBall.transform.localScale = new Vector3((float)originalBall.size / Ball.DEFAULT_BALL_SIZE, (float)originalBall.size / Ball.DEFAULT_BALL_SIZE, (float)originalBall.size / Ball.DEFAULT_BALL_SIZE);

        Ball ball = newBall.GetComponent<Ball>();
        ball.size = originalBall.size;
        ball.route = originalBall.route;
        ball.xDir = xDir;
        ball.yDir = 1;
        ball.needToAlterRouteDueToGround = true;
        allBalls.Add(ball);
    }

    public void AlterBallMovement(Ball ball)
    {
        ball.needToAlterRouteDueToGround = false;
        ball.creationTime -= Mathf.PI / 2;
        ball.route = ball.size;
        ball.lastDistance = 0;
    }
}
