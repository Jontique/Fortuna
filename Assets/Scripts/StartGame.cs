using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {


    private bool gameStarted = false;


    public InputField inputField;
    public Backend backEnd;
    public GameObject mainMenuPrefab;

    public Text top10ListText;
    public string playerName;
    private Text nameErrorText;
    List<User> top10;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start () {
        gameStarted = false;
        if (!backEnd)
        {
            backEnd = GameObject.Find("Backend").GetComponent<Backend>();

        }
        /*if(gameStarted == false)
        {
            LoadMainMenu();
        }*/
        if(!top10ListText)
        {
            top10ListText =  GameObject.Find("Top10list").GetComponent<Text>();
        }
        if(!nameErrorText)
        {
            nameErrorText = GameObject.Find("Error_Name").GetComponent<Text>();
        }
        nameErrorText.text = ""; //sets the string to null so it's not visible at the start
        if(!inputField)
        {
            inputField = GameObject.Find("MainMenuCanvas").GetComponentInChildren<InputField>();
        }
        top10 = backEnd.GetTop(10);
        ShowTopTen();
	}
	

  /*  void LoadMainMenu()
    {
        Instantiate(mainMenuPrefab);

        if (!inputField)
        {
            inputField = GameObject.Find("MainMenuCanvas").GetComponentInChildren<InputField>();
        }
    }*/

    public void BeginGame ()
    {
        if(inputField.text != "Enter your name" && inputField.text != "")
        {
            playerName = inputField.text.ToString();
            gameStarted = true;
            SceneManager.LoadScene(1); //loads the game scene. DontDestroyOnLoad should keep these objects intact.
        }
        else
        {
            nameErrorText.text = "Please enter a name";
        }
    }

    private void ShowTopTen()
    {
        int n = 1;

        if (top10 == null || top10.Count == 0)
        {
            top10ListText.text = "No high scores set";
        }
        else
        {
            foreach (User user in top10)
            {
                top10ListText.text += n + " " + (user.Name + " Score: " + user.HighScore + "\n"); //adds the number for each user, displays the name and score
            }
        }

    }
    void NewGame()
    {
        SceneManager.LoadScene(0);
    }
}
