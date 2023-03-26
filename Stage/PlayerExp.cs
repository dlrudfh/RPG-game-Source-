using UnityEngine.UI;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Slider>().value = (float)PlayerPrefs.GetInt("XP")
                                     / (float)PlayerPrefs.GetInt("LV");
    }
}