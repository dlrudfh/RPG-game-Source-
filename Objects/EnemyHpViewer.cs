using UnityEngine.UI;
using UnityEngine;

// �� ü�� �� ����
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
        // �����̴� ���� ���� ü�� ������ �°� ������Ʈ
        hpSlider.value = (float)enemyHp.CurHp / (float)enemyHp.MaxHp;
    }
}
