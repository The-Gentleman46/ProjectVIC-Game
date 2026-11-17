using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PieceClick : MonoBehaviour
{
    // InputAction that is triggered when the mouse is clicked
    public InputAction clickAction;
    public TileGridScript tileGrid;

    // Event that is fired when we click on a tile
    public UnityEvent<Tile> OnPieceClicked;

    // Subscribe to the clickAction when the script is enabled and unsubscribe when disabled
    void Start()
    {
        clickAction.Enable();
        clickAction.performed += ClickAction_Performed;
    }

    private void ClickAction_Performed(InputAction.CallbackContext obj)
    {
        /* Gives the position of the mouse in world space. Camera.main just finds the first camera
         * with "Main" in it
         */
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.nearClipPlane;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        /* Returns if the mouse is overlapping another collider. OverlapPointAll gets all 
         * colliders, not a specific one
         */
        Collider2D[] col = Physics2D.OverlapPointAll(mousePos);

        // If the collider array isn't empty
        if (col.Length > 1)
        {
            // Get the Tile component from the first collider to have the "Tile" tag
            Tile tile = null;
            foreach(Collider2D collider in col)
            {
                if (collider.CompareTag("Tile"))
                {
                    tile = collider.GetComponent<Tile>();
                }
            }

            // Stores the x and y coord of the tile
            int xcoord = tile.coord.x;
            int ycoord = tile.coord.y;
            tile = tileGrid.GetTile(xcoord, ycoord);

            // Checks if there is a checker piece on the tile
            if (tile.checker != null)
            {
                // Gets that specific piece on the tile
                Checker checker = tile.checker;
                GameObject piece = checker.gameObject;
                
                // If the piece is properly formatted
                if (piece != null)
                {
                    // If its black
                    if (checker.type == Checker.PieceType.Black)
                    {
                        OnPieceClicked?.Invoke(tile);
                    }

                    // If its white
                    else if (checker.type == Checker.PieceType.White)
                    {
                        OnPieceClicked?.Invoke(tile);                      
                    }
                }
            }
        }
        // If there is only one collider (meaning only a tile)
        else if (col.Length == 1)
        {
            // Get that tile and check if you are able to move to it
            Tile tile = col[0].GetComponent<Tile>();
            int xcoord = tile.coord.x;
            int ycoord = tile.coord.y;
            tile = tileGrid.GetTile(xcoord, ycoord);
            if (tile.GetComponent<SpriteRenderer>().color == Color.yellow)
            {
                OnPieceClicked?.Invoke(tile);
            }
            
        }
        
        
        /*Unused code that is being used for reference
        * else if (turn == 1)
        * {
        *     if (col != null)
        *     {
        *         // Get the Tile component from the first collider to get coordinates
        *         Tile clickedTile = col[0].GetComponent<Tile>();
        * 
        *         if (clickedTile != null)
        *         {
        *             int xcoord = clickedTile.coord.x;
        *             int ycoord = clickedTile.coord.y;
        * 
        *             Tile tile = tileGrid.GetTile(xcoord, ycoord);
        *             renderer = tile.GetComponent<SpriteRenderer>();
        * 
        *             if (tile != null)
        *             {
        *                 Color color = renderer.color;
        * 
        *                 // If there's more than one collider, something is on top of the tile
        *                 if (col.Length > 1)
        *                 {
        *                     renderer.color = Color.green;
        *                     Debug.Log("Multiple objects detected - turning tile green");
        *                 }
        *                 else if (color == Color.darkGray)
        *                 {
        *                     OnWhiteClicked?.Invoke(tile);
        *                     Debug.Log("Moving here");
        *                 }
        *                 else if (color == Color.lightGray)
        *                 {
        *                     Debug.Log("Can't move here");
        *                 }
        *             }
        *         }
        *     }
        * }
        */
    }

}
