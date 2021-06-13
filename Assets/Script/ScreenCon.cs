using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCon : MonoBehaviour
{
    public GameObject Prefab = new GameObject();

    private void Start()
    {
        Invoke("find", 2);

    }
    void find()
    {
        GameObject pre = Instantiate(Prefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
