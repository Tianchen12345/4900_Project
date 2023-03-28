using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBetweenAttack;

    [SerializeField] Transform hurtBox; // The center position of the player's hurt box for attacking
    [SerializeField] float attackRadius = 2f;   // player attack range
    [SerializeField] int damage;

    Animator myAnimator;
    AudioSource myAudioSource;

    [SerializeField] AudioClip attackingSFX; // SFX is Sound effects

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();

        timeBtwAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            if (CrossPlatformInputManager.GetButtonDown("Fire1"))
            {
                myAnimator.SetTrigger("Attacking");
                myAudioSource.PlayOneShot(attackingSFX);                

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(hurtBox.position, attackRadius, LayerMask.GetMask("Enemy"));

                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemies>().TakeDamage(damage);
                }
                timeBtwAttack = startTimeBetweenAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hurtBox.position, attackRadius);
    }


}