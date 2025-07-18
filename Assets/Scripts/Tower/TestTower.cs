using System.Collections;
using UnityEngine;

public class TestTower : TowerBase
{
    [SerializeField] float patternInterval;

    protected override IEnumerator ShootTask()
    {
        isReady = false;
        BasicPattern(10);
        yield return new WaitForSeconds(patternInterval);
        isReady = true;
    }

    private void BasicPattern(int bulletNum)
    {
        Debug.Log("Basic Pattern");
        float anglePlus = 360f / bulletNum;
        float angle = 0f;

        for (int i = 0; i < bulletNum; i++)
        {
            // 회전 각도를 라디안으로 변환
            float projectileDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float projectileDirY = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector2 dir = new Vector2(projectileDirX, projectileDirY).normalized;
            FireBullet(dir);
            angle += anglePlus;
        }
    }
}
