using UnityEngine;

public class PieceView : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    public void MoveTo(Vector3 targetPos)
    {
        transform.position = targetPos;
    }

    public void DestroyPiece()
    {
        Destroy(gameObject);
    }

    public void SetMaterial(Material material)
    {
        _meshRenderer.material = material;
    }
}
