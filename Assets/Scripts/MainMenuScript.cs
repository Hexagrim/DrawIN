using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Animator T_Anim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel(string lvl)
    {
        StartCoroutine(LoadScene(lvl));
    }

    IEnumerator LoadScene(string Lvl)
    {
        T_Anim.SetTrigger("fade");
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadSceneAsync(Lvl);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
