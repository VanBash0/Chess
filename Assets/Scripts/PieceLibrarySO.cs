using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PieceData
{
    public PieceType pieceType;
    public PlayerColor playerColor;
    public GameObject piecePrefab;
}

[CreateAssetMenu()]
public class PieceLibrarySO : ScriptableObject
{
    [SerializeField] private List<PieceData> pieceData;
    [SerializeField] private Material whitePieceMaterial;
    [SerializeField] private Material blackPieceMaterial;

    private Dictionary<(PieceType, PlayerColor), PieceData> _cache;

    private void Initialize()
    {
        _cache = new Dictionary<(PieceType, PlayerColor), PieceData>();
        foreach (var piece in pieceData)
        {
            _cache[(piece.pieceType, piece.playerColor)] = piece;
        }
    }

    public GameObject GetPiecePrefab(PieceType pieceType, PlayerColor playerColor)
    {
        if (_cache == null) Initialize();

        return _cache.TryGetValue((pieceType, playerColor), out var pieceData) ? pieceData.piecePrefab : null;
    }

    public Material GetPieceMaterial(PlayerColor color)
    {
        return color == PlayerColor.White ? whitePieceMaterial : blackPieceMaterial;
    }
}
