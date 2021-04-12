using UnityEngine;
using System.Collections;

public class damagePointCollision : MonoBehaviour {

    public Animator animator;
    public Animator hornAnimator;

    public DI_System disystem;
    public EnemyScript enemyScript;


    private void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("painOver"))
        {
            animator.SetTrigger("Finish");
        }    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Arrow" )
        {
            painMode();
            animator.Play("Pain");
            hornAnimator.Play("pain(eyeclosed)");
        }
    }

    void painMode()
    {
        disystem.isAttacking = false;
        enemyScript.foundTarget = false;
        enemyScript.coolDownTimer = enemyScript.maxTime;
        
    }
}
