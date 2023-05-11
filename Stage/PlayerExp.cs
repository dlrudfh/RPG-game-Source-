using UnityEngine.UI;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("XP")
                                     / PlayerPrefs.GetInt("LV");
    }
}