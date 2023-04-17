using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAttack : MonoBehaviour
{
  public GameObject attackPrefab;
	public float respawnTime = 2.0f;
	private Vector2 screenBounds;
  public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
      screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.x));

          StartCoroutine(Rain());
            StartCoroutine(Rain());
              StartCoroutine(Rain());




    }
    private void spawnAttack(){
      if(enemy.currentHealth <=300){
  		GameObject a = Instantiate(attackPrefab) as GameObject;
  		a.transform.position=new Vector2(Random.Range(-screenBounds.x,screenBounds.x), Random.Range(screenBounds.y,screenBounds.y));
    }


  	}
  	IEnumerator Rain(){
  		while(true){
  			yield return new WaitForSeconds(Random.Range(1f,3f));
  			spawnAttack();



  		}
  	}
    // Update is called once per frame
    void Update()
    {

    }
}
