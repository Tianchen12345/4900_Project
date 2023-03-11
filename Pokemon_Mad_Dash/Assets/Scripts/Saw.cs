using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float Sawspeed;

    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    // Start is called before the first frame update
    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingLeft)
        {
            if(transform.position.x > leftEdge)
            {
                transform.position = new Vector2(transform.position.x - Sawspeed * Time.deltaTime, transform.position.y);
            }
            else{ movingLeft = false; }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector2(transform.position.x + Sawspeed * Time.deltaTime, transform.position.y);
            }
            else { movingLeft = true; }
        }
    }
}
