using System.Collections.Generic;
using UnityEngine;

public static class LegalMove 
{
    private static readonly (int,int)[] KingMoves = { (-1,-1) , (-1,-0) , (-1,1) , (0,-1) , (0,1), (1,-1) , (1,-0) , (1,1) };
    private static readonly (int,int)[] KnightMoves = { (-2,-1) , (-2,1) , (-1,-2) , (-1,2) , (2,-1) , (2,1) , (1,-2) , (1,2)};
    private static readonly int[] PawnMove = {7,9};
    
    private static readonly int[] BigCastling = {-1,-2,-3};
    private static readonly int[] SmallCastling = {1,2};
    static public int[] GetLegalMove(GameState gameState, int position, bool checkForCheck)
    {
        PieceType piece = (PieceType)(gameState.grid[position] - gameState.grid[position]%2);
        int[] legalMoves = null;
        switch(piece)
        {
            case PieceType.Rook:
                legalMoves = GetRookLegalMove(gameState.grid, position);
                break;
            case PieceType.Bishop:
                legalMoves = GetBishopLegalMove(gameState.grid, position);
                break;
            case PieceType.Queen:
                legalMoves = GetQueenLegalMove(gameState.grid, position);
                break;
            case PieceType.King:
                legalMoves = GetKingLegalMove(gameState.grid, gameState.gridInfo, position);
                break;
            case PieceType.Knight:
                legalMoves = GetLegalMoveFromArray(gameState.grid, position,KnightMoves);
                break;
            case PieceType.Pawn:
                legalMoves = GetPawnLegalMove(gameState.grid,gameState.gridInfo, position);
                break;
        }


        return checkForCheck ? CheckForCheck(legalMoves, gameState, position) :  legalMoves;
    }
    static public Dictionary<int, int[]> GetAllLegalMoves(GameState gameState, bool checkForCheck = true)
    {
        Dictionary<int, int[]> dic =  new Dictionary<int, int[]>();

        int color = gameState.gridInfo[0];
        for (int i = 0; i < 64; i++)
        {
            if (gameState.grid[i] % 2 == color)
            {
                dic.Add(i,GetLegalMove(gameState, i,false));
            }
        }
        
        return checkForCheck ? CheckForCheck(dic, gameState) : dic;
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
            if (i % 8 == 7 && i != position) break;
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        for (int i = position; i >= 0; i -= 7)
        {
            if (i % 8 == 0 && i != position) break;
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        for (int i = position; i < 64; i += 7)
        {
            if (i % 8 == 7 && i != position) break;
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2; 
                
                break;
            }
            legalMoves[i] = 1;
        }
        for (int i = position; i < 64; i += 9)
        {
            if (i % 8 == 0 && i != position) break;
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
            if (i % 8 == 7 && i != position) break;
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        for (int i = position; i >= 0; i -= 7)
        {
            if (i % 8 == 0 && i != position) break;
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2;
                
                break;
            }
            legalMoves[i] = 1;
        }
        for (int i = position; i < 64; i += 7)
        {
            if (i % 8 == 7 && i != position) break;
            if (grid[i] != -1 && i != position)
            {
                if(grid[i] % 2 != (int)color) legalMoves[i] = 2; 
                
                break;
            }
            legalMoves[i] = 1;
        }
        for (int i = position; i < 64; i += 9)
        {
            if (i % 8 == 0 && i != position) break;
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
    static public int[] GetPawnLegalMove(int[] grid,int[] gridInfo ,int position)
    {
        int[] legalMoves = new int[64];
        PieceColor color = (PieceColor)(grid[position] - PieceType.Pawn);
        int height = position/8;
        int direction = PieceColor.White == color ? -1 : 1;
        
        if (position + 8 * direction is > 0 and < 64)
        {
            if (grid[position + 8 * direction] == -1)
            {
                legalMoves[position + 8 * direction] = 1;
                if (gridInfo[1] > -1)
                {
                    if(gridInfo[1]/8 == height )
                    {
                        if (Mathf.Abs(gridInfo[1] - position) == 1)
                        {
                            legalMoves[gridInfo[1] + 8 * direction] = 3;
                        }
                    } 
                }
                if (height == (color == PieceColor.White ? 6 : 1) && position + 16 * direction is > 0 and < 64 )
                {
                    if (grid[position + 16 * direction ] == -1) legalMoves[position + 16 * direction ] = 1;
                }
            }
        }

        foreach (int i in PawnMove)
        {
            int f = i * direction + position;
            if (f is > 0 and < 64 && f / 8 == height + direction)
            {
                if(grid[f] % 2 != (int)color && grid[f] != -1) legalMoves[f] = 2;
            }
        }
        return legalMoves;
    }
    static public int[] GetKingLegalMove(int[] grid, int[] gridInfo, int position)
    {
        int[] legalMoves =  GetLegalMoveFromArray(grid,position,KingMoves);
        
        if (grid[position] % 2 == (int)PieceColor.White)
        {
            if (gridInfo[2] % 2 == 1)
            {
                Debug.Log("castling 1");
                legalMoves[position - 2] = 4;
                foreach (int i in BigCastling)
                {
                    if (grid[position + i] != -1)
                    {
                        Debug.Log("cancel castling");
                        legalMoves[position - 2] = 0;
                        break;
                    }
                }
            }
            if (gridInfo[3] % 2 == 1)
            {
                Debug.Log("castling 2");
                legalMoves[position + 2] = 4;
                foreach (int i in SmallCastling)
                {
                    if (grid[position + i] != -1) legalMoves[position + 2] = 0;
                }
            }
        }
        else
        {
            if (gridInfo[2] / 2 == 1)
            {
                Debug.Log("castling 3");
                legalMoves[position - 2] = 4;
                foreach (int i in BigCastling)
                {
                    if (grid[position + i] != -1) legalMoves[position - 2] = 0;
                }
            }
            if (gridInfo[3] / 2 == 1)
            {
                Debug.Log("castling 4");
                legalMoves[position + 2] = 4;
                foreach (int i in SmallCastling)
                {
                    if (grid[position + i] != -1) legalMoves[position + 2] = 0;
                }
            }
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

    static public Dictionary<int, int[]> CheckForCheck(Dictionary<int, int[]> moves, GameState gameState)
    {
        int kingPos = -1;
        for(int i = 0 ; i < 64; i++)
        {
            if (gameState.grid[i] == (int)PieceType.King + gameState.gridInfo[0])
            {
                kingPos = i;
                break;
            }
        }
        
        foreach (int key in moves.Keys)
        {
            moves[key] = CheckForCheck(moves[key], gameState,key, kingPos);
        }

        return moves;
    }

    static public int[] CheckForCheck(int[] move, GameState gameState,int postion, int kingPosition = -1)
    {
        bool isKingMoving = gameState.grid[postion] == (int)PieceType.King + gameState.gridInfo[0];
        if(kingPosition == -1 && !isKingMoving) for(int i = 0 ; i < 64; i++)
        {
            if (gameState.grid[i] == (int)PieceType.King + gameState.gridInfo[0])
            {
                kingPosition = i;
                break;
            }
        }

        List<int> sortedMove = new List<int>();
        
        foreach (int i in move)
        {
            GameState tempState = gameState;
            if (tempState.MovePiece((postion, i), true ,false))
            {
                Dictionary<int, int[]> tempCheck = GetAllLegalMoves(tempState, false);
                
                foreach (int[] val in tempCheck.Values) foreach (int pos in val) 
                    if (pos != (isKingMoving ? i : kingPosition)) sortedMove.Add(i);
            }
        }
        
        return sortedMove.ToArray();
    }
    
}