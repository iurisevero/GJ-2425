using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f; // tempo da animação
    public int gameSceneIndex = 1;

    public void StartGame()
    {
        StartCoroutine(PlaySceneTransitionAnimation());
    }

    private IEnumerator PlaySceneTransitionAnimation()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime); 
        SceneManager.LoadScene(gameSceneIndex);
    }

    public void Quit() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
