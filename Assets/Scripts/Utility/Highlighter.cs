using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Highlighter : MonoBehaviour
{
    [SerializeField] Material highlightMaterial;

    MeshRenderer[] meshRenderers;
    Material[] originalMaterials;

    protected virtual Material DefaultHighlightMaterial => GameManager.Instance().GetHighlightMaterial();

    bool isHighlighted = false;
    
    void Awake()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        originalMaterials = new Material[meshRenderers.Length];

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            originalMaterials[i] = meshRenderers[i].sharedMaterial;
        }
    }
    
    public void SetHighlightMaterial(Material material)
    {
        highlightMaterial = material;
    }

    public void HighlightOn()
    {
        if (meshRenderers == null) return;
        if (isHighlighted) return;

        if (highlightMaterial != null)
        {
            SetMaterialToMeshes(highlightMaterial);
        }
        else
        {
            SetMaterialToMeshes(DefaultHighlightMaterial);
        }

        isHighlighted = true;
    }


    public void HighlightOff()
    {
        if (meshRenderers == null) return;
        if (!isHighlighted) return;

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].sharedMaterial = originalMaterials[i];
        }
        isHighlighted = false;
    }

    void SetMaterialToMeshes(Material material)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].sharedMaterial = material;
        }
    }
}
