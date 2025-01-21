using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    SpriteRenderer sr;
    int redColor, blueColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SwitchColors);
    }

    IEnumerator SwitchColors
    {
        
        sr.SetColor = SetColor(2f, 24f, 1f, 6f);
    }
}
