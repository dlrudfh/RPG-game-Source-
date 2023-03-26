using UnityEngine.UI;
using UnityEngine;

public class EnemyHpViewer : MonoBehaviour
{
    private Enemy enemyHp;
    private Slider hpSlider;

    public void Setup(Enemy enemy)
    {
        enemyHp = enemy;
        hpSlider = GetComponent<Slider>();
    }
    private void Update()
    {
        hpSlider.value = (float)enemyHp.CurHp / (float)enemyHp.MaxHp;
    }
}
