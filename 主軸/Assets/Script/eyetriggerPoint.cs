using UnityEngine;
using System.Collections;

public class eyetriggerPoint : MonoBehaviour {

    public MyEye myeye;
    bool istrigger;


	// Use this for initialization
	void Start () {

        myeye.GazePoint.SetActive(false);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            myeye.GazePoint.SetActive(true);
            istrigger = true;
        }
    }

    private void Update()
    {
        if(istrigger)
        {
            Destroy(gameObject);
        }
    }
}
