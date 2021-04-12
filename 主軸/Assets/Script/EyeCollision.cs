using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class EyeCollision : MonoBehaviour {

    public bool isinArea = false;
    public Image area;


    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "Target")
        {
            isinArea = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            isinArea = false;
        }
    }
}
