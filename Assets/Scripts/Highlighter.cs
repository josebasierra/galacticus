using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Highlighter : MonoBehaviour
{
    [SerializeField] Material highlightMaterial;

    MeshRenderer meshRenderer;
    Material originalMaterial;

    protected virtual Material DefaultHighlightMaterial => GameManager.Instance().GetHighlightMaterial();

    bool isHighlighted = false;
    
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;
    }
    
    public void SetHighlightMaterial(Material material)
    {
        highlightMaterial = material;
    }

    public void HighlightOn()
    {
        if (meshRenderer == null) return;
        if (isHighlighted) return;

        if (highlightMaterial != null)
        {
            meshRenderer.material = highlightMaterial;
        }
        else
        {
            meshRenderer.material = DefaultHighlightMaterial;
        }

        isHighlighted = true;
    }


    public void HighlightOff()
    {
        if (meshRenderer == null) return;
        if (!isHighlighted) return;

        meshRenderer.material = originalMaterial;
        isHighlighted = false;
    }
}
