using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCaller : MonoBehaviour
{
    // Start is called before the first frame update
    public void SceneCall(string scene)
    {
        PlayerPrefs.SetInt("CHP", 3);
        SceneManager.LoadScene(scene);
    }

    public void GameStart(string scene)
    {
        PlayerPrefs.SetInt("CHP", 3);
        SceneManager.LoadScene(scene);
        PlayerPrefs.SetFloat("x", -6);
        PlayerPrefs.SetFloat("x", -4.5f);
    }
}
