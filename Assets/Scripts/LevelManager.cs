using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator T_Anim;
    public string nextLevel;
    void Start()
    {

    }

    void Update()
    {

    }

    public void NextLevel()
    {
        StartCoroutine(NextScene());
    }
    IEnumerator NextScene()
    {
        T_Anim.SetTrigger("fade");
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadSceneAsync(nextLevel);
    }
    public void Retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
