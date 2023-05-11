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
        // The speed of the cloud is randomized within the range of minSpeed and maxSpeed.
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // If the cloud reaches the maxX position, it teleports to the minX position, allowing it to loop around the screen.
        if (transform.position.x > maxX)
        {
            Vector2 newPos = new Vector2(minX, transform.position.y);
            transform.position = newPos;
        }
    }
}
