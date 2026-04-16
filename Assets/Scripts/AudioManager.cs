using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            audioSource.volume = 0.2f;
        }
        else if (SceneManager.GetActiveScene().name == "LevelEditor")
        {
            audioSource.volume = 0f;
        }
        else
        {
            audioSource.volume = 0.1f;
        }
    }

}
