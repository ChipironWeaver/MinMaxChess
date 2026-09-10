using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class LegalMove 
{
    static public int[] GetLegalMove(int[] grid, int position)
    {
        PieceType piece = (PieceType)(grid[position] - grid[position]%2);
        
        switch(piece)
        {
            case PieceType.Rook:
                return GetRookLegalMove(grid, position);
            case PieceType.Bishop:
                return GetBishopLegalMove(grid, position);
            case PieceType.Queen:
                return GetQueenLegalMove(grid, position);
            default:
            {
                Debug.LogWarning("No legal move for position " + position + " for " + grid[position].ToString());
                return new int[64];
            }
        }
    }


    static public int[] GetRookLegalMove(int[] grid, int position)
    {
        int[] legalMoves = new int[64];
        PieceColor color = (PieceColor)(grid[position] - PieceType.Rook);
        

        for (int i = position; i != position - position % 8 - 1; i--)
        {
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        
        for (int i = position; i != position - position % 8 + 8; i++)
        {
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        
        for (int i = position; i >= 0; i -= 8)
        {
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        
        for (int i = position; i < 64; i += 8)
        {
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        
        return legalMoves;
    }
    static public int[] GetBishopLegalMove(int[] grid, int position)
    {
        int[] legalMoves = new int[64];
        PieceColor color = (PieceColor)(grid[position] - PieceType.Rook);
        
        for (int i = position; i >= 0; i -= 9)
        {
            if (i % 8 == 7) break;
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        for (int i = position; i >= 0; i -= 7)
        {
            if (i % 8 == 0) break;
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        for (int i = position; i < 64; i += 7)
        {
            if (i % 8 == 7) break;
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        for (int i = position; i < 64; i += 9)
        {
            if (i % 8 == 0) break;
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        
        return legalMoves;
    }
    static public int[] GetQueenLegalMove(int[] grid, int position)
    {
        int[] legalMoves = new int[64];
        PieceColor color = (PieceColor)(grid[position] - PieceType.Rook);
        
        for (int i = position; i >= 0; i -= 9)
        {
            if (i % 8 == 7) break;
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        for (int i = position; i >= 0; i -= 7)
        {
            if (i % 8 == 0) break;
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        for (int i = position; i < 64; i += 7)
        {
            if (i % 8 == 7) break;
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        for (int i = position; i < 64; i += 9)
        {
            if (i % 8 == 0) break;
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        
        for (int i = position; i != position - position % 8 - 1; i--)
        {
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        
        for (int i = position; i != position - position % 8 + 8; i++)
        {
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        
        for (int i = position; i >= 0; i -= 8)
        {
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        
        for (int i = position; i < 64; i += 8)
        {
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        
        return legalMoves;
    }
}
