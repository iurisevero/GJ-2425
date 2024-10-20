using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseUIController : MonoBehaviour
{
    private GameObject pauseGameObj;
    public bool paused = false;

    public Animator transition;
    public float transitionTime = 1f; // tempo da animação
    public int menuSceneIndex = 0;
    
    private void Start()
    {
        paused = false;
        pauseGameObj = this.transform.GetChild(0).gameObject;
        this.AddObserver(OnEsc, InputHandler.OnEscClickEvent);
    }

    public void OnEsc(object sender, object args)
    {
        Pause();
    }

    public void Pause()
    {
        if(paused) {
            StopPause();
        } else {
            StartPause();
        }
        
    }

    public void StartPause()
    {
        Time.timeScale = 0;
        paused = true;
        pauseGameObj.SetActive(paused);
    }

    public void StopPause()
    {
        Time.timeScale = 1;
        paused = false;
        pauseGameObj.SetActive(paused);
    }

    public void OnClickContinueButton()
    {
        Debug.Log("Click continue");
        StopPause();
    }

    public void OnClickExit()
    {
        Debug.Log("Click exit");
        StartCoroutine(OnClickExitRoutine());
    }

    private IEnumerator OnClickExitRoutine()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime); 
        SceneManager.LoadScene(menuSceneIndex);
    }
}
