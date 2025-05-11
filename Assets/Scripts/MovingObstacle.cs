using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float moveDistance = 3f;
    public float speed = 2f;

    private Vector3 startPos;
    private Vector3 direction;

    void Start()
    {
        startPos = transform.position;
        // Pick a random direction: x or z axis
        if (Random.value > 0.5f)
            direction = Vector3.right;
        else
            direction = Vector3.forward;

        // Random flip
        if (Random.value > 0.5f)
            direction *= -1;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * moveDistance;
        transform.position = startPos + direction * offset;
    }
}

