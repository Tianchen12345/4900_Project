using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private GameObject gameOverScreen;

    private RectTransform myRectTransform;
    private int currentPosition;

    private void Awake()
    {
        myRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {   
        //change position of selection arrow
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);
        }

        //interact with options
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void ChangePosition(int i)
    {
        currentPosition += i;
        if(currentPosition < 0)
        {
            currentPosition = options.Length - 1;
        }        
        if(currentPosition > options.Length - 1)
        {
            currentPosition = 0;
        }

        myRectTransform.position = new Vector2(myRectTransform.position.x, options[currentPosition].position.y);
    }
    private void Interact()
    {
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
