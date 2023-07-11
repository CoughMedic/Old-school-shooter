using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
    public MenuScript menu_func_ref;


    public void NewGame(bool startGame)
    {
        print("PlayGame");
        SceneManager.LoadScene("Scenes/Level1Scene");
        
    }

    public void LoadLevel(string value)
    {
        SceneManager.LoadScene("Scenes/" + value);
    }

    public void ExitGame(bool exitGame)
    {
        print("ExitGame");
        Application.Quit();
    }

    public void OpenLevelSelect(bool open)
    {
        menu_func_ref = GameObject.FindObjectOfType<MenuScript>();
        menu_func_ref.main_menu_ref.SetActive(false);
        menu_func_ref.level_select_ref.SetActive(true);


    }

    public void backToMainMenu(bool value)
    {
        menu_func_ref = GameObject.FindObjectOfType<MenuScript>();
        menu_func_ref.main_menu_ref.SetActive(true);
        menu_func_ref.level_select_ref.SetActive(false);
    }
}
