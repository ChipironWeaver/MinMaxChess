using System.Collections.Generic;
using UnityEngine;

public struct GameState
{
    public int[] grid;
    public int[] gridInfo; // current moving color | position of the last en passantable pawn | left rook(1 = white, 2 = black, 3 = both), right rook)
    public Dictionary<int, int[]> currentLegalMove;

    public bool MovePiece((int x, int y) piece, bool trust = false, bool generateLegalMoves = true)
    {
        if (piece.x > 64 || piece.y > 64 || piece.x < 0 || piece.y < 0) return false; // move outside the grid
        if (grid[piece.x] % 2 != gridInfo[0] && trust) return false; //Not your turn
    
    
        if (currentLegalMove[piece.x][piece.y] == 0 && !trust) return false; //Illegal Move
    
        PieceColor pieceColor = (PieceColor)(grid[piece.x] % 2);
        PieceType pieceType = (PieceType)(grid[piece.x] - pieceColor);
    
        if (pieceType == PieceType.Pawn && Mathf.Abs(piece.x / 8 - piece.y / 8) == 2)
            gridInfo[1] = piece.y; //Changed the lattest pawn that did a double move, for the en passant
        else gridInfo[1] = -1;
    
        if (currentLegalMove[piece.x][piece.y] == 3) //EN PASSANT
        {
            //This destroys the piece that got en passanted
            int enPassantPiece = piece.y + (grid[piece.x] % 2 == 0 ? 8 : -8);
            PieceManager.Instance.DestroyPiece(enPassantPiece);
            grid[enPassantPiece] = -1;
        }
        else if (currentLegalMove[piece.x][piece.y] == 4) //CASTLING
        {
            switch (piece.y)
            {
                /*
                This is definitely one code of all time so trust the process, This part is to manage the castling, it moves the rook to the correct position.
                The values are hard codded since only 4 specific castling can even be played.
                */
                case 58: // BIG WHITE CASTLING
                    grid[56] = -1; //56 = A1
                    grid[59] = 6;
                    PieceManager.Instance.MovePiece((56, 59));
                    break;
                case 62: // SMALL WHITE CASTLING
                    grid[63] = -1; //56 = H1
                    grid[61] = 6;
                    PieceManager.Instance.MovePiece((63, 61));
                    break;
                case 2: // BIG BLACK CASTLING
                    grid[0] = -1; //0 = A8
                    grid[3] = 7;
                    PieceManager.Instance.MovePiece((0, 3));
                    break;
                case 6: // SMALL BLACK CASTLING
                    grid[7] = -1; //7 = H8
                    grid[5] = 7;
                    PieceManager.Instance.MovePiece((7, 5));
                    break;
            }
        }
    
        gridInfo[0] = gridInfo[0] == (int)PieceColor.White ? (int)PieceColor.Black : (int)PieceColor.White;
    
        if (pieceType == PieceType.King) //This reset the possible based on the king movement
        {
            if (pieceColor == PieceColor.White)
            {
                if (gridInfo[2] % 2 == 1)
                {
                    gridInfo[2] -= 1;
                }
    
                if (gridInfo[3] % 2 == 1)
                {
                    gridInfo[3] -= 1;
                }
            }
            else
            {
                if (gridInfo[2] / 2 == 1)
                {
                    gridInfo[2] -= 2;
                }
    
                if (gridInfo[3] / 2 == 1)
                {
                    gridInfo[3] -= 2;
                }
            }
        }
    
        if (pieceType == PieceType.Rook) //this reset the possible castling base on rook movement
        {
            if (pieceColor == PieceColor.White)
            {
                if (gridInfo[2] % 2 == 1 && piece.x == 56)
                {
                    gridInfo[2] -= 1;
                }
    
                if (gridInfo[3] % 2 == 1 && piece.x == 63)
                {
                    gridInfo[3] -= 1;
                }
            }
            else
            {
                if (gridInfo[2] / 2 == 1 && piece.x == 0)
                {
                    gridInfo[2] -= 2;
                }
    
                if (gridInfo[3] / 2 == 1 && piece.x == 7)
                {
                    gridInfo[3] -= 2;
                }
            }
        }
    
        grid[piece.y] = grid[piece.x]; //moves the piece
        grid[piece.x] = -1;
    
        if(generateLegalMoves) currentLegalMove = LegalMove.GetAllLegalMoves(this); //get the legal move of the new board
    
        return true;
    }

    public void Reset(string fenCode)
    {
        if(fenCode == null)
        {
            for (int i = 0; i < 64; i++)
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
        
        currentLegalMove = LegalMove.GetAllLegalMoves(this);
    }
    
    static public (PieceColor,PieceType) GetPiece(int value)
    {
        PieceColor pieceColor = (PieceColor)(value % 2);
        PieceType pieceType = (PieceType)(value - pieceColor);
        
        return(pieceColor, pieceType);
    }
}


