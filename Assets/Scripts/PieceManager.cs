using System;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PieceManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Camera _camera;
    [Header("Grid Settings")]
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Vector2 _cellSize;
    [SerializeField] private bool _wobble;
    [SerializeField] private Material _pieceMaterial;
    [SerializeField] private Material _wobbleMaterial;
    [Header("Move Piece Settings")]
    [SerializeField] private float _snapSpeed;
    [SerializeField] private float _resetSpeed;

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

    private readonly Dictionary<int, SpriteRenderer> _piecePosition = new Dictionary<int, SpriteRenderer>();
    private GameObject _follower = null;
    private int _followerIndex = -1;
    
    
    public void Update()
    {
        if (_follower)
        {
            Vector2 pos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _follower.transform.position = pos;
        }
    }
    
    public void RenderNewBoard(int[] grid)
    {
        DestroyBoard();
        
        for (int i = 0; i < grid.Length; i++)
        {
            CreatePiece(grid[i], i);
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SetPieceFollowCursor(GetPiecePosition(_camera.ScreenToWorldPoint(Mouse.current.position.ReadValue())));
        }

        if (context.canceled)
        {
            SetPieceFollowCursor(-1);
        }
    }

    public int GetPiecePosition(Vector3 mousePos)
    {
        mousePos.x += 4;
        mousePos.y -= 4;
        mousePos.y *= -1;
        if (mousePos.x < 0 || mousePos.x > _gridSize.x || mousePos.y < 0 || mousePos.y > _gridSize.y) return -1;
            
        Vector2Int gridPosition = new Vector2Int((int)mousePos.x, (int)mousePos.y);
        
        return gridPosition.x + gridPosition.y * 8;
    }

    public void CreatePiece(int pieceIndex,int gridPosition)
    {
        if (pieceIndex == -1) return;
            
        (PieceColor,PieceType) piece = GameManager.GetPiece(pieceIndex);
        PieceColor pieceColor = piece.Item1;
        PieceType pieceType = piece.Item2;
            
        GameObject newPiece = new GameObject();
        newPiece.transform.parent = transform;
        newPiece.transform.localPosition = new Vector3(gridPosition % 8 * _cellSize.x, gridPosition / 8 *  -_cellSize.y , 0);
        newPiece.name = pieceColor + pieceType.ToString();
        SpriteRenderer spriteRenderer = newPiece.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 1;
        spriteRenderer.sprite = GetSprite(pieceIndex);
        spriteRenderer.material = _wobble ? _wobbleMaterial : _pieceMaterial;
            
        _piecePosition.Add(gridPosition, spriteRenderer);
    }

    public void ResetPiecePos(int piecePosition,float moveSpeed)
    {
        Vector3 position = new Vector3(piecePosition % 8 * _cellSize.x, piecePosition / 8 * -_cellSize.y, 0);
        if(moveSpeed == 0) _piecePosition[piecePosition].transform.localPosition = position;
        else _piecePosition[piecePosition].transform.DOLocalMove(position,moveSpeed).SetEase(Ease.InOutQuad);
    }
    
    public void SetPieceFollowCursor(int piecePosition)
    {
        if(piecePosition == -1) 
        {
            if (_follower)
            {
                int currentIndex = GetPiecePosition(_follower.transform.position); 
                if(currentIndex == -1) ResetPiecePos(_followerIndex,_resetSpeed);
                else
                {
                    if(currentIndex != _followerIndex)
                    {
                        if (GameManager.Instance.MovePiece((_followerIndex, currentIndex)))
                        {
                            DestroyPiece(currentIndex);
                            SpriteRenderer spriteRenderer = _piecePosition[_followerIndex];
                            _piecePosition.Remove(_followerIndex);
                            _followerIndex = currentIndex;
                            _piecePosition.Add(currentIndex, spriteRenderer);
                        }
                    }
                    ResetPiecePos(_followerIndex,_snapSpeed);
                    GridRenderer.Instance.ResetGridColors();
                    
                }
                _follower = null;
                _followerIndex = -1;
            }
        }
        else if (piecePosition < 64)
        {
            if (_piecePosition.TryGetValue(piecePosition, out var piece))
            {
                _follower = piece.gameObject;
                _followerIndex = piecePosition;
                GridRenderer.Instance.ShowLegalMoves(_followerIndex);
                GridRenderer.Instance.SetHightlight(GridRenderer.Highlights.OriginalColor,_followerIndex);
            }
            else Debug.Log("Clicker on no piece position: " + piecePosition);
        }
    }
    
    [Button]
    public void DestroyBoard()
    {
        foreach (SpriteRenderer piece in _piecePosition.Values)
        {
            if(piece) Destroy(piece);
        }
        _piecePosition.Clear();
    }

    public void DestroyPiece(int piecePosition)
    {
        if(!_piecePosition.TryGetValue(piecePosition, out var piece))
        {
            Debug.LogWarning("Piece not found: " + piecePosition);
            return;
        }
        if(piece) Destroy(piece.gameObject);
        _piecePosition.Remove(piecePosition);
    }

    public void ReplaceSprite(int piecePosition, int pieceIndex)
    {
        if(!_piecePosition.TryGetValue(piecePosition, out var piece))
        {
            Debug.LogWarning("Piece not found: " + piecePosition);
            return;
        }
        piece.sprite = GetSprite(pieceIndex);
    }
    
    public Sprite GetSprite(int pieceIndex)
    {
        
        (PieceColor,PieceType) piece = GameManager.GetPiece(pieceIndex);
        PieceColor pieceColor = piece.Item1;
        PieceType pieceType = piece.Item2;
        
        switch (pieceType)
        {
            case PieceType.Pawn:
                return pieceColor == PieceColor.White ? _whitePawn : _blackPawn;
            case PieceType.Bishop:
                return pieceColor == PieceColor.White ? _whiteBishop : _blackBishop;
            case PieceType.Knight:
                return pieceColor == PieceColor.White ? _whiteKnight : _blackKnight;
            case PieceType.Rook:
                return pieceColor == PieceColor.White ? _whiteRook : _blackRook;
            case PieceType.Queen:
                return pieceColor == PieceColor.White ? _whiteQueen : _blackQueen;
            case PieceType.King:
                return pieceColor == PieceColor.White ? _whiteKing : _blackKing;
            default:
                Debug.LogWarning("Unknown piece type: " + pieceType);
                return null;
        }
    }
    
    static public PieceManager Instance { get; private set; }
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
    
    public void OnDrawGizmosSelected()
    {
        for (int i = 0; i < _gridSize.x; i++)
        {
            for (int f = 0; f < _gridSize.x; f++)
            {
                Gizmos.color = (i+f) % 2  == 0 ? Color.violetRed: Color.blueViolet;
                Gizmos.DrawCube( new Vector2(i * _cellSize.x + transform.position.x, -f * _cellSize.y + transform.position.y), _cellSize);
            }
        }
    }
}
