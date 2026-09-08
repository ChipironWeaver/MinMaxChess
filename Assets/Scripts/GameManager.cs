using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int[] grid;
    public Vector2Int gridSize = new Vector2Int(8,8); 
    
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

    private void Start()
    {
        print(PieceType.Rook + (int)PieceColor.Black);
    }
    
    private void CreateBoard(string fenCode = "")
    {
        grid = new int[gridSize.x * gridSize.y];
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int f = 0; f < gridSize.x; f++)
            {
                grid[i+f] = -1;
            }
        }
    }
    
    
    static public (PieceColor,PieceType) GetPiece(int value)
    {
        PieceColor pieceColor = (PieceColor)(value % 2);
        PieceType pieceType = (PieceType)(value - pieceColor);
        
        return(pieceColor, pieceType);
    }
}