using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// Wanna rotate the camera 180 degrees when its player 2's turn and back to change perspective
public class TileMovement : MonoBehaviour
{
    // 0 for player 1, 1 for player 2
    [SerializeField] int turn = 0;

    // Reference to the PieceClick script for handling piece clicks
    public PieceClick clicker;

    // Reference to the TileGridScript for accessing the grid of tiles
    public TileGridScript tileGrid;

    // References to the PieceCapture scripts for updating captured piece counts
    public PieceCapture pieceCapture;
    public PieceCapture pieceCapture2;

    // UI text to display current player's turn
    public TMP_Text turnText;

    // The currently selected tile and the tile that was clicked
    private Tile _currentTile;
    private Tile _selectedTile;

    // List of tiles that are valid moves for the selected piece
    private List<Tile> _validMoveTiles = new List<Tile>();

    // Store the original color of the tile to restore it after highlighting
    private Color _originalColor;

    // Capture counts for each player
    public int whiteCaptureCount = 0;
    public int blackCaptureCount = 0;

    /* Subscribe to the OnPieceClicked event when the script is enabled and unsubscribe when 
     * disabled
     */
    private void OnEnable()
    {
        clicker.OnPieceClicked.AddListener(OnTileClicked);
    }

    // Unsubscribe from the OnPieceClicked event when the script is disabled
    private void OnDisable()
    {
        clicker.OnPieceClicked.RemoveListener(OnTileClicked);
    }

    // When a tile is clicked, check if it's a valid move or if it's the player's own piece
    private void OnTileClicked(Tile clickedTile)
    {
        if (_selectedTile != null && _validMoveTiles.Contains(clickedTile))
        {
            MovePiece(_selectedTile, clickedTile);
            ClearHighlights();
            return;
        }

        _currentTile = clickedTile;

        /* If the clicked tile is occupied and belongs to the current player, highlight any valid
         * moves in yellow
         */
        if (_currentTile.isOccupied && _currentTile.checker.owner == turn)
        {
            ClearHighlights();

            _selectedTile = _currentTile;

            _validMoveTiles = GetValidMoves(_currentTile);

            foreach (Tile tile in _validMoveTiles)
            {
                tile.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }
    }

    // Get all valid moves for the selected piece, including jumps over opponent pieces
    private List<Tile> GetValidMoves(Tile currentTile)
    {
        // To make multi-jumps availible, make a recursive function that has the current tile, a list of tiles that you can move to, 
        // and a hashset of tiles to mark what pieces were captured
        List<Tile> validMoves = new List<Tile>();
        Checker piece = currentTile.checker;
        int col = currentTile.coord.x;
        int row = currentTile.coord.y;
        int left = -1;
        int right = 1;

        // For regular pieces: only move forward
        // For kings: move forward AND backward
        int[] directions = piece.isKing 
            ? new int[] { -1, 1 } 
            : new int[] { piece.type == Checker.PieceType.Black 
            ? 1 
            : -1 };

        foreach (int direction in directions)
        {
            Tile[] targetTiles = {

                getNextTile(currentTile, left, direction),
                getNextTile(currentTile, right, direction)

                };
            foreach (Tile tile in targetTiles)
            {
                // If not occupied, then its a space directly next to the current one and allows for movement there
                if (!tile.isOccupied)
                {
                    validMoves.Add(tile);
                }
                // If its occupied and t
                else if (tile.checker.owner != piece.owner && tile.coord.x < currentTile.coord.x)
                {
                    Tile newTile = getNextTile(tile, left, (direction < 0) ? direction: direction);
                    
                    if (!newTile.isOccupied)
                        validMoves.Add(newTile);

                    
                }
                else if(tile.checker.owner != piece.owner && tile.coord.x > currentTile.coord.x)
                {
                    Tile newTile = getNextTile(tile, right, (direction < 0) ? direction : direction);

                    if(!newTile.isOccupied)
                        validMoves.Add(newTile);

                }
                
            }
            
        }

        return validMoves;
    }

    private Tile getNextTile(Tile tile, int directionX, int directionY)
    {

        int col = tile.coord.x + directionX;
        int row = tile.coord.y + directionY;
        
        Tile moveTile = tileGrid.GetTile(col, row);

        if (col >= 0 && col < tileGrid.numCol && row >= 0 && row < tileGrid.numRow)
            return moveTile;
        return null;
    }

    /* Move the piece from the selected tile to the target tile, handle captures, and check for 
     * promotion to king
     */
    private void MovePiece(Tile fromTile, Tile toTile)
    {
        Checker piece = fromTile.checker;
        
        // Check if this is a capture move (more than 1 tile away)
        int colDifference = Mathf.Abs(toTile.coord.x - fromTile.coord.x);
        
        if (colDifference == 2)
        {
            // This is a jump - calculate the captured piece's position
            int capturedCol = (fromTile.coord.x + toTile.coord.x) / 2;
            int capturedRow = (fromTile.coord.y + toTile.coord.y) / 2;
            Tile capturedTile = tileGrid.GetTile(capturedCol, capturedRow);

            if (capturedTile.checker.gameObject.CompareTag("WhitePiece"))
            {
                whiteCaptureCount++;
                pieceCapture.CaptureRedPiece(whiteCaptureCount);
                if (whiteCaptureCount == 12)
                {
                    SceneManager.LoadScene("GameOver");
                }
            }
            else if (capturedTile.checker.gameObject.CompareTag("BlackPiece"))
            {
                blackCaptureCount++;
                pieceCapture2.CaptureBlackPiece(blackCaptureCount);
                if (blackCaptureCount == 12)
                {
                    SceneManager.LoadScene("GameOver");
                }
            }

            // Delete the captured piece
            Destroy(capturedTile.checker.gameObject);
            capturedTile.checker = null;
            capturedTile.isOccupied = false;
        }
        
        // Move the piece
        fromTile.checker = null;
        fromTile.isOccupied = false;
        
        toTile.checker = piece;
        toTile.isOccupied = true;
        
        // Update piece position and coordinates
        piece.coord = toTile.coord;
        piece.transform.position = toTile.transform.position;

        // Checking for promotion to king
        if (piece.type == Checker.PieceType.Black && toTile.coord.y == tileGrid.numRow - 1)
        {
            PromoteToKing(piece);
            piece.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("kingSprite"); 
        }
        else if (piece.type == Checker.PieceType.White && toTile.coord.y == 0)
        {
            PromoteToKing(piece);
            piece.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("kingSprite"); 
        }
        
        // Change turn
        turn = turn == 0 ? 1 : 0;
        if (turn == 0)
        {
            turnText.color = Color.blue;
            turnText.text = "Blue's Turn";
        }
        else if (turn == 1) 
        {
            turnText.color = Color.red;
            turnText.text = "Red's Turn";
        }
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, turn * 180);
    }

    /* Checks if a piece should be promoted to a king and updates its status and sprite 
     * accordingly
     */
    
    private void PromoteToKing(Checker piece)
    {
        piece.isKing = true;
        
    }

    /* Clear the highlights from the previously highlighted valid move tiles and reset the
     * selected tile
     */
    private void ClearHighlights()
    {
        foreach (Tile tile in _validMoveTiles)
        {
            // Restore original tile color based on checkerboard pattern
            int sum = tile.coord.x + tile.coord.y;
            tile.GetComponent<SpriteRenderer>().color = sum % 2 == 0 ? Color.lightGray : 
                Color.black;
        }
        
        _validMoveTiles.Clear();
        _selectedTile = null;
    }

    // Check if the current player has any valid moves left; if not, the game is over
    private bool HasValidMoves(int playerTurn)
    {
        for (int col = 0; col < tileGrid.numCol; col++)
        {
            for (int row = 0; row < tileGrid.numRow; row++)
            {
                Tile tile = tileGrid.GetTile(col, row);
                
                if (tile.isOccupied && tile.checker.owner == playerTurn)
                {
                    List<Tile> validMoves = GetValidMoves(tile);
                    if (validMoves.Count > 0)
                    {
                        return true;
                    }
                }
            }
        }
        
        return false;
    }

}
