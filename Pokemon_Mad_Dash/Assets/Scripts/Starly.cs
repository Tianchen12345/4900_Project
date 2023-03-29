using UnityEngine;

public class Starly : MonoBehaviour
{
    Rigidbody2D myRigidbody;

    [SerializeField] private float movementDistanceX;
    [SerializeField] private float movementDistanceY;

    private bool movingLeft;

    private float leftEdge;
    private float rightEdge;

    float speed;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistanceX;
        rightEdge = transform.position.x + movementDistanceX;
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        speed = GetComponent<Enemies>().speed;
        FlipSprites();
    }

    // Update is called once per frame
    void Update()
    {
        speed = GetComponent<Enemies>().speed;

        if (movingLeft)
        {
            if (transform.position.x >= leftEdge)
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            }
            else
            {
                movingLeft = false;
                FlipSprites();
            }
        }
        else
        {
            if (transform.position.x <= rightEdge)
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            }
            else
            {
                movingLeft = true;
                FlipSprites();
            }
        }
    }

    private void FlipSprites()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
