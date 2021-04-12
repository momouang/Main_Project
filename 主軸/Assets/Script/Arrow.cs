using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    public Shoot shoot;
    Rigidbody myBody;
    private float lifeTimer = 2f;
    private float timer = 0f;
    private bool hitSomething = false;
    //public bool shootOut;
    private GameObject forward;
    public bool isShoot = false;



	void Awake () {

        myBody = GetComponent<Rigidbody>();
        forward = GameObject.Find("Forward");
        transform.LookAt(forward.transform);
        shoot = GetComponent<Shoot>();

    }
	
	void Update () {

        if (isShoot)
        {
            timer += Time.deltaTime;

            //if (shoot.go.transform.setParent(shoot.parent) != null)
            //{
               // Destroy(gameObject);
            //}
        }


        if(timer >= lifeTimer)
        {
            Destroy(gameObject);
        }

        if (!hitSomething && !isShoot)
        {
            transform.LookAt(forward.transform);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.collider.tag != "Arrow")
        {
            hitSomething = true;
            Stick();
        }
    }

    private void Stick()
    {
        myBody.constraints = RigidbodyConstraints.FreezeAll;
    }

}
