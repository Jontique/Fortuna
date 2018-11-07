using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private int ballCount = 5;


    public int activeBall;
    public float changeSceneAfterSeconds = -1;
    public bool changeScene = false;
    public int currentScore = 0;
    public int totalScore = 0;
    public string playerName;
    public StartGame gameStarter;
    public GameObject ball;
    public GameObject[] balls;
    public GameObject launchSpawn;
    public GameObject backEndObject;
    public Text finalScore;
    public Text scoreText;
    private Backend backEnd;

    private Vector3 launchPos;



	void Awake () //spawns before other scripts so they are ready to use
    {
        SpawnBalls();

    }

    private void Start()
    {
        changeSceneAfterSeconds = 0;
        changeScene = false;
        if (!gameStarter)
            gameStarter = GameObject.Find("GameStarter").GetComponent<StartGame>();
        playerName = gameStarter.playerName;
        
        if (!launchSpawn)
            launchSpawn = GameObject.Find("BallLaunchPos");
        activeBall = 0;
        launchPos = launchSpawn.transform.position;
        if (!backEndObject)
            backEndObject = GameObject.Find("Backend");
        backEnd = backEndObject.GetComponent<Backend>();
        if (!scoreText)
            scoreText = GameObject.Find("CurrentScoreText").GetComponent<Text>();
    }

    private void Update()
    {
        if(!finalScore){
            finalScore = GameObject.Find("FinalScoreText").GetComponent<Text>();
            if(finalScore) finalScore.text = "";
        }
        if(changeScene == true)
        {   
            changeSceneAfterSeconds -= Time.deltaTime;
            if(changeSceneAfterSeconds < float.Epsilon)
            {
                finalScore.text = "";
                changeScene = false;
                changeSceneAfterSeconds = -1;
                SceneManager.LoadScene(0);
            }
        }
        //  print("score = " + currentScore);
        scoreText.text = ("Score: " + currentScore.ToString());
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
       // print("next ball");
        balls[activeBall].GetComponent<BallBehaviour>().enabled = false; //disables the controls of previous ball
        if (activeBall < 4)
        {
            ++activeBall;
            balls[activeBall].transform.position = launchPos;
            balls[activeBall].GetComponent<BallBehaviour>().enabled = true;
        }

        else
        {
            print("game end");
        }
    }


    public void GameOver()
    {
        totalScore = currentScore;
        if(backEnd.ConnectedToDB == true) backEnd.SubmitScore(playerName, totalScore);
        finalScore.text = "Your final score " + totalScore.ToString();
        changeSceneAfterSeconds = 1;
        changeScene = true;
    }

    public GameObject GetActiveBall()
    {
        return balls[activeBall];
    }

}
