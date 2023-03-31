using UnityEngine;

public class Starly : MonoBehaviour
{
    Rigidbody2D myRigidbody;

    [SerializeField] private float movementDistanceX;
    [SerializeField] private float movementDistanceY;

    private bool movingLeft;
    private bool movingUp;

    private float leftEdge;
    private float rightEdge;
    private float upEdge;
    private float downEdge;

    float speed;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistanceX;
        rightEdge = transform.position.x + movementDistanceX;
        upEdge = transform.position.y + movementDistanceY;
        downEdge = transform.position.y - movementDistanceY;
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

        if (movingUp)
        {
            if (transform.position.y <= upEdge)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            }
            else
            {
                movingUp = false;
            }
        }
        else
        {
            if (transform.position.y >= downEdge)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
            }
            else
            {
                movingUp = true;
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
