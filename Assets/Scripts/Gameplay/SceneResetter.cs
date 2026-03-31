using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneResetter : MonoBehaviour
{
    public Score score;
    public AudioSource audioSource;
    public AudioClip resetSound;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayResetSound();
            score.ResetRunScore();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void PlayResetSound()
    {
        audioSource.PlayOneShot(resetSound);
    }
}