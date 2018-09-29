using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private int ballCount = 5;


    public int activeBall;

    public GameObject ball;
    public GameObject[] balls;
    public GameObject launchSpawn;


    private Vector3 launchPos;



	void Awake () //spawns before other scripts so they are ready to use
    {
        SpawnBalls();

    }

    private void Start()
    {
        if (!launchSpawn)
            launchSpawn = GameObject.Find("BallLaunchPos");
        activeBall = 0;
        launchPos = launchSpawn.transform.position;
    }

    private void Update()
    {

    }



    private void SpawnBalls()
    {
        balls = new GameObject[ballCount];
        balls[0] = GameObject.Find("Ball"); //add first ball to the list
        if (balls.Length != ballCount)
        {
            balls = new GameObject[ballCount];
        }
        for (int i = 0; i < ballCount; ++i)
        {
            if (!balls[i]) //if ball doesn't exist it instantiates a new one 
            {
                balls[i] = Instantiate(ball, new Vector3(-i + 1.5f, -4.5f, 0), Quaternion.identity); //spawn them in a at the bottom, bubblegum code ftw
                balls[i].GetComponent<BallBehaviour>().enabled = false;
            }
        }
    }

    public void NextBall()
    {
        balls[activeBall].GetComponent<BallBehaviour>().enabled = false; //disables the controls of previous ball
        if (activeBall < 5)
        {
            ++activeBall;
            balls[activeBall].transform.position = launchPos;
            balls[activeBall].GetComponent<BallBehaviour>().enabled = true;
        }

        if(activeBall >= 5)
        {
            print("game over");
        }
    }

}
