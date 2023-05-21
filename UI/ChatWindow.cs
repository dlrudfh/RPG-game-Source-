using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ȭâ
public class ChatWindow : MonoBehaviour
{
    [SerializeField] GameObject branch;

    //������ Ȱ��ȭ
    public void BranchOn()
    {
        branch.SetActive(true);
    }

    //������ ��Ȱ��ȭ
    public void BranchOff()
    {
        branch.SetActive(false);
    }

    //������ ���� ����
    public void BranchValue(int i)
    {
        //branchSelect�� true�� ������ ������ �Ϸ������� ������ ���� ��ȭ�� ����������, 
        //questAccept ���� ������ ���� or ���� ���� �Ǵ� ����
        GameObject.FindGameObjectWithTag("NPC").GetComponent<Npc>().questAccept = i;
        GameObject.FindGameObjectWithTag("NPC").GetComponent<Npc>().branchSelect = true;
        // ������ �Ϸ������Ƿ� ������ â ��Ȱ��ȭ
        BranchOff();
    }
}
