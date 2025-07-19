using System.Collections;
using UnityEngine;

public class DiamondTower : TowerBase
{
    [SerializeField] int bulletCnt; //4
    [SerializeField] int diamondAngle;
    [SerializeField] int delta; //90
    [SerializeField] float delayIn;
    [SerializeField] float delayDiamond;
    WaitForSeconds wfsCache2; //두,두두,두 (!) 두,두두,두 (!) 두,두두,두 

    new protected void Start()
    {
        base.Start();
        wfsCache = new WaitForSeconds(delayIn);
        wfsCache2 = new WaitForSeconds(delayDiamond);
    }

    protected override IEnumerator ShootTask()
    {
        isReady = false;
        yield return StartCoroutine(DiamondPattern());
        yield return new WaitForSeconds(patternInterval);
        isReady = true;
    }

    private IEnumerator DiamondPattern()
    {
        float angle = 0;
        for (int i = 0; i < bulletCnt; i++)
        {
            FireBulletOne(angle);
            yield return wfsCache;
            FireBulletOne(angle - diamondAngle);
            FireBulletOne(angle + diamondAngle);
            yield return wfsCache;
            FireBulletOne(angle);

            angle += delta;
            yield return wfsCache2;
        }
    }

    private void FireBulletOne(float angle)
    {
        float projectileDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float projectileDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector2 dir = new Vector2(projectileDirX, projectileDirY).normalized;
        FireBullet(dir);
    }
}
