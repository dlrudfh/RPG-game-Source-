using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// NPC ��ȭ ����
public class Npc : MonoBehaviour
{
    KeyCode action;
    GameObject questText;
    GameObject chatWindow;
    GameObject player;
    public GameObject timer;
    public int textNum = 0;
    bool canTalk;
    public int questAccept = 0;
    public bool branchSelect;

    void Start()
    {
        // ��ũ��Ʈ ���࿡ �ʿ��� ������Ʈ find �� ����
        chatWindow = GameObject.Find("System").transform.Find("ChatWindow").gameObject;
        questText = chatWindow.transform.GetChild(0).gameObject;
        timer = GameObject.Find("System").transform.Find("Timer").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾� ���� �� ��⸦ �����Ͽ� ��ȣ�ۿ� Ȱ��ȭ ���θ� ǥ��
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 1);
            canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // �÷��̾ �־����� ��⸦ �ǵ��� ��ȣ�ۿ� �Ұ� ���θ� ǥ��
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            canTalk = false;
        }
    }

    private void Update()
    {
        // ��ȭ â�� Ȱ��ȭ�Ǹ� �÷��̾� �ൿ �Ұ���
        player.GetComponent<Player>().enabled = !chatWindow.activeSelf;
        action = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ACTION"), true);
        // �б� ������ ��ٸ��� ���°� �ƴϰ�(�б� ���� â Ȱ��ȭ �� �������� ���� ������ ��ȭ ���� �Ұ���)
        // �׼� ��ư�� ��������, �÷��̾ ������ �ִٸ� or �������� ����ٸ�
        if ((!branchSelect && Input.GetKeyUp(action) && canTalk) || branchSelect)
        {
            // �ش� NPC�� �´� ��ȭ ��ũ��Ʈ ����
            Invoke(name, 0);
        }
    }

    // MaskDude ��ȭ ��ũ��Ʈ
    void MaskDude()
    {
        canTalk = false;
        // ��ȭ�� ���� ������ ���� ���Ǻб�
        switch (textNum)
        {
            case 0:
                // ù ��ȭ �� ��ȭâ Ȱ��ȭ
                chatWindow.SetActive(true);
                // ��ȭ ��ũ��Ʈ
                questText.GetComponent<TextMeshProUGUI>().text = "Hi, I'm Mask Dude. I have a small problem. Can you help me?";
                break;
            case 1:
                // ������ Ȱ��ȭ
                chatWindow.GetComponent<ChatWindow>().BranchOn();
                break;
            case 2:
                // �������� ���� �� ���
                if (questAccept == 0)
                {
                    textNum--;
                    break;
                }
                branchSelect = false;
                // ������ ���� ��
                if (questAccept == 1)
                {
                    // ��ũ��Ʈ ����, ����Ʈ ����
                    questText.GetComponent<TextMeshProUGUI>().text = "Thank you. Too many ladybugs are bothering me. Kill 30 ladybugs please.";
                    PlayerPrefs.SetInt("CurrentQuest", 1);
                }
                // ���� �� ��ȭ ������
                else questText.GetComponent<TextMeshProUGUI>().text = "Bye.";
                break;
            case 3:
                // ��ȭ ����Ǿ� ��ȭâ ��Ȱ��ȭ
                chatWindow.SetActive(false);
                break;
            case 4:
                questText.GetComponent<TextMeshProUGUI>().text = "......"; ;
                chatWindow.SetActive(true);
                // ��ȭ ���� �� �߰����� ��ȭ �õ� �� ���� ��ũ��Ʈ ���� ���
                textNum -= 2;
                break;
        }
        // ��ȭ�� ���� ������ ���� ��ȭ�� �Ѿ�� ���� ������ ����
        textNum++;
        canTalk = true;
    }

    // Developer ��ȭ ��ũ��Ʈ
    void Developer()
    {
        canTalk = false;
        switch (textNum)
        {
            case 0:
                chatWindow.SetActive(true);
                questText.GetComponent<TextMeshProUGUI>().text = "Welcome to RPG game!";
                break;
            case 1:
                questText.GetComponent<TextMeshProUGUI>().text = "I am developer, and I want you to enjoy this game.";
                break;
            case 2:
                questText.GetComponent<TextMeshProUGUI>().text = "Thank you.";
                break;
            case 3:
                chatWindow.SetActive(false);
                textNum = -1;
                break;
        }
        textNum++;
        canTalk = true;
    }


    // NinjaFrog ��ȭ ��ũ��Ʈ
    void NinjaFrog()
    {
        // Ÿ�Ӿ��� ���������� ���� �ִ� �� ������Ʈ
        Transform startline = GameObject.Find("walls").transform.Find("Timeattack").transform.Find("startline");
        Transform tilemap = GameObject.Find("Grid").transform.Find("Tilemap");
        canTalk = false;
        switch (textNum)
        {
            case 0:
                chatWindow.SetActive(true);
                questText.GetComponent<TextMeshProUGUI>().text = "Hey, there is a maze. You can get a reward if you can complete this maze.";
                break;
            case 1:
                questText.GetComponent<TextMeshProUGUI>().text = "Will you try?";
                break;
            case 2:
                chatWindow.GetComponent<ChatWindow>().BranchOn();
                branchSelect = true;
                break;
            case 3:
                if (questAccept == 0) textNum--;
                else
                {
                    branchSelect = false;
                    if (questAccept == 1)
                    {
                        // Ÿ�Ӿ��� Ȱ��ȭ, 60��
                        timer.SetActive(true);
                        timer.GetComponent<TimeAttack>().timeAttack = 60;
                    }
                    else textNum = -1; // �����ϸ� �ʱ�ȭ
                    chatWindow.SetActive(false);
                }
                break;
            case 4:
                if (timer.activeSelf)
                {
                    // Ÿ�Ӿ��� ���� �� ��ȭ �� �ߴ� ���� ���
                    chatWindow.SetActive(true);
                    questText.GetComponent<TextMeshProUGUI>().text = "Will you stop?";
                    chatWindow.GetComponent<ChatWindow>().BranchOn();
                    branchSelect = true;
                }
                else textNum = -1; // Ÿ�Ӿ��� �ð��ʰ� �� �ʱ�ȭ
                break;
            case 5:
                if (questAccept == 0)
                {
                    textNum--;
                    break;
                }
                else
                {
                    branchSelect = false;
                    if (questAccept == 1) // ���� �ߴ� ��
                    {
                        // Ÿ�̸ӿ� ������ �þ� ����Ʈ ����, ��ŸƮ���� ��Ȱ��ȭ
                        timer.SetActive(false);
                        GameObject.Find("darkness").SetActive(false);
                        startline.gameObject.SetActive(true);
                        tilemap.GetComponent<TileChange>().RecoverTiles();
                        // ��ȭ �ʱ�ȭ
                        textNum = -1;
                    }
                    else textNum = textNum-2;
                    chatWindow.SetActive(false);
                }
                break;
        }
        questAccept = 0;
        textNum++;
        canTalk = true;
    }
}
