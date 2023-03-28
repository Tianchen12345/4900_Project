using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player1;
  

    // Update is called once per frame
    void Update()
    {

        transform.position= new Vector3(player1.transform.position.x,transform.position.y,transform.position.z);



  }

}
