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
            if (Input.GetKeyDown(KeyCode.H))
            {
                player.transform.position = hookDestination.position;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hook")
        {
                isNearLedge = true;
                hookDestination = other.transform;
        }
    }
}
