using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetection : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.TryGetComponent<ScrewAnimation>(out ScrewAnimation target))
                {
                    if (!target.unscrewed)
                    {
                        Debug.Log("unscrewed");
                        target.StartAnimation();
                    }
                }
                else if (hit.collider.name.Equals("Cube"))
                {
                    gameObject.GetComponent<AnimationProgress>().UnhideScrews();
                    foreach (ScrewAnimation t in gameObject.GetComponent<AnimationProgress>().animatedObject)
                    {
                        t.ResetAnimation();
                    }
                }
            }
        }
    }
}
