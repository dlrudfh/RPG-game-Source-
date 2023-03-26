using UnityEngine.UI;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private void Update()
    {
        GetComponent<Slider>().value = (float)PlayerPrefs.GetInt("CHP")
                                     / (float)PlayerPrefs.GetInt("HP");
    }
}
