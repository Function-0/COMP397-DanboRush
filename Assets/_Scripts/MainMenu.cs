using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Method for the new game button SceneManager.GetActiveScene().buildIndex + 1
    public void NewGame()
    {
        SceneManager.LoadScene("Prototype_1");
    }

    //Method for options
    public void Options()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    //Method For Quit
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
