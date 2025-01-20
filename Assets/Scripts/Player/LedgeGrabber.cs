using UnityEngine;

public class LedgeGrabber : MonoBehaviour
{
    [SerializeField] GameObject player;
    Transform hookDestination;
    [SerializeField] bool isNearLedge = false;

        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isNearLedge)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (hookDestination.position.y > player.transform.position.y - 2)
                {
                    player.transform.position = new Vector2(hookDestination.position.x - 1, hookDestination.position.y + 3);
                }

                if (hookDestination.position.y < player.transform.position.y - 2)
                {
                    player.transform.position = new Vector2(hookDestination.position.x - 1, hookDestination.position.y - 2);
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hook"))
        {
            isNearLedge = true;
            hookDestination = other.transform;

        }

        else
        {
            isNearLedge = false;
        }
    }
}
