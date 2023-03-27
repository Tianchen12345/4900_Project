using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    [SerializeField] private float movementDistanceX;
    [SerializeField] private float movementDistanceY;
    [SerializeField] private float speed;

    private bool movingLeft;
    private bool movingDown;
    private float leftEdge;
    private float rightEdge;
    private float topEdge;
    private float buttonEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistanceX;
        rightEdge = transform.position.x + movementDistanceX;
        topEdge = transform.position.y + movementDistanceY;
        buttonEdge = transform.position.y - movementDistanceY;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            }
            else { movingLeft = false; }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            }
            else { movingLeft = true; }
        }

        if (movingDown)
        {
            if (transform.position.y > buttonEdge)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
            }
            else { movingDown = false; }
        }
        else
        {
            if (transform.position.y < topEdge)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            }
            else { movingDown = true; }
        }
    }
}
