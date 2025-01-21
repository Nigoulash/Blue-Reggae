using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    SpriteRenderer sr;
    Color newColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

//    // Update is called once per frame
//    void Update()
//    {
//        StartCoroutine(SwitchColors);
//    }

//    IEnumerator SwitchColors
//    {
//        sr.color = Color(2f, 24f, 1f, 6f);
//    }
}
