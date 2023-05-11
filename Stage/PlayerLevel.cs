using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerLevel : MonoBehaviour
{
    public int level;
    public float damage;
    public float exp;
    public float maxHp;
    public float curHp;
    public int point;
    public int gold;
    public int chargeshot;
    public int dash;

    public void GetExp(float enemyExp)
    {
        level = PlayerPrefs.GetInt("LV");
        exp = PlayerPrefs.GetFloat("XP");

        exp += enemyExp;
        if (exp >= level)
        {
            exp -= level;
            PlayerPrefs.SetInt("LV", ++level);
            PlayerPrefs.SetInt("PTS", ++point);
            PlayerPrefs.SetFloat("CHP", PlayerPrefs.GetFloat("HP"));
        }
        PlayerPrefs.SetFloat("XP", exp);

    }

    public void DamageUp()
    {
        point = PlayerPrefs.GetInt("PTS");
        damage = PlayerPrefs.GetFloat("DMG");
        if (point > 0)
        {
            PlayerPrefs.SetFloat("DMG", ++damage);
            PlayerPrefs.SetInt("PTS", --point);
        }
        
    }

    public void PointUp()
    {
        point = PlayerPrefs.GetInt("PTS");
        gold = PlayerPrefs.GetInt("GOLD");
        if (gold >= 100)
        {
            PlayerPrefs.SetInt("PTS", ++point);
            PlayerPrefs.SetInt("GOLD", gold-100);
        }

    }

    public void HpUp()
    {
        point = PlayerPrefs.GetInt("PTS");
        maxHp = PlayerPrefs.GetFloat("HP");
        curHp = PlayerPrefs.GetFloat("CHP");
        if (point > 0)
        {
            PlayerPrefs.SetFloat("HP", ++maxHp);
            PlayerPrefs.SetFloat("CHP", ++curHp);
            PlayerPrefs.SetInt("PTS", --point);
        }

    }

    public void SkillLVUP(string skillLV)
    {
        if (PlayerPrefs.GetInt("PTS") > 0 && PlayerPrefs.GetInt(skillLV) < 10)
        {
            PlayerPrefs.SetInt(skillLV, PlayerPrefs.GetInt(skillLV) + 1);
            PlayerPrefs.SetInt("PTS", PlayerPrefs.GetInt("PTS") - 1);
        }
    }
    /*
    public void ChargeShotLVUP()
    {
        point = PlayerPrefs.GetInt("PTS");
        chargeshot = PlayerPrefs.GetInt("CHARGESHOTLV");
        if (point > 0 && chargeshot < 10)
        {
            PlayerPrefs.SetInt("CHARGESHOTLV", ++chargeshot);
            PlayerPrefs.SetInt("PTS", --point);
        }
    }
    public void DashLVUP()
    {
        point = PlayerPrefs.GetInt("PTS");
        dash = PlayerPrefs.GetInt("DASHLV");
        if (point > 0 && dash < 10)
        {
            PlayerPrefs.SetInt("DASHLV", ++dash);
            PlayerPrefs.SetInt("PTS", --point);
        }
    }*/

    public void TakeDamage(float enemyDmg, Animator a)
    {
        PlayerPrefs.SetFloat("CHP", PlayerPrefs.GetFloat("CHP")-enemyDmg);
        if (PlayerPrefs.GetFloat("CHP") <= 0)
        {
            a.SetBool("isDying", true);
            a.SetBool("isRunning", false);
            a.SetBool("isJumping", false);
            a.SetBool("isDashing", false);
            a.SetBool("isFalling", false);
            Invoke("Gameover", 1.0f);
        }
    }

    private void Gameover()
    {
        GameObject.Find("System").GetComponent<UIinfo>().Deactivate();
        SceneManager.LoadScene("GameOver");
    }

}
