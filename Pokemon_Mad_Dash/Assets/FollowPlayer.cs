using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player1;
    public GameObject player2;
    public bool isChar1 = false;
    public bool isChar2 = true;

    // Update is called once per frame
    void Update()
    {
      if(isChar1)
      {
        transform.position= new Vector3(player1.transform.position.x,transform.position.y,transform.position.z);
      }
      if(isChar2)
      {
        transform.position= new Vector3(player2.transform.position.x,transform.position.y,transform.position.z);
      }


  }
  public void isSelected(){
    isChar1= true;
    isChar2= false;
  }
}
