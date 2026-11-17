using UnityEngine;

public class Tile : MonoBehaviour
{
    // still x, y, just as an int
    public Vector2Int coord;

    // Indicates whether the tile is currently occupied by a checker piece
    public bool isOccupied;

    // Reference to the checker piece occupying this tile (if any)  
    public Checker checker;
}

