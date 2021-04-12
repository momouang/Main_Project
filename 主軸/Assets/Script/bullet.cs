using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

    Rigidbody rb;
    public GameObject target;
    GameObject spawnPosition;
    private bool hitPlayer = false;
    private Vector3 targetDistance;
    public float bulletShootForce = 5000;

    private void Awake()
    {
        spawnPosition = GameObject.Find("AttackSpawn");
        rb = gameObject.GetComponent<Rigidbody>();
        transform.LookAt(target.transform);
    }

    private void Start()
    {
        targetDistance = target.transform.position - spawnPosition.transform.localPosition;
    }

    private void Update()
    {
        if (!hitPlayer)
        {
            rb.AddForce(targetDistance * bulletShootForce);
            transform.LookAt(target.transform);
        }
        Destroy(gameObject,3);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player" )
        {
            Destroy(gameObject);
            hitPlayer = true;
        }
    }
}
