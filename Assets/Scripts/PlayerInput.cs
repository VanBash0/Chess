using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private LayerMask _pieceMask;
    [SerializeField] private LayerMask _squareMask;

    private BoardInputActions _boardInputActions;
    private Camera _mainCamera;
    private float _rayDistance = 500f;


    public EventHandler<SquareSelectedEventArgs> OnSquareSelected;

    public class SquareSelectedEventArgs : EventArgs
    {
        public (int, int) SquareCoord { get; set; }
    }

    public void Awake()
    {
        _mainCamera = Camera.main;
        _boardInputActions = new BoardInputActions();
    }

    private void OnEnable()
    {
        _boardInputActions.PlayerInput.Enable();
        _boardInputActions.PlayerInput.Click.performed += HandlePlayerClick;
    }

    private void OnDisable()
    {
        _boardInputActions.PlayerInput.Click.performed -= HandlePlayerClick;
        _boardInputActions.PlayerInput.Disable();
    }

    private void HandlePlayerClick(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (Pointer.current == null) return;

        Vector2 mousePos = Pointer.current.position.ReadValue();
        Ray ray = _mainCamera.ScreenPointToRay(mousePos);

        LayerMask combinedMask = _pieceMask | _squareMask;

        (int, int) targetCoord = (-1, -1);
        bool targetCoordChanged = false;

        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, combinedMask))
        {
            PieceView piece = hit.collider.GetComponentInParent<PieceView>();
            if (piece != null)
            {
                targetCoord = piece.GetPieceCoordinates();
                targetCoordChanged = true;
            }

            SquareView square = hit.collider.GetComponentInParent<SquareView>();
            if (square != null)
            {
                targetCoord = square.GetSquareCoordinates();
                targetCoordChanged = true;
            }
        }

        if (targetCoordChanged)
        {
            OnSquareSelected?.Invoke(this, new SquareSelectedEventArgs { SquareCoord = targetCoord });
        }
    }
}
