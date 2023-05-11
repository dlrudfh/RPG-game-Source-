using UnityEngine.UI;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private void Update()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("CHP")
                                     / PlayerPrefs.GetFloat("HP");
    }
}
