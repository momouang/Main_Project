using UnityEngine;
using Tobii.Gaming;
using System.Collections.Generic;

public class MouseLook : MonoBehaviour {


    public float[] stick;
    public int jc_ind = 0;
    private List<Joycon> joycons;

    public Quaternion orientation;

    public float mouseSensitivity = 100f;
    public Transform playerBody;
    //float xRotation = 0f;


	void Start () {

        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind + 1)
        {
            Debug.Log("joy-con not connected");
        }

        //Cursor.lockState = CursorLockMode.Locked;
	}


    void Update()
    {
        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];

            /*
            float joycon_orx = j.GetVector()[0] * mouseSensitivity ;
            float joycon_ory = j.GetVector()[1] * mouseSensitivity ;
            float joycon_orz = j.GetVector()[2] * mouseSensitivity ;
            Debug.Log(j.GetVector());
            Debug.Log(j.GetVector().w);

            //xRotation += joycon_ory;
            xRotation = Mathf.Clamp(joycon_orx, -0f, 0f);
            */

            Quaternion q = Quaternion.AngleAxis(180, Vector3.up)*Quaternion.AngleAxis(90, Vector3.right) * j.GetVector();

            Vector3 axis = new Vector3(0,0,0);
            float angle = 0;

            q.ToAngleAxis(out angle, out axis);
            playerBody.Rotate(0, j.GetVector()[3] * mouseSensitivity,0);
           
        }
    }
}
