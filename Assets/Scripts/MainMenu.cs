using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Lvl1()
    {
        SceneManager.LoadScene("Lvl1");
    }

    public void Lvl2()
    {
        SceneManager.LoadScene("Lvl2");
    }

    public void Lvl3()
    {
        SceneManager.LoadScene("Lvl3");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
