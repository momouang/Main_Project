using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMask : MonoBehaviour
{
    public LayerMask layer;
    public GameObject[] allObj = new GameObject[100];

    private void Start()
    {
        Invoke("find", 1);
        
    }
    void find()
    {
        allObj = GameObject.FindGameObjectsWithTag("Untagged");

        for (int i = 0; i < allObj.Length; i++)
        {
            if (allObj[i].layer == 10)
            {
                print(allObj[i]);
            }
        }
    }

    void Update()
    {
        
    }
}
