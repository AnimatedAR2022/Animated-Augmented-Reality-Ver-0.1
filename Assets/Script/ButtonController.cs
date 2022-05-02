using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void Execute_Tutorial()
    {
        Debug.Log("Begining Tutorial...");
    }

    public void Execute_Exit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }

    public void Execute_LoadScene(int sceneBuildIndex)
    {
        Debug.Log("Loading Scene " + sceneBuildIndex + "...");
        SceneManager.LoadScene(sceneBuildIndex);
    }
}
