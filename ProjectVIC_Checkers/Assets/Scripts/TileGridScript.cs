using UnityEngine;
using System.Collections.Generic;


public class TileGridScript : MonoBehaviour
{
    public int numCol = 8;
    public int numRow = 8;
    public float tileWidth = 2.0f;
    public float tileHeight = 2.0f;
    public Tile tilePrefab;
    public Checker blackCheckerPrefab;
    public Checker whiteCheckerPrefab;

    private List<Tile> _tiles = new List<Tile>();

    void Start()
    {
        // Max Tiles = numCol * numRow
        _tiles.Capacity = numCol * numRow;
        for (int row = 0; row < numRow; row++)
            for (int col = 0; col < numCol; col++)
            {
                // Gets the position of x and y
                Vector3 pos = transform.position;
                pos.x += (col * tileWidth);
                pos.y += (row * tileHeight);

                // Instantiates a new Tile gameobject
                // Prefab, position, rotation, parent
                Tile tile = Instantiate(tilePrefab, pos, Quaternion.identity,transform);
                tile.coord = new Vector2Int(col, row);

                // Assigns colors
                tile.GetComponent<SpriteRenderer>().color = (col + row) % 2 == 0 ? Color.lightGray : Color.black;
                _tiles.Add(tile);

                // Checks if its the first 3 rows and alternates position
                if ((col + row) % 2 == 0 && tile.coord.y < 3)
                {
                    // Spawns in a black piece and stores all its data in the tile
                    Checker piece = Instantiate(blackCheckerPrefab, pos, Quaternion.identity, transform);
                    piece.coord = tile.coord;
                    piece.type = Checker.PieceType.Black;
                    piece.owner = 0;
                    tile.checker = piece;
                    tile.isOccupied = true;
                }

                // Checks if it is the last 3 rows and alternates position
                if ((col + row) % 2 == 0 && tile.coord.y > 4)
                {
                    // Same as black except its white pieces
                    Checker piece = Instantiate(whiteCheckerPrefab, pos, Quaternion.identity, transform);
                    piece.coord = tile.coord;
                    piece.type = Checker.PieceType.White;
                    piece.owner = 1;
                    tile.checker = piece;
                    tile.isOccupied = true;
                }
            }
    }
    // Used for getting the tile at a specific location from the list
    public Tile GetTile(int col, int row)
    {
        int i = row * numCol + col;
        return _tiles[i];
    }

}
