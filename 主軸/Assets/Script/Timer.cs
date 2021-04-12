using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class Timer : MonoBehaviour {

    Image fillImg;
    public float timeAmt = 10;
    float time;
    public Text timeText;
    public RectTransform indicatorTransform;

    public EyeCollision eyecollision;
    public PlayerMovement player;

    private void Start()
    {
        fillImg = GetComponent<Image>();
        time = timeAmt;
        fillImg.enabled = false;
        timeText.enabled = false;
    }

    private void Update()
    {
       gameObject.transform.position = indicatorTransform.position;

        if(eyecollision.isinArea)
        {
            fillImg.enabled = true;
            timeText.enabled = true;
            timerStart();
        }
        else
        {
            fillImg.enabled = false;
            timeText.enabled = false;
            time = timeAmt;
        }
    }

    void timerStart()
    {
        if(!eyecollision.isinArea)
        {
            return;
        }

        if (time > 0)
        {
            time -= Time.deltaTime;
            fillImg.fillAmount = time / timeAmt;
            timeText.text = time.ToString("F0");

            if (time <= 0)
            {
                player.TakeDamage(100);
                CameraShaker.Instance.ShakeOnce(4f, 4f, 0.5f, 0.5f);

                player.speed -= 0.03f;
                if (player.speed == 0)
                {
                    player.speed = 0;
                }
                if (player.currentHealth <= 0)
                {
                    player.currentHealth = 0;
                }
            }
        }
    }
}
