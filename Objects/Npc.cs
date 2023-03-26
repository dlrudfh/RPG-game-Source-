using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Npc : MonoBehaviour
{
    KeyCode action;
    [SerializeField] GameObject questText;
    [SerializeField] GameObject chatWindow;
    public int textNum = 0;
    bool canTalk;
    public int questAccept = 0;
    public bool branchSelect;

    private void Awake()
    {
        chatWindow = GameObject.Find("System").transform.Find("ChatWindow").gameObject;
        questText = chatWindow.transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 1);
            canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            canTalk = false;
        }
    }

    private void Update()
    {
        action = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ACTION"), true);
        if ((!branchSelect && Input.GetKeyUp(action) && canTalk) || branchSelect)
        {
            canTalk = false;
            switch (textNum)
            {
                case 0:
                    chatWindow.SetActive(true);
                    questText.GetComponent<TextMeshProUGUI>().text = "Hi, I'm Mask Dude. I have a small problem. Can you help me?";
                    break;
                case 1:
                    chatWindow.GetComponent<ChatWindow>().BranchOn();
                    break;
                case 2:
                    if (questAccept == 0)
                    {
                        textNum--;
                        break;
                    }
                    branchSelect = false;
                    if (questAccept == 1)
                    {
                        questText.GetComponent<TextMeshProUGUI>().text = "Thank you. Too many ladybugs are bothering me. Kill 30 ladybugs please.";
                        PlayerPrefs.SetInt("CurrentQuest", 1);
                    }
                    else questText.GetComponent<TextMeshProUGUI>().text = "Bye.";
                    break;
                case 3:
                    chatWindow.SetActive(false);
                    break;
                case 4:
                    questText.GetComponent<TextMeshProUGUI>().text = "......"; ;
                    chatWindow.SetActive(true);
                    textNum -= 2;
                    break;
                
            }
            textNum++;
            canTalk = true;
        }
    }
}
