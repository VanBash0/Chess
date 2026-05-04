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
    [SerializeField] private List<PieceData> _pieceData;

    private Dictionary<(PieceType, PlayerColor), PieceData> _cache;

    private void Initialize()
    {
        _cache = new Dictionary<(PieceType, PlayerColor), PieceData>();
        foreach (var piece in _pieceData)
        {
            _cache[(piece.pieceType, piece.playerColor)] = piece;
        }
    }

    public GameObject GetPiecePrefab(PieceType pieceType, PlayerColor playerColor)
    {
        if (_cache == null) Initialize();

        return _cache.TryGetValue((pieceType, playerColor), out var pieceData) ? pieceData.piecePrefab : null;
    }
}
