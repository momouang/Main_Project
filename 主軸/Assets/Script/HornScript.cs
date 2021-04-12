using UnityEngine;
using System.Collections;

public class HornScript : MonoBehaviour {

    public GameObject Player;
    public Animator animator;
    public DI_System diSystem;
    public EnemyScript enemyScript;

    public float targetRange = 100f;
    public float enemyDistanceRun = 60f;
    public float stopDistance = 80f;
    public bool foundTarget;

    public GameObject bodyTransform;

    private void Update()
    {
        if(enemyScript.currentHealth == 0)
        {
            deadMode();
        }

        if (!enemyScript.isDead)
        {
            enemyMove();
        }

        if (diSystem.isAttacking)
        {
            attackMode();
        }
        else
        {
            peaceMode();
        }
    }

    void peaceMode()
    {
        //DamgeIndicator.SetActive(false);
        if (foundTarget)
        {
            animator.SetBool("eyeClosed", true);
            //animator.Play("eyeisClosed");
        }
    }

    void attackMode()
    {
        //DamgeIndicator.SetActive(true);
        if (foundTarget)
        {
            animator.SetBool("eyeClosed", false);
        }
    }

    void enemyMove()
    {
        foundTarget = true;

        gameObject.transform.localPosition = bodyTransform.transform.localPosition;
        gameObject.transform.localRotation = bodyTransform.transform.localRotation;
    }

    void deadMode()
    {
        enemyScript.foundTarget = false;
        diSystem.isAttacking = false;
        enemyScript.isDead = true;
        animator.Play("dead");
    }

    void loadNewScene()
    {
        enemyScript.loadNewScene();
    }

    public void loaddeadDust()
    {
        enemyScript.loaddeadDust();
    }
}
