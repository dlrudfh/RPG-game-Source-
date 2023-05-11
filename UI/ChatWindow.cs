using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatWindow : MonoBehaviour
{
    [SerializeField] GameObject branch;

    public void BranchOn()
    {
        branch.SetActive(true);
    }

    public void BranchOff()
    {
        branch.SetActive(false);
    }

    public void BranchValue(int i)
    {
        GameObject.FindGameObjectWithTag("NPC").GetComponent<Npc>().questAccept = i;
        GameObject.FindGameObjectWithTag("NPC").GetComponent<Npc>().branchSelect = true;
        BranchOff();
    }
}
