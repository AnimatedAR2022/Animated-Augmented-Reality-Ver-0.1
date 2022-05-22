using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TouchTransfrom : MonoBehaviour
{
    [SerializeField] private Transform targetSelected;
    [SerializeField] private Camera arCamera;
    [SerializeField] private float rotationSpeed = 20f;

    HashSet<Renderer> highlightedRenderers = new HashSet<Renderer>();
    List<Renderer> renderersBuffer = new List<Renderer>();
    List<Material> materialsBuffer = new List<Material>();

    static Material lineMaterial;
    static Material outlineMaterial;

    private float initialDistanceForScale = 0.0f;
    private Vector3 initialScale = Vector3.zero;

    private Rigidbody rbSelected;
    private bool dragging = false;

    private void Awake()
    {
        //GameObject.Find("/----------AR---------/AR Session Origin/AR Camera").TryGetComponent<Camera>(out arCamera);
        SetMaterial();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touchZero = Input.GetTouch(0);

            if (Input.touchCount == 1)
            {
                if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled)
                {
                    dragging = false;
                    return;
                }
                if (touchZero.phase == TouchPhase.Began)
                {
                    Ray ray = arCamera.ScreenPointToRay(touchZero.position);
                    RaycastHit hitObject;

                    if (Physics.Raycast(ray, out hitObject))
                    {
                        SetTargetSelected(GetTransformID(hitObject.transform));
                        AddTargetHighlightedRenderers(GetTransformID(targetSelected));
                        return;
                    }
                    else
                    {
                        RemoveHighlightedRenderers(GetTransformID(targetSelected));
                        targetSelected = null;
                        rbSelected = null;
                        return;
                    }
                }
                else
                {
                    if (targetSelected == null) return;
                    dragging = true;
                }
            }

            if (Input.touchCount == 2)
            {
                if (targetSelected == null) return;

                Touch touchOne = Input.GetTouch(1);
                if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
                   touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
                {
                    return;
                }
                if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                {
                    initialDistanceForScale = Vector2.Distance(touchZero.position, touchOne.position);
                    initialScale = targetSelected.localScale;
                }
                else
                {
                    float currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                    if (Mathf.Approximately(initialDistanceForScale, 0))
                    {
                        return;
                    }

                    float factor = currentDistance / initialDistanceForScale;
                    ScaleTargetSelected(initialScale, factor);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (rbSelected == null) return;
        if (dragging)
        {
            float x = Input.touches[0].deltaPosition.x * rotationSpeed * Time.fixedDeltaTime;
            float y = Input.touches[0].deltaPosition.y * rotationSpeed * Time.fixedDeltaTime;

            RotateTargetSelected(x, y);
        }
    }

    public void SetTargetSelected(int targetID)
    {
        Transform target = GetTransformFromID(targetID);
        if (target != null)
        {
            targetSelected = target;
            rbSelected = target.GetComponent<Rigidbody>();
        }
    }

    public void ClearTargetSelected()
    {
        targetSelected = null;
        rbSelected = null;
    }

    public void ScaleTargetSelected(Vector3 initialScale, float factor)
    {
        targetSelected.localScale = initialScale * factor;
    }

    public void RotateTargetSelected(float x, float y)
    {
        if (rbSelected == null || targetSelected == null) return;
        rbSelected.AddTorque(Vector3.down * x);
        rbSelected.AddTorque(Vector3.right * y);
    }

    public int GetTransformID(Transform target)
    {
        if (target.TryGetComponent<ManipulatableObject>(out ManipulatableObject manipulatableObject)) return manipulatableObject.id;
        else return -1;
    }

    public Transform GetTransformFromID(int targetID)
    {
        var objects = FindObjectsOfType<ManipulatableObject>();
        foreach (var o in objects)
        {
            if (o.id == targetID) return o.transform;
        }
        return null;
    }

    void GetTargetRenderers(Transform target, List<Renderer> renderers)
    {
        renderers.Clear();
        if (target != null)
        {
            target.GetComponentsInChildren<Renderer>(true, renderers);
        }
    }

    void AddTargetHighlightedRenderers(int targetID)
    {
        Transform target = GetTransformFromID(targetID);
        if (target != null)
        {
            GetTargetRenderers(target, renderersBuffer);

            for (int i = 0; i < renderersBuffer.Count; i++)
            {
                Renderer render = renderersBuffer[i];

                if (!highlightedRenderers.Contains(render))
                {
                    materialsBuffer.Clear();
                    materialsBuffer.AddRange(render.sharedMaterials);

                    if (!materialsBuffer.Contains(outlineMaterial))
                    {
                        materialsBuffer.Add(outlineMaterial);
                        render.materials = materialsBuffer.ToArray();
                    }

                    highlightedRenderers.Add(render);
                }
            }

            materialsBuffer.Clear();
        }
    }

    void RemoveHighlightedRenderers(int targetID)
    {
        Transform target = GetTransformFromID(targetID);

        GetTargetRenderers(target, renderersBuffer);
        for (int i = 0; i < renderersBuffer.Count; i++)
        {
            Renderer render = renderersBuffer[i];
            if (render != null)
            {
                materialsBuffer.Clear();
                materialsBuffer.AddRange(render.sharedMaterials);

                if (materialsBuffer.Contains(outlineMaterial))
                {
                    materialsBuffer.Remove(outlineMaterial);
                    render.materials = materialsBuffer.ToArray();
                }
            }

            highlightedRenderers.Remove(render);
        }

        renderersBuffer.Clear();
    }

    void SetMaterial()
    {
        if (lineMaterial == null)
        {
            lineMaterial = new Material(Shader.Find("Custom/Lines"));
            outlineMaterial = new Material(Shader.Find("Custom/Outline"));
        }
    }
}