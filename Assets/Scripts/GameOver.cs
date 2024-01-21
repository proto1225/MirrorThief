using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject player;
    public GameObject[] enemies;
    public GameObject objective;
    public GameObject overScreen;
    public GameObject winScreen;
    public GameObject pauseScreen;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        objective = GameObject.FindGameObjectWithTag("Objective");
        overScreen = FindInActiveObjectByTag("GameOver");
        winScreen = FindInActiveObjectByTag("Win");
        pauseScreen = FindInActiveObjectByTag("Pause");
    }

    public void EndGame()
    {
        player.SetActive(false);
        foreach(var enemy  in enemies)
        {
            enemy.SetActive(false);
        }
        objective.SetActive(false);
        overScreen.SetActive(true);
    }

    public void Win()
    {
        player.SetActive(false);
        foreach (var enemy in enemies)
        {
            enemy.SetActive(false);
        }
        objective.SetActive(false);
        winScreen.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    GameObject FindInActiveObjectByTag(string tag)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].CompareTag(tag))
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
    }
}
