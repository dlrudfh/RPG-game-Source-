using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerLevel : MonoBehaviour
{
    public int level;
    public int damage;
    public int exp;
    public int maxHp;
    public int curHp;
    public int point;
    public int gold;
    public int chargeshot;
    public int dash;

    public void GetExp(int enemyExp)
    {
        level = PlayerPrefs.GetInt("LV");
        exp = PlayerPrefs.GetInt("XP");

        exp += enemyExp;
        if (exp >= level)
        {
            exp -= level;
            PlayerPrefs.SetInt("LV", ++level);
            PlayerPrefs.SetInt("PTS", ++point);
            PlayerPrefs.SetInt("CHP", PlayerPrefs.GetInt("HP"));
        }
        PlayerPrefs.SetInt("XP", exp);
        
    }

    public void DamageUp()
    {
        point = PlayerPrefs.GetInt("PTS");
        damage = PlayerPrefs.GetInt("DMG");
        if (point > 0)
        {
            PlayerPrefs.SetInt("DMG", ++damage);
            PlayerPrefs.SetInt("PTS", --point);
        }
        
    }

    public void HpUp()
    {
        point = PlayerPrefs.GetInt("PTS");
        maxHp = PlayerPrefs.GetInt("HP");
        curHp = PlayerPrefs.GetInt("CHP");
        if (point > 0)
        {
            PlayerPrefs.SetInt("HP", ++maxHp);
            PlayerPrefs.SetInt("CHP", ++curHp);
            PlayerPrefs.SetInt("PTS", --point);
        }

    }
    public void ChargeShotLVUP()
    {
        point = PlayerPrefs.GetInt("PTS");
        chargeshot = PlayerPrefs.GetInt("CHARGESHOT");
        if (point > 0 && chargeshot < 10)
        {
            PlayerPrefs.SetInt("CHARGESHOT", ++chargeshot);
            PlayerPrefs.SetInt("PTS", --point);
        }

    }
    public void DashLVUP()
    {
        point = PlayerPrefs.GetInt("PTS");
        dash = PlayerPrefs.GetInt("DASH");
        if (point > 0 && dash < 10)
        {
            PlayerPrefs.SetInt("DASH", ++dash);
            PlayerPrefs.SetInt("PTS", --point);
        }

    }

    public void TakeDamage(int enemyDmg, Animator a)
    {
        PlayerPrefs.SetInt("CHP", PlayerPrefs.GetInt("CHP")-enemyDmg);
        if (PlayerPrefs.GetInt("CHP") <= 0)
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
        SceneManager.LoadScene("GameOver");
    }

}
