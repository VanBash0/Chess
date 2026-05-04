using UnityEngine;

public class PieceView : MonoBehaviour
{
    public void MoveTo(Vector3 targetPos)
    {
        transform.position = targetPos;
    }

    public void DestroyPiece()
    {
        Destroy(gameObject);
    }
}
