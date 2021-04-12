using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class DI_System : MonoBehaviour
{
    public EyeCollision eyecollision;

    [Header("References")]

    public Canvas canvas;
    [SerializeField]
    private GameObject indicatorPrefab = null;
    [SerializeField]
    private Transform player = null;

    public Dictionary<Transform, DamageIndicator> Indicators = new Dictionary<Transform, DamageIndicator>();
    public bool isAttacking;


    #region Delegates
    public static Action<Transform> CreateIndicator = delegate { };
    public static Func<Transform, bool> CheckIfObjectInSight = null;
    #endregion

    public void OnEnable()
    {
        CreateIndicator += Create;
        CheckIfObjectInSight += InSight;
    }

    public void OnDisable()
    {
        CreateIndicator -= Create;
        CheckIfObjectInSight -= InSight;
    }

    void Create(Transform target)
    {
        if (Indicators.ContainsKey(target))
        {
            Indicators[target].Restart();
            return;
        }
        GameObject newIndicator = Instantiate(indicatorPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        DamageIndicator IndicatorScript = newIndicator.GetComponent<DamageIndicator>();
        IndicatorScript.Register(target, player, new Action(() => { Indicators.Remove(target); }));
        Indicators.Add(target, IndicatorScript);
        newIndicator.transform.SetParent(canvas.transform, false);

        eyecollision.area = newIndicator.gameObject.GetComponentInChildren<Image>();
    }

    bool InSight(Transform t)
    {
        Vector3 screenPoint = GetComponent<Camera>().WorldToViewportPoint(t.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

}