using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewAnimation : MonoBehaviour
{
    public bool startAnimation = false;
    public bool setTransparent = false;
    public bool unscrewed = false;

    [NamedArrayAttribute(new string[] { "x", "y", "z" })]
    public bool[] targetLocationControl = new bool[3];
    [NamedArrayAttribute(new string[] { "x", "y", "z" })]
    public bool[] targetRotationControl = new bool[3];

    public Vector3 targetLocation;
    public Vector3 targetRotation;

    public float rotationDelay = 0f;
    public float translationDelay = 0f;
    public float transparentDelay = 0f;

    public float translateAnimtionTime;
    public float rotateAnimtionTime;

    public Vector3 originalPosition;
    public Vector3 originalRotation;

    private float transateAnimationRatio = 0f;
    private float rotateAnimationRatio = 0f;

    private bool translationFlagRoutine = false;
    private bool transparentFlagRoutine = false;
    private bool completeRotation = false;
    private bool completeTranslation = false;

    private void Awake()
    {
        originalPosition = this.transform.position;
        originalRotation = this.transform.rotation.eulerAngles;
        targetLocation = new Vector3(
            targetLocationControl[0] ? this.transform.position.x : targetLocation.x,
            targetLocationControl[1] ? this.transform.position.y : targetLocation.y,
            targetLocationControl[2] ? this.transform.position.z : targetLocation.z
        );
        targetRotation = new Vector3(
            targetRotationControl[0] ? this.transform.rotation.x : targetRotation.x,
            targetRotationControl[1] ? this.transform.rotation.y : targetRotation.y,
            targetRotationControl[2] ? this.transform.rotation.z : targetRotation.z
        );
    }

    void Update()
    {
        if (!startAnimation) return;
        if (this.transform.rotation.eulerAngles.Equals(targetRotation) && this.transform.position.Equals(targetLocation) && !setTransparent)
        {
            startAnimation = false;
            return;
        }

        if (!this.transform.rotation.Equals(Quaternion.Euler(targetRotation)))
        {
            rotateAnimationRatio += Time.deltaTime / rotateAnimtionTime;
            this.transform.rotation = Quaternion.Euler(Vector3.Lerp(originalRotation, targetRotation, rotateAnimationRatio));
        }
        else if (!this.transform.position.Equals(targetLocation))
        {
            if (!translationFlagRoutine)
            {
                translationFlagRoutine = true;
                StartCoroutine(AnimationDelay(translationDelay, 1));
            }
            if (completeRotation)
            {
                transateAnimationRatio += Time.deltaTime / translateAnimtionTime;
                this.transform.position = Vector3.Lerp(originalPosition, targetLocation, transateAnimationRatio);
            }
        }
        else if (setTransparent)
        {
            if (!transparentFlagRoutine)
            {
                transparentFlagRoutine = true;
                StartCoroutine(AnimationDelay(transparentDelay, 2));
            }
            if (completeTranslation)
            {
                this.gameObject.SetActive(false);
            }
        }

    }

    IEnumerator AnimationDelay(float delay, int flagIdentity)
    {
        yield return new WaitForSeconds(delay);
        if (flagIdentity == 0) startAnimation = true;
        else if (flagIdentity == 1) completeRotation = true;
        else if (flagIdentity == 2) completeTranslation = true;
    }

    public void StartAnimation()
    {
        unscrewed = !unscrewed;
        StartCoroutine(AnimationDelay(rotationDelay, 0));
    }

    public void ResetAnimation()
    {
        startAnimation = false;
        setTransparent = false;

        transateAnimationRatio = 0f;
        rotateAnimationRatio = 0f;

        translationFlagRoutine = false;
        transparentFlagRoutine = false;
        completeRotation = false;
        completeTranslation = false;

        targetLocation = originalPosition;
        targetRotation = originalRotation;
        originalPosition = this.transform.position;
        originalRotation = this.transform.rotation.eulerAngles;
        StartCoroutine(AnimationDelay(rotationDelay, 0));
    }

    public void DebugClick()
    {
        Debug.Log("Clicked");
    }
}
