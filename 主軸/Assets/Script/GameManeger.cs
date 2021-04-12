using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManeger : MonoBehaviour {

    bool gameHasEnded = false;
    public float restartDelay = 1f;

    public static bool gameIsPaused = false;

    public GameObject levelLoader;
    public GameObject gameOverUI;
    public EnemyScript enemyHealthbar;
    public PlayerMovement playermovement;
    public DI_System disystem;
    public Animator animator;

    public GameObject healthbarImage;


    private void Start()
    {
        healthbarImage.SetActive(false);
    }

    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (enemyHealthbar.foundTarget == true) { healthbarImage.SetActive(true); }

        if (playermovement.currentHealth == 0)
        {
            gameOver();
        }
        else
        {
            gameOverUI.SetActive(false);
        }
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
        disystem.isAttacking = false;
        enemyHealthbar.foundTarget = false;
        //Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameIsPaused = true;
        Invoke("startgameOver",1.5f);
    }

    void startgameOver()
    {
        AudioManager.instance.Play("gameOver");
        AudioManager.instance.fadeOut("gameBattle");
        Time.timeScale = 0;
    }
}
