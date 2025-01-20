using UnityEngine;

public class LedgeGrabber : MonoBehaviour
{        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hook"))
        {
            GameManager.isNearLedge = true;
            Debug.Log("hook detected");
            if (Input.GetKey(KeyCode.G))
            {
                GameManager.grabbingLedge = true;
                GameManager.hookDestination = other.transform.position;
            }
            else
            {
                GameManager.grabbingLedge = false;
            }
        }

        else
        {
            GameManager.isNearLedge = false;
        }
    }
}
