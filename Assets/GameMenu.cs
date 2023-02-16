using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
   
    public void LoadGamePlayDelayed(float delay)
    {
        StartCoroutine(LoadDelayed(delay));
    }
    public IEnumerator LoadDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadGameplay();
    }
    public void LoadGameplay()
    {
        Debug.Log("loading game");
        SceneManager.LoadScene(1);
    }
}
