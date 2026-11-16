using TMPro;
using UnityEngine;

public class PieceCapture : MonoBehaviour
{
    public TileMovement tileMovement;
    public void CaptureRedPiece(int whiteCaptureCount)
    {
        GetComponent<TextMeshProUGUI>().text = "Red Captures: " + whiteCaptureCount;
    }

    public void CaptureBlackPiece(int blackCaptureCount)
    {
        GetComponent<TextMeshProUGUI>().text = "Black Captures: " + blackCaptureCount;
    }
}
