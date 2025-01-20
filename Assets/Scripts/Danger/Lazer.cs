using UnityEngine;

public class Lazer : MonoBehaviour
{
    private float timeTilSpawn;
    public float startTimeTilSpawn;

    public GameObject lazer;
    public Transform whereToSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeTilSpawn <=0)
        {
            Instantiate(lazer, whereToSpawn.position, whereToSpawn.rotation);

            timeTilSpawn = startTimeTilSpawn;
            Debug.Log("Laser");
            
        }
        else
        {
            timeTilSpawn -= Time.deltaTime;
        }

    }
}
