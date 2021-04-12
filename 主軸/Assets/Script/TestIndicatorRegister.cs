using UnityEngine;
using System.Collections;

public class TestIndicatorRegister : MonoBehaviour {
    
	void Start () {

        Invoke("Register", 0.3f);
    }
	
    void Register()
    {
        DI_System.CreateIndicator(this.transform);
    }
}
