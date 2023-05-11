using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    bool healCooldown;

    public void Heal(AudioSource a)
    {
        float chp = PlayerPrefs.GetFloat("CHP");
        float hp = PlayerPrefs.GetFloat("HP");
        if (healCooldown) return;
        if (chp == hp)
            GameObject.Find("System").transform.Find("Notice").GetComponent<Notice>().message("Your HP is already full!");
        else
        {
            GameObject.Find("healParticle").GetComponent<ParticleSystem>().Play();
            a.Play();
            healCooldown = true;
            int amount;
            if (PlayerPrefs.GetInt("HEALLV") <= 4) amount = 1;
            else if (PlayerPrefs.GetInt("HEALLV") <= 9) amount = 2;
            else amount = 3;
            PlayerPrefs.SetFloat("CHP", (chp + amount > hp) ? hp : (chp + amount));
        }
        Invoke("HealCooldown", 30 - 2 * PlayerPrefs.GetInt("HEALLV"));
    }

    void HealCooldown()
    {
        healCooldown = false;
    }
}
