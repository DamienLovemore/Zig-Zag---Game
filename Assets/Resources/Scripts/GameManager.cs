using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameStarted = false;
    public int score;

    public void StartGame()
    {
        //Indicates that the game has started, and the character
        //should be able to move and turn around.
        gameStarted = true;
        GameObject.Find("StartGameLayout").SetActive(false);

        //When the game starts, it begins
        //creating the scenario.
        Road roadManager = FindObjectOfType<Road>();
        roadManager.StartBuilding();
        CharController charController = FindObjectOfType<CharController>();
        StartCoroutine(charController.IncreaseCharSpeed());
    }

    private void Update()
    {
        //Starts the game, if it has not started yet
        if((Input.GetKeyDown(KeyCode.Return)) && (gameStarted is false))
        {
            StartGame();
        }
        //Quits the game, when the player press ESC
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        //Toggles between fullscreen and windowed mode,
        //when the player press F11
        else if(Input.GetKeyDown(KeyCode.F11))
        {
            FullScreenMode actualScreenMode = Screen.fullScreenMode;

            //If it is at windowed mode, then it
            //switches to fullscreen. 
            if(actualScreenMode == FullScreenMode.Windowed)
            {
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            }
            //If it was at fullscreen, then it
            //switches to windowed mode.
            else
            {
                Screen.SetResolution(840, 607, false);
            }
        }
    }

    public void EndGame()
    {
        //Reload the main scene (Restart the game)
        SceneManager.LoadScene(0);
    }

    public void IncreaseScore()
    {
        score++;

        //If the value is higher than the highscore
        //Sets the new HighScore
        if (score > GetHighScore())
        {
            PlayerPrefs.SetInt("Highscore", score);
            TextMeshProUGUI textHighScore = GameObject.Find("TextHighScore").GetComponent<TextMeshProUGUI>();
            textHighScore.text = $"Highscore: {score}";
        }
    }

    public int GetHighScore()
    {
        int i = PlayerPrefs.GetInt("Highscore");
        return i;
    }
}
