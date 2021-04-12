using UnityEngine;
using System.Collections.Generic;

public class AmmoScript : MonoBehaviour {

    public Shoot playerAmmo;
    public int jc_ind = 0;
    private List<Joycon> joycons;


    private void Start()
    {
        joycons = JoyconManager.Instance.j;
    }


    private void OnTriggerStay(Collider other)
    {
        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];
            if (j.GetButtonDown(Joycon.Button.SHOULDER_1) && other.tag == "Player")
            {
                //Debug.Log("pick up");
                AudioManager.instance.Play("pickUp");
                Destroy(gameObject);
                playerAmmo.currentAmmo += 10;
            }
        }
    }
}
