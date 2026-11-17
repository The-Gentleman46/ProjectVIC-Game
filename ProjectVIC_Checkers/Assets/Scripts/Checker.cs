using UnityEngine;

public class Checker : MonoBehaviour
{
    [SerializeField]
    public Vector2Int coord;

    // Enum to represent the type of piece (Black or White)
    public enum PieceType
    {
        Black = 0,
        White = 1
    }

    // The type of the piece (Black or White)
    [SerializeField] public PieceType type;

    // 0 for player 1 (Black), 1 for player 2 (White/Red)
    public int owner;

    // Indicates if the piece has been promoted to a king
    public bool isKing = false;

    
}
