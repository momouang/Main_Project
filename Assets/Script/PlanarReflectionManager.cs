using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Camera m_ReflectionCamera;
    Camera m_MainCamera;

    GameObject m_ReflectionPlane;
    void Start()
    {
        GameObject reflectionCameraGO = new GameObject("ReflectionCamera");
        m_ReflectionCamera = reflectionCameraGO.AddComponent<Camera>();
        m_ReflectionCamera.enabled = false;

        m_MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPostRender()
    {
        RenderReflection();
    }
    void RenderReflection()
    {
        m_ReflectionCamera.CopyFrom(m_MainCamera);
        Vector3 cameraDirectionWorldSpace = m_MainCamera.transform.forward;
        Vector3 cameraUpWorldSpace = m_MainCamera.transform.up;
        Vector3 cameraPositionWorldSpace = m_MainCamera.transform.position;
    }
}
