using TMPro;
using UnityEngine;

public class PieceCapture : MonoBehaviour
{
    public TileMovement tileMovement;

    //Displays the number of captured red pieces
    public void CaptureRedPiece(int whiteCaptureCount)
    {
        GetComponent<TextMeshProUGUI>().text = "Red Captures: " + whiteCaptureCount;
    }

    //Displays the number of captured black pieces
    public void CaptureBlackPiece(int blackCaptureCount)
    {
        GetComponent<TextMeshProUGUI>().text = "Black Captures: " + blackCaptureCount;
    }
}
