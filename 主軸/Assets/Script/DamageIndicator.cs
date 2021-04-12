using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour {

    DI_System disystem;
    private const float MaxTimer = 8f;
    //private float timer = MaxTimer;

    private CanvasGroup canvasGroup = null;
    protected CanvasGroup CanvasGroup
    {
        get
        {
            if(canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
                if(canvasGroup == null)
                {
                    canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
            }
            return canvasGroup;
        }
    }

    private RectTransform rect = null;
    protected RectTransform Rect
    {
        get
        {
            if (rect == null)
            {
                rect = GetComponent<RectTransform>();
                if (rect == null)
                {
                    rect = gameObject.AddComponent<RectTransform>();
                }
            }
            return rect;
        }
    }

    public Transform Target { get; protected set; }
    private Transform Player = null;

    internal void Register()
    {
        throw new NotImplementedException();
    }

    //private IEnumerator IE_CountDown = null;
    //private Action unRegister = null;

    private Quaternion tRot = Quaternion.identity;
    private Vector3 tPos = Vector3.zero;

    private void Start()
    {
        disystem = GameObject.Find("GameCanvas").GetComponent<DI_System>();
        gameObject.transform.GetChild(0).gameObject.SetActive(!disystem.isAttacking);
    }

    private void Update()
    {
        if(gameObject.transform.GetChild(0))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(disystem.isAttacking);
        }
    }

    public void Register(Transform target, Transform Player, Action unRegister)
    {
        this.Target = target;
        this.Player = Player;
        //this.unRegister = unRegister;

        StartCoroutine(RotateToTheTarget());
        //StartTimer();       
    }

    public void Restart()
    {
        //timer = MaxTimer;
        //StartTimer();
    }

    /*private void StartTimer()
    {
        if(IE_CountDown != null) { StopCoroutine(IE_CountDown); }
        IE_CountDown = Countdown();
        StartCoroutine(IE_CountDown);
    }*/

    IEnumerator RotateToTheTarget()
    {
        while(enabled)
        {
            if(Target)
            {
                tPos = Target.position;
                tRot = Target.rotation;
            }

            Vector3 direction = Player.position - tPos;

            tRot = Quaternion.LookRotation(direction);
            tRot.z = -tRot.y;
            tRot.y = 0;
            tRot.x = 0;

            Vector3 northDirection = new Vector3(0, 0, Player.eulerAngles.y);
            Rect.localRotation = tRot * Quaternion.Euler(northDirection);

            yield return null;
        }

    }
        
    /*private IEnumerator Countdown()
    {
        while (CanvasGroup.alpha < 1.0f)
        {
            canvasGroup.alpha += 4 * Time.deltaTime;
            yield return null;
        }
        while(timer > 0)
        {
            timer--;
            yield return new WaitForSeconds(1);
        }
        while(CanvasGroup.alpha > 0.0f)
        {
            CanvasGroup.alpha -= 2 * Time.deltaTime;
            yield return null;
        }
        unRegister();
        Destroy(gameObject);
    }*/

}
