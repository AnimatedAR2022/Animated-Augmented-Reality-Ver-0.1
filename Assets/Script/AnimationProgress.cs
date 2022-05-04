using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationProgress : MonoBehaviour
{
    [NamedArrayAttribute(new string[] { "GuideArm", "Actuator_Bolt", "Platter_Middle", "Circles", "TopCover",
        "Screws1", "Screws2", "Screws3", "Screws4", "Screws5", "Screws6" })]
    public Animator[] animators = new Animator[11];

    public int animationStage = -1;
    public float delayAnimation = 1f;
    public float animationRatio = 1f;
    private bool hasSetupUnscrewStage = false;
    private bool hasSetupRest = false;
    private bool hasSetupDiskDisplay = false;
    private bool hasSetupPreMovement = false;
    private bool hasSetupSlowMovement = false;
    private bool hasSetupClosingStage = false;

    private void Start()
    {
        StartCoroutine(StartAnimation());
    }

    void Update()
    {
        if (animationStage == 6) return;
        if (animationStage == 0)
        {
            if (!hasSetupUnscrewStage)
            {
                StartCoroutine(SetupUnscrewStage());
            }
        }
        else if (animationStage == 1)
        {
            if (!hasSetupRest)
            {
                StartCoroutine(SetupRestStage());
            }
        }
        else if (animationStage == 2)
        {
            if (!hasSetupDiskDisplay)
            {
                StartCoroutine(SetupDiskDisplay());
            }
        }
        else if (animationStage == 3)
        {
            if (!hasSetupPreMovement)
            {
                StartCoroutine(SetupPreMovement());
            }
        }
        else if (animationStage == 4)
        {
            if (!hasSetupSlowMovement)
            {
                StartCoroutine(SetupSlowMovement());
            }
        }
        else if (animationStage == 5)
        {
            if (!hasSetupClosingStage)
            {
                StartCoroutine(SetupClosingStage());
            }
        }
    }

    private IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(delayAnimation);
        animationStage = 0;
    }

    private IEnumerator SetupUnscrewStage()
    {
        hasSetupUnscrewStage = true;
        yield return new WaitForSeconds(1f / animationRatio);
        for (int i = 5; i < 11; i++)
            animators[i].SetTrigger("Unscrew");
        yield return new WaitForSeconds(2f / animationRatio);
        animators[4].SetTrigger("Lift");
        yield return new WaitForSeconds(2f / animationRatio);
        animationStage = 1;
    }

    private IEnumerator SetupRestStage()
    {
        hasSetupRest = true;
        yield return new WaitForSeconds(2f / animationRatio);
        for (int i = 0; i < 4; i++)
        {
            animators[i].SetTrigger("Rest");
        }
        animationStage = 2;
    }

    private IEnumerator SetupDiskDisplay()
    {
        hasSetupDiskDisplay = true;
        yield return new WaitForSeconds(5f / animationRatio);
        animators[2].SetTrigger("Float");
        yield return new WaitForSeconds(5f / animationRatio);
        animators[2].SetTrigger("Rest");
        yield return new WaitForSeconds(2f / animationRatio);
        animationStage = 3;
    }

    private IEnumerator SetupPreMovement()
    {
        hasSetupPreMovement = true;
        animators[0].SetTrigger("Support");
        animators[1].SetTrigger("Normal");
        animators[2].SetTrigger("Normal");
        yield return new WaitForSeconds(10f / animationRatio);
        animationStage = 4;
    }

    private IEnumerator SetupSlowMovement()
    {
        hasSetupSlowMovement = true;
        animators[1].SetTrigger("Slow");
        animators[2].SetTrigger("Slow");
        animators[3].SetTrigger("Outer");
        yield return new WaitForSeconds(3f / animationRatio);
        animators[3].SetTrigger("Inner");
        animators[2].SetTrigger("Rest");
        animators[1].SetTrigger("Random");
        yield return new WaitForSeconds(2f / animationRatio);
        animators[3].SetTrigger("Segment");
        yield return new WaitForSeconds(2f / animationRatio);
        animators[3].SetTrigger("Identify");
        yield return new WaitForSeconds(4f / animationRatio);
        animators[1].SetTrigger("Read");
        animators[2].SetTrigger("Read");
        animators[3].SetTrigger("Read");
        yield return new WaitForSeconds(5f / animationRatio);
        animators[3].SetTrigger("Rest");
        animators[2].SetTrigger("Normal");
        animators[1].SetTrigger("Normal");
        yield return new WaitForSeconds(3f / animationRatio);
        animationStage = 5;
    }

    private IEnumerator SetupClosingStage()
    {
        hasSetupClosingStage = true;
        for (int i = 0; i < 11; i++)
        {
            if (i < 4) animators[i].SetTrigger("Rest");
            else if (i == 4) animators[i].SetTrigger("Unlift");
            else animators[i].SetTrigger("Screw");
        }
        yield return new WaitForSeconds(3f / animationRatio);
        animationStage = 6;
    }
}
