using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerAttack : MonoBehaviour {

    private float PunchTimeBetweenAttack;
    private float kickTimeBetweenAttack;
    public float startTimeBetweenAttack;

    public LayerMask whatIsEnemy;
    public Transform attackPos;

    public float attackRange;
    public int punchDamage;
    public int kickDamage;

    //private PlayerClass player;

    private Animator animator;


    private int noOfClicks;
    private bool canClick; 

    private void Start()
    {

   
        animator = GetComponent<Animator>();
        //player = GetComponent<PlayerClass>();

        noOfClicks = 0;
        canClick = true;
    }

    void Update()
    {

        //check if cooldown is finished and attack is selected
        //WILL NEED TO BE TWEAKED WITH COMBO
        //punch
        if (PunchTimeBetweenAttack <= 0 &&
            Input.GetButton("Punch"))
        // && player.currentEnergy >= 10)
        {
            //Attack
            animator.SetTrigger("punch");
            Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
            for (int i = 0; i < enemiesToHit.Length; i++)
            {
                //enemiesToHit[i].GetComponent<Enemy>().takeDamage(punchDamage);
            }

            //player.currentEnergy -= 10;
            PunchTimeBetweenAttack = startTimeBetweenAttack;
        }//end if
        else
        {
            PunchTimeBetweenAttack -= Time.deltaTime;
        }


        if(kickTimeBetweenAttack <= 0 &&
           Input.GetButtonDown("Kick"))
        {
            animator.SetTrigger("kick");
            kickTimeBetweenAttack = startTimeBetweenAttack * 2;
        }
        else
        {
            kickTimeBetweenAttack -= Time.deltaTime;
        }


    }


 private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
