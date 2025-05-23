using UnityEngine;

public class LazerMovement : MonoBehaviour
{

    public LayerMask layersToHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 50f, layersToHit);
        if(hit.collider == null)
        {
            transform.localScale = new Vector3(50f, 1, 1);
            return;


        }
        transform.localScale = new Vector3(hit.distance, transform.localScale.y, 1);
        Debug.Log(hit.collider.gameObject.name);
        if(hit.collider.tag == "Player")
        {
            Destroy(hit.collider.gameObject);
        }


    }
}
