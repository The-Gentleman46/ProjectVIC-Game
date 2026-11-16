using UnityEngine;

public class Checker : MonoBehaviour
{
    [SerializeField]
    public Vector2Int coord; 
    public enum PieceType
    {
        Black = 0,
        White = 1
    }

    [SerializeField] public PieceType type;
    public int owner; // 0 for player 1 (Black), 1 for player 2 (White/Red)
    public bool isKing = false;
}
