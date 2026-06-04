using UnityEngine;

[CreateAssetMenu()]
public class SquareLibrarySO : ScriptableObject
{
    [SerializeField] private Material whiteSquareMaterial;
    [SerializeField] private Material blackSquareMaterial;   

    public Material GetSquareMaterial(PlayerColor color)
    {
        return color == PlayerColor.White ? whiteSquareMaterial : blackSquareMaterial;
    }
}