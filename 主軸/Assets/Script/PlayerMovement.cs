using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

    public float[] stick;
    public int jc_ind = 0;
    private List<Joycon> joycons;

    public Animator anim;
    public GameManeger gameManeger;
    public CharacterController controller;
    public Rigidbody rb;
    Timer timerObj;

    public float speed = 12f;
    public float gravity = -9.81f;
    Vector3 velocity;

    public int maxHealth = 500;
    public int currentHealth;
    public HealthBar healthBar;
    public EyeCollision eyeCollision;

    Color alphaColor;
    public Image damageOverlay;

    float parameter;
    float parameter2;

    float forwardx;
    float turny;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        alphaColor = damageOverlay.color;

        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind + 1)
        {
            Debug.Log("joy-con not connected");
        }

    }

    void Update () {

        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];
            stick = j.GetStick();

            parameter = Mathf.InverseLerp(0, maxHealth, currentHealth);
            parameter2 = Mathf.InverseLerp(0, 255, parameter);

            Move();

            /*if (Input.GetButtonDown("Horizontal2") || Input.GetButtonDown("Vertical2"))
                AudioManager.instance.Play("footStep");
            else if (!Input.GetButton("Horizontal2") && !Input.GetButton("Vertical2"))
                AudioManager.instance.stopPlaying("footStep"); // or Pause()*/
        }
    }

    public void Move()
    {
        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];
            float[] axes = j.GetStick();

            Vector3 move = transform.right * axes[0] + transform.forward * axes[1];

            forwardx = Mathf.InverseLerp(-1,1,axes[0]);
            turny = Mathf.InverseLerp(-1, 1, axes[1]);

            controller.Move(move * speed * Time.deltaTime);
            anim.SetFloat("Forward", axes[1]);
            anim.SetFloat("Turn", axes[0]);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if(rb.position.y < -5f)
        {
            FindObjectOfType<GameManeger>().EndGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "bullet")
        {
            CameraShaker.Instance.ShakeOnce(1f, 1f, 0.5f, 0.5f);
            TakeDamage(5);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.Sethealth(currentHealth);
        alphaColor.a = (1-parameter2*255);
        damageOverlay.color = alphaColor;
    }
}
