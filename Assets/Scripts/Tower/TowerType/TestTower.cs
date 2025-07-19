using System.Collections;
using UnityEngine;

public class TestTower : TowerBase
{
    [SerializeField] int bulletCnt;
    protected override IEnumerator ShootTask()
    {
        isReady = false;
        BasicPattern();
        yield return new WaitForSeconds(patternInterval);
        isReady = true;
    }

    private void BasicPattern()
    {
        float anglePlus = 360f / bulletCnt;
        float angle = 0f;

        for (int i = 0; i < bulletCnt; i++)
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
