using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public bool changeVisual;
    public GameState gameState = new GameState();
    public string fenCode;
    public float backgroundStrengh;
    public bool checkForColor = true;
    
    
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
        gameState.Reset(fenCode);
        
        PieceManager.Instance.RenderNewBoard(gameState.grid);
        
        if (Camera.main != null)
            Camera.main.DOColor(Color.Lerp(gameState.gridInfo[0] == (int)PieceColor.White
                ? GridRenderer.Instance.evenColor
                : GridRenderer.Instance.oddColor, Color.black, backgroundStrengh) , 0.5f);
    }

    public bool MovePiece((int x, int y) piece, bool trust = false, bool movePiece = false)
    {
        bool didMove = gameState.MovePiece(piece, trust);
        if (Camera.main != null & changeVisual & didMove)
            Camera.main.DOColor(Color.Lerp(gameState.gridInfo[0] == (int)PieceColor.White
                ? GridRenderer.Instance.evenColor
                : GridRenderer.Instance.oddColor, Color.black, backgroundStrengh) , 0.5f);
        
        return didMove;
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