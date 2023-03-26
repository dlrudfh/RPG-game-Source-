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
}
