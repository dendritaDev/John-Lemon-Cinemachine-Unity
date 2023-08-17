using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{

    public float fadeDuration = 1f;

    public float displayImageDuration = 1f;

    private bool isPlayerAtExit, isPlayerCaught;

    public GameObject player;

    public CanvasGroup exitBackGroundImageCanvasGroup;
    
    public CanvasGroup caughtBackGroundImageCanvasGroup;

    private bool hasAudioPlayed;

    public AudioSource exitAudio, caughtAudio;

    private float timer;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }

    private void Update()
    {
        if(isPlayerAtExit)
        {
            EndLevel(exitBackGroundImageCanvasGroup, false, exitAudio);
        }
        else if(isPlayerCaught)
        {
            EndLevel(caughtBackGroundImageCanvasGroup, true, caughtAudio);
        }
    }

    /// <summary>
    /// Lanza la imagen de fin de la partida
    /// </summary>
    /// <param name="imageCanvasGroup">Imagen de fin de partida correspondiente</param>
    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if(!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }
        

        timer += Time.deltaTime;
        imageCanvasGroup.alpha = Mathf.Clamp(timer / fadeDuration, 0, 1);


        if (timer > fadeDuration + displayImageDuration)
        {
            if(doRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Application.Quit();
            }
            
        }
    }

    public void CatchPlayer()
    {
        isPlayerCaught = true;
    }
}
