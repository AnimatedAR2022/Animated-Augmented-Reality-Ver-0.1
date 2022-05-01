using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void Execute_LearningMode()
    {
        Debug.Log("Begining Leaning Mode...");
        SceneManager.LoadScene(1);
    }

    public void Execute_Tutorial()
    {
        Debug.Log("Begining Tutorial...");
    }

    public void Execute_Exit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }

    public void Execute_Back(int sceneBuildIndex)
    {
        Debug.Log("Returning to scene " + sceneBuildIndex + "...");
        SceneManager.LoadScene(sceneBuildIndex);
    }
}
