using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private float timeBetweenAttack;
    public float startTimeBetweenAttack;

    public LayerMask whatIsEnemy;
    public Transform attackPos;
    public float attackRange;
    public int punchDamage;
    public int kickDamage;

    //private PlayerClass player;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        //player = GetComponent<PlayerClass>();
    }

    void Update () {
		
        //check if cooldown is finished and attack is selected
        //WILL NEED TO BE TWEAKED WITH COMBO
        //punch
        if (timeBetweenAttack <= 0   &&
            Input.GetButton("Punch"))
            // && player.currentEnergy >= 10)
        {
            //Attack
            anim.SetBool("isPunching", true);
            Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy); 
            for(int i =0; i < enemiesToHit.Length; i++)
            {
                //enemiesToHit[i].GetComponent<Enemy>().takeDamage(punchDamage);
            }

            //player.currentEnergy -= 10;
            timeBetweenAttack = startTimeBetweenAttack;
        }//end if
        else
        {
            anim.SetBool("isPunching", false);
            timeBetweenAttack -= Time.deltaTime;
        }

        //kick
        if (timeBetweenAttack <= 0   &&
             Input.GetButton("Kick"))
             //&& player.currentEnergy >= 20)
        {
            //Attack
            anim.SetBool("isKicking", true);
            Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
            for (int i = 0; i < enemiesToHit.Length; i++)
            {
                //enemiesToHit[i].GetComponent<Enemy>().takeDamage(kickDamage);
            }

            //player.currentEnergy -= 20;
            timeBetweenAttack = startTimeBetweenAttack * 2;
        }//end if
        else
        {
            anim.SetBool("isKicking", false);
            timeBetweenAttack -= Time.deltaTime;
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
