using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isDead = false;
    public static bool isNearLedge = false;
    public static bool grabbingLedge = false;

    public static bool isNearSlider = false;
    public static bool grabbingSlider = false;

    public static string hook;

    public static string slider;

    public static Vector2 startPosition;
    public static bool artifactGrabbed = false;

    [SerializeField] GameObject background;



    void Update()
    {
        if (artifactGrabbed)
        {
            background.SetActive(true);
        }

    }
}
