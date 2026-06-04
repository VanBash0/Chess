using System;
using UnityEngine;

public class SquareView : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    public void SetMaterial(Material material)
    {
        meshRenderer.material = material;
    }
}
