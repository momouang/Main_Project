using UnityEngine;
using Tobii.Gaming;
using UnityEngine.UI;
using System.Collections;

public class MyEye : MonoBehaviour
{
    public float speed;
    public GameObject GazePoint;
    public EnemyScript enemyscript;

    void Update()
    {
        GazePoint gazePoint = TobiiAPI.GetGazePoint();
        if (gazePoint.IsRecent())
        {           
            //Debug.Log("eye working");
            //Vector2 gazePosition = gazePoint.Screen;
            //Vector2 roundedSampleInput = new Vector2(Mathf.RoundToInt(gazePosition.x), Mathf.RoundToInt(gazePosition.y));
            GazePoint.transform.localPosition = (gazePoint.Screen - new Vector2(Screen.width, Screen.height) / 2f) / GetComponentInParent<Canvas>().scaleFactor;
        }
    }
}
