using System;
using UnityEngine;

public enum SquareType
{
    Idle,
    ActivePiece,
    Target
}

public class SquareView : MonoBehaviour
{
    [SerializeField] private MeshRenderer baseMeshRenderer;
    [SerializeField] private MeshRenderer outlineMeshRenderer;
    [SerializeField] private Material activePieceMaterial;
    [SerializeField] private Material targetMaterial;
    
    private Material baseMaterial;

    private int _x;
    private int _y;

    public void SetBaseMaterial(Material material)
    {
        baseMeshRenderer.sharedMaterial = material;
        outlineMeshRenderer.sharedMaterial = material;
        baseMaterial = material;
    }

    public void SetCoords(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public (int, int) GetSquareCoordinates()
    {
        return (_x, _y);
    }

    public void SetSquareType(SquareType type)
    {
        switch (type)
        {
            case SquareType.Idle:
                outlineMeshRenderer.sharedMaterial = baseMaterial;
                break;

            case SquareType.ActivePiece:
                outlineMeshRenderer.sharedMaterial = activePieceMaterial;
                break;

            case SquareType.Target:
                outlineMeshRenderer.sharedMaterial = targetMaterial;
                break;
        }
    }
}
