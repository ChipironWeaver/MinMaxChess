using NaughtyAttributes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int[] grid;
    public Vector2Int gridSize = new Vector2Int(8,8);
    public string fenCode;
    
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
        PieceManager.Instance.RenderNewBoard(grid);
    }

    public bool MovePiece((int x, int y) piece)
    {
        if (piece.x > 64 || piece.y > 64 || piece.x < 0 || piece.y < 0) return false;
        grid[piece.y] =  piece.x;
        grid[piece.x] = -1;

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