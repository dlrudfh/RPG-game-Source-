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
                gameObject.GetComponent<TextMeshProUGUI>().text = "Now you can use Charge attack!!";
            }
            else if (pastLevel == 5)
            {
                gameObject.GetComponent<TextMeshProUGUI>().text = "Now you can use Dash!!";
            }
            else gameObject.GetComponent<TextMeshProUGUI>().text = "LEVEL UP!!";
            Invoke("clean", 2f);
        }

    }

    public void message(string texts)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = texts;
    }

    private void clean()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = " ";
    }
}
