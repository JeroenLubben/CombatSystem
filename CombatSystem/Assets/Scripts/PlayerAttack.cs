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

    void Update() {

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


        if ((kickTimeBetweenAttack > 0 && noOfClicks ==0))
        {
            kickTimeBetweenAttack -= Time.deltaTime;
        }

        else if (Input.GetButtonDown("Kick"))
        {
            ComboStarter();
            kickTimeBetweenAttack = startTimeBetweenAttack * 2;
        }


    }

    void ComboStarter()
    {
        if (canClick)
        {
            noOfClicks++;
        }
        Debug.Log(noOfClicks);

        if (noOfClicks == 1)
        {
            animator.SetInteger("kickAttack", 1);
        }
    }

    public void ComboCheck()
    {

        canClick = false;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Kick1") && noOfClicks == 1)
        {//If the first animation is still playing and only 1 click has happened, return to idle
            animator.SetInteger("kickAttack", 0);
            canClick = true;
            noOfClicks = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Kick1") && noOfClicks >= 2)
        {//If the first animation is still playing and at least 2 clicks have happened, continue the combo          
            animator.SetInteger("kickAttack", 2);
            canClick = true;
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Kick2"))
        {
            animator.SetInteger("kickAttack", 0);
            canClick = true;
            noOfClicks = 0;
        }
    }


 private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
