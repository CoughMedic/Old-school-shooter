using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{

    private void Start()
    {
        
    }
    private void Update()
    {
    }

    public void playGame(bool startGame)
    {
        print("PlayGame");
        SceneManager.LoadScene("Scenes/MainScene");
    }

    public void exitGame(bool exitGame)
    {
        print("ExitGame");
        Application.Quit();
    }
}
