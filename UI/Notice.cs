using TMPro;
using UnityEngine;

public class Notice : MonoBehaviour
{
    private int pastLevel;
    private void Start()
    {
        pastLevel = PlayerPrefs.GetInt("LV");
    }
    private void Update()
    {
        if(PlayerPrefs.GetInt("LV") > pastLevel)
        {
            pastLevel = PlayerPrefs.GetInt("LV");
            if(pastLevel == 3)
            {
                message("Now you can use Charge attack!!");
            }
            else if (pastLevel == 5)
            {
                message("Now you can use Dash!!");
            }
            else message("LEVEL UP!!");
        }

    }

    public void message(string texts)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = texts;
        Invoke("clean", 2f);
    }

    private void clean()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = " ";
    }
}
