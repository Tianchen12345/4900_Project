using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nidoran : MonoBehaviour
{
    [SerializeField] float nidoranRunSpeed = 5f;
    [SerializeField] AudioClip nidoranDyingSFX;

    Rigidbody2D NidoranRigidbody;
    Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        NidoranRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    public void Dying()
    {
        enemyAnimator.SetTrigger("Die");        
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        NidoranRigidbody.bodyType = RigidbodyType2D.Static;        
        StartCoroutine(DestroyEnemy());
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    private void EnemyMovement()
    {
        if(GetComponent<BoxCollider2D>().enabled)
        {
            if (IsFacingLeft())
            {
                NidoranRigidbody.velocity = new Vector2(-nidoranRunSpeed, 0f);
            }
            else
            {
                NidoranRigidbody.velocity = new Vector2(nidoranRunSpeed, 0f);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FlipSprites();
    }

    private void FlipSprites()
    {
        transform.localScale = new Vector2(Mathf.Sign(NidoranRigidbody.velocity.x), 1f);
    }

    private bool IsFacingLeft()
    {
        return transform.localScale.x > 0;
    }

    void PlaynidoranDyingSFX()
    {
        AudioSource.PlayClipAtPoint(nidoranDyingSFX, Camera.main.transform.position);
    }
}
