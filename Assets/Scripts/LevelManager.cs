using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator T_Anim;
    public string nextLevel;
    public TMP_Text nextlvlText;
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "LoadedLevel")
        {
            nextLevel = "MainMenu";
            nextlvlText.text = "Main Menu";
            
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Retry();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(NextScene("MainMenu"));
        }
    }

    public void NextLevel()
    {
        StartCoroutine(NextScene(nextLevel));
    }
    IEnumerator NextScene(string lvl)
    {
        T_Anim.SetTrigger("fade");
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadSceneAsync(lvl);
    }
    public void Retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
