using UnityEngine;

public class SmoothCamera2D : MonoBehaviour
{
    [SerializeField] float _dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] Transform _target;

    // Update is called once per frame
    void Update()
    {
        if (_target)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(_target.position);
            Vector3 delta = _target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, _dampTime);
        }

    }
}