using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Animator transition;
    public float TransitionTime = 1f;

    public void PlayGame()
    {
        StartCoroutine( Loadlevel(1));
    }

    public void GameOver()
    {
        GameManeger.gameIsPaused = true;
        Time.timeScale = 1;
        StartCoroutine(Loadlevel(0));
        AudioManager.instance.fadeOut("gameOver");
        AudioManager.instance.Play("gameBattle");
    }

    public void gameWins()
    {
        StartCoroutine(Loadlevel(0));
        AudioManager.instance.Play("gameBattle");
        AudioManager.instance.fadeOut("youWin");
    }

    public void quit()
    {
        Application.Quit();
    }

    IEnumerator Loadlevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadScene(levelIndex);
    }
    
}
