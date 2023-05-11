using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCaller : MonoBehaviour
{
    public void SceneCall(string scene)
    {
        PlayerPrefs.SetFloat("CHP", PlayerPrefs.GetFloat("HP"));
        GameObject.Find("System").GetComponent<UIinfo>().Activate();
        SceneManager.LoadScene(scene);
    }

    public void GameStart(string scene)
    {
        PlayerPrefs.SetFloat("CHP", PlayerPrefs.GetFloat("HP"));
        Transform s = GameObject.Find("System").transform;
        s.GetChild(0).gameObject.SetActive(true);
        s.GetChild(1).gameObject.SetActive(true);
        s.GetChild(2).gameObject.SetActive(true);
        SceneManager.LoadScene(scene);
        PlayerPrefs.SetFloat("x", -6);
        PlayerPrefs.SetFloat("x", -4.5f);
    }
}
