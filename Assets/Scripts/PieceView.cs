using UnityEngine;

public class PieceView : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    private int _x;
    private int _y;

    public void MoveTo(Vector3 targetPos, int newX, int newY)
    {
        transform.position = targetPos;
        SetCoords(newX, newY);
    }

    public void SetCoords(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public (int, int) GetPieceCoordinates()
    {
        return (_x, _y);
    }

    public void DestroyPiece()
    {
        Destroy(gameObject);
    }

    public void SetMaterial(Material material)
    {
        _meshRenderer.sharedMaterial = material;
    }
}
