using UnityEngine;

public class LedgeGrabber : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isNearLedge)
        {
            GameManager.grabbingLedge = false;

        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hook"))
        {
            GameManager.isNearLedge = true;
            if (Input.GetKey(KeyCode.G) && GameManager.isNearLedge)
            {
                GameManager.grabbingLedge = true;
                GameManager.hook = other.gameObject.name;
                Debug.Log(GameManager.hook + " detected");
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hook"))
        {
            GameManager.isNearLedge = false;
        }
    }
}
