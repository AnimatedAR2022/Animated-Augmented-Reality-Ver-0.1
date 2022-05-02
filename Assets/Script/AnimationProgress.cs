using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationProgress : MonoBehaviour
{
    public List<ScrewAnimation> animatedObject;
    public string screwCommonName = "";
    public string topCoverCommonName = "";

    private int animationStage = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach (ScrewAnimation sa in animatedObject)
        {
            sa.enabled = false;
        }
        SetupUnscrewStage();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckAnimationProgression())
        {
            SetupUncoverStage();
        }
    }

    private bool CheckAnimationProgression()
    {
        foreach (ScrewAnimation sa in animatedObject)
        {
            if (sa.gameObject.name.Contains(screwCommonName) && sa.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    private void SetupUnscrewStage()
    {
        Debug.Log("hi");
        foreach (ScrewAnimation sa in animatedObject)
        {
            if (sa.gameObject.name.Contains(screwCommonName))
            {
                sa.enabled = true;
            }
        }
    }

    private void SetupUncoverStage()
    {
        foreach (ScrewAnimation sa in animatedObject)
        {
            if (sa.gameObject.name.Contains(topCoverCommonName))
            {
                sa.gameObject.GetComponent<MeshCollider>().enabled = true;
                sa.enabled = true;
            }
        }
    }
}
