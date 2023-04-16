using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public int health;
    public float speed;
    private float dazedTime;
    public float startDazedTime;

    private Animator myAnimator;
    private Rigidbody2D myRigidbody;

    [SerializeField] GameObject hpBar;
    [SerializeField] GameObject hp;

    int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if(dazedTime <= 0)
        {
            speed = 5;
        }
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }

        if(health <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            myRigidbody.bodyType = RigidbodyType2D.Static;
            StartCoroutine(DestroyEnemy());
        }
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        dazedTime = startDazedTime;
        health -= damage;
        float scale = (float)health / (float)maxHealth;
        hpBar.transform.localScale = new Vector3(Mathf.Max(scale, 0f), 1f, 1f);
        hp.transform.localScale = new Vector3(Mathf.Max(scale, 0f), 1f, 1f);
    }
}
