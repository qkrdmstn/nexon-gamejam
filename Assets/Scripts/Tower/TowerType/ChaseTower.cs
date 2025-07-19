using System.Collections;
using UnityEngine;

public class ChaseTower : TowerBase
{
    [SerializeField] int bulletCnt;
    [SerializeField] float delay;
    private GameObject playerObj;

    new protected void Start()
    {
        base.Start();
        playerObj = GameObject.FindWithTag("Player");
        wfsCache = new WaitForSeconds(delay);
    }

    protected override IEnumerator ShootTask()
    {
        isReady = false;
        yield return StartCoroutine(ChasePattern());
        yield return new WaitForSeconds(patternInterval);
        isReady = true;
    }

    private IEnumerator ChasePattern()
    {
        for (int i = 0; i < bulletCnt; i++)
        {
            var dir = (playerObj.transform.position - transform.position).normalized;
            FireBullet(dir);
            yield return wfsCache;
        }
    }
}
