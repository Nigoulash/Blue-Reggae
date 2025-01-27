using System.Collections;
using UnityEngine;

public class ArtifactGrabber : MonoBehaviour
{
    [SerializeField] Transform _hookDetector;
    [SerializeField] LayerMask artifactMask;
    bool isNearArtifact = false;
    [SerializeField] GameObject artifact;
    [SerializeField] GameObject overlay;
    [SerializeField] GameObject alarmText;
    [SerializeField] GameObject completed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        overlay.SetActive(false);
        alarmText.SetActive(false);
        completed.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        isNearArtifact = Physics2D.OverlapCapsule(_hookDetector.position, new Vector2(3f, 6f), CapsuleDirection2D.Vertical, 0, artifactMask);

        if (isNearArtifact && Input.GetKeyDown(KeyCode.T))
        {
            Object.Destroy(artifact);
            GameManager.artifactGrabbed = true;
            overlay.SetActive(true);
            alarmText.SetActive(true);
            StartCoroutine("Success");
        }

    }
    IEnumerator Success()
    {
        yield return new WaitForSeconds(2);
        GameManager.canMove = false;
        completed.SetActive(true);
    }
}
