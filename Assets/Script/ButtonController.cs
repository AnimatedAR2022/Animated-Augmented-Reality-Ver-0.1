using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public void Execute_LearningMode()
    {
        Debug.Log("Begining Leaning Mode...");
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
}
