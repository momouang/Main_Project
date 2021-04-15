using UnityEngine;
using Tobii.Gaming;
using UnityEngine.UI;
using System.Collections;

public class MyEye : MonoBehaviour
{
    public float speed;
    public GameObject GazePoint;

    void Update()
    {
        GazePoint gazePoint = TobiiAPI.GetGazePoint();

        if (gazePoint.IsValid)
        {
            Vector2 gazePosition = gazePoint.Screen;
        }
        if (gazePoint.IsRecent())
        {           
            //Vector2 gazePosition = gazePoint.Screen;
            //Vector2 roundedSampleInput = new Vector2(Mathf.RoundToInt(gazePosition.x), Mathf.RoundToInt(gazePosition.y));
            GazePoint.transform.localPosition = (gazePoint.Screen - new Vector2(Screen.width, Screen.height) / 2f) / GetComponent<Canvas>().scaleFactor;
        }
    }
}
