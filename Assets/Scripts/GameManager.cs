using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public bool changeVisual;
    public int[] grid;
    public int[] gridInfo; // current moving color | position of the last en passantable pawn | left rook(1 = white, 2 = black, 3 = both), right rook)
    public Vector2Int gridSize = new Vector2Int(8,8);
    public string fenCode;
    public float backgroundStrengh;
    public bool checkForColor = true;


    public Dictionary<int, int[]> currentLegalMove;
    
    /*
    -1 = None
    0 = White Pawn
    1 = Black Pawn
    2 = White Bishop
    3 = Black Bishop
    4 = White Knight
    5 = Black Knight
    6 = White Rook
    7 = Black Rook
    8 = White Queen
    9 = Black Queen
    10 = White King
    11 = Black King
    */

    public void Start()
    {
        CreateBoard();
    }
    
    [Button]
    private void CreateBoard()
    {
        if(fenCode == null)
        {
            grid = new int[gridSize.x * gridSize.y];
            for (int i = 0; i < gridSize.x * gridSize.y; i++)
            {
                grid[i] = -1;
            }
        }
        else
        {
            grid = FenConvertor.GetGridFromFen(fenCode);
        }

        gridInfo = new int[4]
        {
            0, 
            -1, 
            3, 
            3
        };
        
        currentLegalMove = LegalMove.GetAllLegalMoves(grid, gridInfo);
        
        PieceManager.Instance.RenderNewBoard(grid);
        if (Camera.main != null)
            Camera.main.DOColor(Color.Lerp(gridInfo[0] == (int)PieceColor.White
                ? GridRenderer.Instance.evenColor
                : GridRenderer.Instance.oddColor, Color.black, backgroundStrengh) , 0.5f);
    }

    public bool MovePiece((int x, int y) piece, bool trust = false, bool movePiece = false)
    {
        if (piece.x > 64 || piece.y > 64 || piece.x < 0 || piece.y < 0) return false;
        if(grid[piece.x] % 2 != gridInfo[0] && checkForColor) 
        {
            print("NOT YOUR TURN");
            return false;
        }
        
        if(currentLegalMove[piece.x][piece.y] == 0 && !trust) return false;
        
        if ((PieceType)(grid[piece.x] - grid[piece.x] % 2) == PieceType.Pawn && Mathf.Abs(piece.x/8 - piece.y/8) == 2)
        {
            Debug.Log("Bitch did a nice double move");
            gridInfo[1] = piece.y;
        }
        else gridInfo[1] = -1;

        if (currentLegalMove[piece.x][piece.y] == 3)
        {
            
            int enPassantPiece = piece.y + (grid[piece.x] % 2 ==  0 ? 8 : -8 ) ;
            print(enPassantPiece);
            PieceManager.Instance.DestroyPiece(enPassantPiece);
            grid[enPassantPiece] = -1;
            Debug.Log("EnPassant");
        }
        
        gridInfo[0] =  gridInfo[0] == (int)PieceColor.White ?  (int)PieceColor.Black : (int)PieceColor.White;
        
        grid[piece.y] =  grid[piece.x];
        grid[piece.x] = -1;

        currentLegalMove = LegalMove.GetAllLegalMoves(grid, gridInfo);
        if (Camera.main != null&changeVisual)
            Camera.main.DOColor(Color.Lerp(gridInfo[0] == (int)PieceColor.White
                ? GridRenderer.Instance.evenColor
                : GridRenderer.Instance.oddColor, Color.black, backgroundStrengh) , 0.5f);
        
        return true;
    }
    
    static public (PieceColor,PieceType) GetPiece(int value)
    {
        PieceColor pieceColor = (PieceColor)(value % 2);
        PieceType pieceType = (PieceType)(value - pieceColor);
        
        return(pieceColor, pieceType);
    }
    
    static public GameManager Instance { get; private set; }
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