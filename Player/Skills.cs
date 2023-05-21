using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    bool healCooldown;

    // �� ��ų
    public void Heal(AudioSource a)
    {
        float chp = PlayerPrefs.GetFloat("CHP");
        float hp = PlayerPrefs.GetFloat("HP");
        // ���� ���ð��� ��� �������� ����
        if (healCooldown) return;
        // ü���� �̹� ������ ���� ��� ��ų�� �ߵ����� �ʰ� ü���� ������ �ִٴ� �޽��� ���
        if (chp == hp) PlayerPrefs.SetString("Notice", "Your HP is already full!");
        //ü���� ���� �� ���� ���� ���
        else
        {
            // ��ƼŬ �ִϸ��̼� �� ȿ���� ���
            GameObject.Find("healParticle").GetComponent<ParticleSystem>().Play();
            a.Play();
            // ��Ÿ�� ���� ����
            healCooldown = true;
            int amount;
            //��ų ������ ���� ȸ������ �޶���
            if (PlayerPrefs.GetInt("HEALLV") <= 4) amount = 1;
            else if (PlayerPrefs.GetInt("HEALLV") <= 9) amount = 2;
            else amount = 3;
            //ȸ���� ��ŭ ü���� ȸ����(�ִ� ü���� �ѱ��� ����)
            PlayerPrefs.SetFloat("CHP", (chp + amount > hp) ? hp : (chp + amount));
        }
        // ���� �ð� ���� ��Ÿ�� 
        Invoke("HealCooldown", 30 - 2 * PlayerPrefs.GetInt("HEALLV"));
    }

    void HealCooldown()
    {
        //���� �ð� �� �ش� �Լ��� ȣ��Ǹ� ��ų ������ ������
        healCooldown = false;
    }
}
