using UnityEngine;

public class SliderGrabber : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isNearSlider)
        {
            GameManager.grabbingSlider = false;

        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Slider"))
        {
            GameManager.isNearSlider = true;
            if (Input.GetKey(KeyCode.H) && GameManager.isNearSlider)
            {
                GameManager.grabbingSlider = true;
                GameManager.slider = other.gameObject.name;
                Debug.Log(GameManager.slider + " detected");
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Slider"))
        {
            GameManager.isNearSlider = false;
        }
    }
}
