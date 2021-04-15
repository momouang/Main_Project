using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Shoot : MonoBehaviour {

    public float[] stick;
    public int jc_ind = 0;
    private List<Joycon> joycons;

    public Camera cam;
    public GameObject arrowPrefab;
    public Transform arrowSpawn;
    public float currentForce = 20f;
    public float stopForce = 0.5f;
    
    public Transform parent;
    public bool isShooting;
    private GameObject go;
    private Rigidbody rb;

    public float maxAmmo = 10;
    public float currentAmmo;
    public Text ammoText;

    public Animator anim;


    void Start()
    {
        isShooting = false;
        currentAmmo = maxAmmo;
        anim.SetBool("hold", false);
        joycons = JoyconManager.Instance.j;
    } 

    void Update()
    {
        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];
            ammoText.text = currentAmmo.ToString();
            if (currentAmmo > 0)
            {
                if (j.GetButtonDown(Joycon.Button.SHOULDER_2))
                {
                    isShooting = false;
                    anim.SetBool("released", false);
                    anim.SetBool("hold", true);
                    anim.Play("Hold");
                    Hold();
                }


                if (j.GetButtonUp(Joycon.Button.SHOULDER_2))
                {
                    isShooting = true;
                    anim.SetBool("released",true);
                    anim.SetBool("hold", false);
                    Release();
                }
            }
        }
    }

    public void Hold()
    {
        go = Instantiate(arrowPrefab, arrowSpawn.position,transform.rotation) as GameObject;
        rb = go.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        go.transform.SetParent(parent);
    }

    private void Release()
    {
        //AudioManager.instance.Play("Shoot");
        rb = go.GetComponent<Rigidbody>();
        rb.velocity = cam.transform.forward * currentForce;
        rb.useGravity = true;
        go.transform.parent = null;
        rb.isKinematic = false;
        go.GetComponent<Arrow>().isShoot = true;
        currentAmmo -=1;
    }

}
