using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float speed;
    public float minSpeed = 2f;
    public float maxSpeed = 8f;
    public float minX = 0f;
    public float maxX = 50f;

    private void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (transform.position.x > maxX)
        {
            Vector2 newPos = new Vector2(minX, transform.position.y);
            transform.position = newPos;
        }
    }
}
