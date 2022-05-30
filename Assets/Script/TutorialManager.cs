using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public List<Image> pageKnob;
    public List<GameObject> tutorials;
    public Button nextBtn;
    public Button prevBtn;

    private int currentPage = 0;

    private void Start()
    {
        currentPage = 0;
        nextBtn.interactable = true;
        prevBtn.interactable = false;
        CheckKnobState();
    }

    public void NextTutorial()
    {
        currentPage += 1;
        CheckTutorialState();
        CheckKnobState();
        if (currentPage >= tutorials.Count - 1) nextBtn.interactable = false;
        prevBtn.interactable = true;
    }

    public void PrevTutorial()
    {
        currentPage -= 1;
        CheckTutorialState();
        CheckKnobState();
        if (currentPage <= 0) prevBtn.interactable = false;
        nextBtn.interactable = true;
    }

    private void CheckKnobState()
    {
        for (int i = 0; i < pageKnob.Count; i++)
        {
            if (currentPage == i)
            {
                pageKnob[i].color = new Color(pageKnob[i].color.r, pageKnob[i].color.g, pageKnob[i].color.b, 1f);
            }
            else
            {
                pageKnob[i].color = new Color(pageKnob[i].color.r, pageKnob[i].color.g, pageKnob[i].color.b, 0.5f);
            }
        }
    }

    private void CheckTutorialState()
    {
        for (int i = 0; i < tutorials.Count; i++)
        {
            if (currentPage == i)
            {
                tutorials[i].SetActive(true);
            }
            else
            {
                tutorials[i].SetActive(false);
            }
        }
    }
}
