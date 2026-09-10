using System;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GridRenderer : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Vector2 _cellSize;
    
    [Header("Cell Graphism")]
    [SerializeField] private Sprite _cellSprite;
    [SerializeField] private Color _evenColor;
    [SerializeField] private Color _oddColor;
    [SerializeField] private float _colorFadeDuration;
    [SerializeField] private Ease _colorFaceEase;
    [Header("HighlightType")]
    [SerializeField] private float _highlightStrenght;
    [SerializeField] private Color _originalColor;
    [SerializeField] private Color _legalMoveColor;
    [SerializeField] private Color _pieceEatingColor;
    
    private List<List<SpriteRenderer>> _cells = new List<List<SpriteRenderer>>();

    public void Start()
    {
        CreateGrid();
    }

    [Button]
    public void CreateGrid()
    {
        DestroyGrid();
        for (int i = 0; i < _gridSize.x; i++)
        {
            _cells.Add(new List<SpriteRenderer>());
            for (int f = 0; f < _gridSize.x; f++)
            {
                print(i + " : " + f);
                GameObject cell = new GameObject();
                cell.name = "Cell" + i + " : "+  f;
                cell.transform.parent = transform;
                cell.transform.localPosition = new Vector2(i * _cellSize.x, -f * _cellSize.y);
                SpriteRenderer cellSpriteRenderer = cell.AddComponent<SpriteRenderer>();
                cellSpriteRenderer.sprite = _cellSprite;
                cellSpriteRenderer.color = (i+f) % 2  == 0 ? _evenColor : _oddColor;
                
                _cells[i].Add(cellSpriteRenderer);
            }
        }
    }

    [Button]
    public void DestroyGrid()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        _cells.Clear();
    }

    [Button]
    public void ResetGridColors( bool instant = false)
    {
        for (int i = 0; i < _gridSize.x; i++)
        {
            for (int f = 0; f < _gridSize.x; f++)
            {
                if(instant) _cells[i][f].color = (i+f) % 2 == 0 ? _evenColor : _oddColor;
                else _cells[i][f].DOColor((i+f) % 2 == 0 ? _evenColor : _oddColor,_colorFadeDuration).SetEase(_colorFaceEase);
            }
        }
    }

    public void ShowLegalMoves(int position)
    {
        int[] legalMoves = LegalMove.GetLegalMove(GameManager.Instance.grid, position);
        if(legalMoves == null)
        {
            print("No legal moves found");
            return;
        }
        for (int i = 0; i < legalMoves.Length; i++)
        {
            if (legalMoves[i] > 0)
            {
                if (legalMoves[i] == 2) SetHightlight(Highlights.PieceEatingColor,i);
                else SetHightlight(Highlights.LegalColor,i);
            }
        }
    }

    public void SetHightlight(Highlights highlight, int position)
    {
        Color color;
        switch(highlight)
        {
            case Highlights.OriginalColor:
                color = _originalColor;
                break;
            case Highlights.LegalColor:
                color = _legalMoveColor;
                break;
            case Highlights.PieceEatingColor:
                color = _pieceEatingColor;
                break;
            default:
                color = Color.magenta;
                break;
        }
        SetGridColor(position,Color.Lerp(color, (position % 8 + position / 8) % 2 == 0 ? _evenColor : _oddColor, _highlightStrenght));
    }

    public void ResetSingularColor(int position, bool instant = false)
    {
        Vector2Int gridPos = new Vector2Int(position % 8, position / 8);
        if(instant) _cells[gridPos.x][gridPos.y].color = (gridPos.x + gridPos.y) % 2 == 0 ? _evenColor : _oddColor;
        else _cells[gridPos.x][gridPos.y].DOColor((gridPos.x + gridPos.y) % 2 == 0 ? _evenColor : _oddColor,_colorFadeDuration).SetEase(_colorFaceEase);
    }
    
    public void SetGridColor(int position, Color color, bool instant = false)
    {
        Vector2Int gridPos = new Vector2Int(position % 8, position / 8);
        if (_cells.Count > gridPos.x)
        {
            List<SpriteRenderer> cell = _cells[gridPos.x];
            if (cell.Count > gridPos.y)
            {
                if(instant) cell[gridPos.y].color = color;
                else cell[gridPos.y].DOColor(color,_colorFadeDuration).SetEase(_colorFaceEase);
            }
        }
    }
    
    static public GridRenderer Instance { get; private set; }
    private void Awake()
    {
        Singleton();
    }
    private void Singleton()
    {
        if (Instance !=null && Instance != this)
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

    public enum Highlights
    {
        OriginalColor,
        LegalColor,
        PieceEatingColor,
    }
}
