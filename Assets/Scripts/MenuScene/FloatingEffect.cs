using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    public float amplitude = 0.1f; 
    public float speed = 0.1f;    
    private Vector3 startPosition; 

    void Start()
    {
        // Save the start position
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate new Y-position
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;

        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
