using UnityEngine;

public class ArtifactGrabber : MonoBehaviour
{
    [SerializeField] Transform _hookDetector;
    [SerializeField] LayerMask artifactMask;
    bool isNearArtifact = false;
    [SerializeField] GameObject artifact;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isNearArtifact = Physics2D.OverlapCapsule(_hookDetector.position, new Vector2(3f, 6f), CapsuleDirection2D.Vertical, 0, artifactMask);

        if (isNearArtifact && Input.GetKeyDown(KeyCode.T))
        {
            Object.Destroy(artifact);
            GameManager.artifactGrabbed = true;
        }

    }
}
