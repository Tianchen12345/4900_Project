using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D myRigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        PrintingToConsole();
    }



    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        OutOfBoundsMessage();
    }

    public string PrintingFromOutside(int i)
    {
        return "the number you enter is " + i;
    }

    private void OutOfBoundsMessage()
    {
        if (transform.position.x > 9.5f)
        {
            Debug.LogWarning("The player is out of bounds to the right side!!!");
        }
        else if (transform.position.x < -9.5f)
        {
            Debug.LogWarning("The player is out of bounds to the left side!!!");
        }
        else if (transform.position.y > 5.5f)
        {
            Debug.LogWarning("The player is out of bounds to the upper side!!!");
        }
    }

    private void MovePlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody2d.velocity = new Vector2(0f, 10f);
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            myRigidbody2d.velocity = new Vector2(10f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            myRigidbody2d.velocity = new Vector2(-10f, 0f);
        }
    }

    private static void PrintingToConsole()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("space key was pressed");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            print("DownArrow(s) key was pressed");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            print("UpArrow key(w)  was pressed");
        }
    }
}
