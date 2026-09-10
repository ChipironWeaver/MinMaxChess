using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class LegalMove 
{
    private static readonly (int,int)[] KingMoves = { (-1,-1) , (-1,-0) , (-1,1) , (0,-1) , (0,1), (1,-1) , (1,-0) , (1,1) };
    private static readonly (int,int)[] KnightMoves = { (-2,-1) , (-2,1) , (-1,-2) , (-1,2) , (2,-1) , (2,1) , (1,-2) , (1,2)};
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
            case PieceType.King:
                return GetLegalMoveFromArray(grid, position,KingMoves);
            case PieceType.Knight:
                return GetLegalMoveFromArray(grid, position,KnightMoves);
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
        PieceColor color = (PieceColor)(grid[position] - PieceType.Bishop);
        
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
        PieceColor color = (PieceColor)(grid[position] - PieceType.Queen);
        
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


    static public int[] GetLegalMoveFromArray(int[] grid, int position, (int,int)[] moveArray)
    {
        int[] legalMoves = new int[64];
        PieceColor color = (PieceColor)(grid[position]%2);

        foreach ((int i,int f) in moveArray)
        {
            
            int pos = i + position + f * 8;

            if(pos is < 0 or >= 64 ) continue;
            if(pos/8 - position/8 != f) continue;
            if (grid[pos] != -1 && i != position)
            {
                if(grid[pos] % 2 != (int)color) legalMoves[pos] = 2;
                continue;
            }
            legalMoves[pos] = 1;
        }
        return legalMoves;
    }
}
