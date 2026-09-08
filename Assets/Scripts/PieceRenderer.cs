using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PieceRenderer : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Vector2 _cellSize;

    [Foldout("WhiteSprites"), SerializeField] private Sprite _whitePawn;
    [Foldout("WhiteSprites"), SerializeField] private Sprite _whiteBishop;
    [Foldout("WhiteSprites"), SerializeField] private Sprite _whiteKnight;
    [Foldout("WhiteSprites"), SerializeField] private Sprite _whiteRook;
    [Foldout("WhiteSprites"), SerializeField] private Sprite _whiteQueen;
    [Foldout("WhiteSprites"), SerializeField] private Sprite _whiteKing;
    
    [Foldout("BlackSprites"), SerializeField] private Sprite _blackPawn;
    [Foldout("BlackSprites"), SerializeField] private Sprite _blackBishop;
    [Foldout("BlackSprites"), SerializeField] private Sprite _blackKnight;
    [Foldout("BlackSprites"), SerializeField] private Sprite _blackRook;
    [Foldout("BlackSprites"), SerializeField] private Sprite _blackQueen;
    [Foldout("BlackSprites"), SerializeField] private Sprite _blackKing;

    private Dictionary<int, GameObject> _piecePosition = new Dictionary<int, GameObject>();
    
    
    public void RenderNewBoard(int[] grid)
    {
        foreach (GameObject piece in _piecePosition.Values)
        {
            Destroy(piece);
        }
        
        _piecePosition.Clear();
        
        for (int i = 0; i < grid.Length; i++)
        {
            if(grid[i] == -1) continue;
            
            (PieceColor,PieceType) piece = GameManager.GetPiece(grid[i]);
            PieceColor pieceColor = piece.Item1;
            PieceType pieceType = piece.Item2;
            
            
        }
    }
    
    
    
    
    
    
    
    
    
    static public PieceRenderer Instance { get; private set; }
    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
