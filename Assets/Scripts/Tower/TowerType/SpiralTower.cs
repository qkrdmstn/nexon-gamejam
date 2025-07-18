using System.Collections;
using UnityEngine;

public class SpiralTower : TowerBase
{
    [SerializeField] int bulletCnt;
    [SerializeField] int delta;
    [SerializeField] float delay;

    new protected void Start()
    {
        base.Start();
        wfsCache = new WaitForSeconds(delay);
    }

    protected override IEnumerator ShootTask()
    {
        isReady = false;
        yield return StartCoroutine(SpiralPattern());
        yield return new WaitForSeconds(patternInterval);
        isReady = true;
    }

    private IEnumerator SpiralPattern()
    {
        Debug.Log("Spiral Pattern");
        float angle = 0f;

        for (int i = 0; i < bulletCnt; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                // 회전 각도를 라디안으로 변환
                float projectileDirX = Mathf.Cos((angle + j * 90) * Mathf.Deg2Rad);
                float projectileDirY = Mathf.Sin((angle + j * 90) * Mathf.Deg2Rad);

                Vector2 dir = new Vector2(projectileDirX, projectileDirY).normalized;
                FireBullet(dir);
            }
            angle += delta;
            yield return wfsCache;
        }
    }
}
