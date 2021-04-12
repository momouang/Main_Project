using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public GameObject Player;
    public DI_System diSystem;
    public HealthBar healthBar;
    public EyeCollision eyeCollision;
    public Shoot shootScript;

    public float targetRange = 100f;
    public float enemyDistanceRun = 60f;
    public float stopDistance = 80f;

    public float gravity = -9.81f;
    Vector3 velocity;

    public int maxHealth = 100;
    public int currentHealth;
    //public Text number;
    //public GameObject damageNumber;

    public float coolDown = 5;
    public float coolDownTimer;
    public bool foundTarget;

    private Animator animator;

    public GameObject hitEffect;

    public GameObject bullet;
    public Transform bulletSpawn;
    public Rigidbody bulletRB;
    public float shootForce;

    public float maxTime = 5;
    public float minTime = 3;
    private float time;
    private float spawnTime;

    public bool isDead;

    public GameObject dust;
    public Transform dustPoint;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        diSystem.isAttacking = false;
        animator = gameObject.GetComponent<Animator>();
        setRandomTime();
        time = minTime;
    }

    void Update()
    {
        if (!isDead)
        {
            enemyMove();
            eyeAttacking();
        }

        if(currentHealth <= 0)
        {
            deadMode();
        }

        if(diSystem.isAttacking)
        {
            attackMode();
        }
        else
        {
            peaceMode();
        }
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            animator.Play("Hurt");

            ContactPoint contact = collision.contacts[0];
            GameObject hit = Instantiate(hitEffect, contact.point, Quaternion.identity) as GameObject;
            //GameObject damagenumber = Instantiate(damageNumber, contact.point, Quaternion.identity) as GameObject;
            //AudioManager.instance.Play("arrowAttack");
            Destroy(hit, 0.7f);

            if (diSystem.isAttacking)
            {
                TakeDamage(50);
            }
            else
            {
                TakeDamage(30);
            }

            Destroy(collision.gameObject);
        }

    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.Sethealth(currentHealth);
        if(currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    void peaceMode()
    {
        eyeCollision.isinArea = false;
        if (foundTarget)
        {
            if (time >= spawnTime)
            {
                time = 0;
                GameObject shootingBullet = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
                bulletRB.AddForce(bulletSpawn.transform.forward * shootForce);
                shootingBullet.transform.LookAt(Player.transform);

                Destroy(shootingBullet,10f);
                //AudioManager.instance.Play("snakeHiss");
                setRandomTime();            
            }
        }
    }

    void attackMode()
    {
        if(foundTarget)
        {
            //animator.SetBool("eyeClosed",false);
        }
    }

    void switchMode()
    {
        Vector3 enemyPosition = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 playerPosition = new Vector3(Player.transform.position.x, 0, Player.transform.position.z);

        float distance = Vector3.Distance(enemyPosition, playerPosition);

        if (distance < targetRange)
        {
            diSystem.isAttacking = !diSystem.isAttacking;
        }
        else
        {
            diSystem.isAttacking = false;
        }
    }

    void enemyMove()
    {

        Vector3 enemyPosition = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 playerPosition = new Vector3(Player.transform.position.x, 0, Player.transform.position.z);

        float distance = Vector3.Distance(enemyPosition, playerPosition);

        transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z));

        foundTarget = true;
        //transform.Translate(velocity * Time.deltaTime);
        if (distance >= targetRange)
        {
            foundTarget = false;
            return;
        }

        if(distance < targetRange && distance > stopDistance)
        {
            transform.Translate(transform.forward * Time.deltaTime * 20);
            //AudioManager.instance.Play("snakeMove");
            animator.Play("walkForward");
            foundTarget = true;
        }

        if(distance < stopDistance && distance > enemyDistanceRun)
        {
            foundTarget = true;
            return;
        }

        if (distance <= enemyDistanceRun)
        {         
            //transform.Translate(-transform.forward * Time.deltaTime * 20);
            foundTarget = true;
        }
    }

    void eyeAttacking()
    {
        if (!foundTarget)
        {
            return;
        }

        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }

        if (coolDownTimer < 0)
        {
            coolDownTimer = 0;
        }

        if (coolDownTimer == 0)
        {
            coolDownTimer = coolDown;
            switchMode();
        }
    }

    void setRandomTime()
    {
        spawnTime = Random.Range(minTime,maxTime);
    }

    void deadMode()
    {
        isDead = true;
        animator.Play("Dead");
        //AudioManager.instance.Play("snakeDead");
        shootScript.enabled = false;
        foundTarget = false;
        diSystem.isAttacking = false;
    }

    public void loadNewScene()
    {
        SceneManager.LoadScene(2);
        //AudioManager.instance.fadeOut("gameBattle");
        //AudioManager.instance.stopPlaying("Shoot");
        //AudioManager.instance.Play("youWin");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void loaddeadDust()
    {
        //AudioManager.instance.Play("deadDust");
        Instantiate(dust,dustPoint.position,Quaternion.identity);
    }
}