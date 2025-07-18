using System.Collections;
using UnityEngine;

public class Tower135 : TowerBase
{
    [SerializeField] int bulletCnt; //8
    [SerializeField] int delta; //135
    [SerializeField] float delay;

    new protected void Start()
    {
        base.Start();
        wfsCache = new WaitForSeconds(delay);
    }

    protected override IEnumerator ShootTask()
    {
        isReady = false;
        yield return StartCoroutine(Pattern135());
        yield return new WaitForSeconds(patternInterval);
        isReady = true;
    }

    private IEnumerator Pattern135()
    {
        Debug.Log("135 Pattern");
        float angle = 45f;

        for (int i = 0; i < bulletCnt; i++)
        {
            float projectileDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float projectileDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 dir = new Vector2(projectileDirX, projectileDirY).normalized;
            FireBullet(dir);
            angle -= delta;
            yield return wfsCache;
        }
    }
}
