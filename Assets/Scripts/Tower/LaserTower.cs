using System.Collections;
using UnityEngine;

public class LaserTower : TowerBase
{
    [SerializeField] int bulletCnt; //8��
    [SerializeField] int repeatCnt; //3��
    [SerializeField] int delta; //45��
    [SerializeField] float delayIn;
    [SerializeField] float delayLaser;
    WaitForSeconds wfsCache2; //�εε� (!) �εε� (!) �εε� 

    new protected void Start()
    {
        base.Start();
        wfsCache = new WaitForSeconds(delayIn);
        wfsCache2 = new WaitForSeconds(delayLaser);
    }

    protected override IEnumerator ShootTask()
    {
        isReady = false;
        yield return StartCoroutine(LaserPattern());
        yield return new WaitForSeconds(patternInterval);
        isReady = true;
    }

    private IEnumerator LaserPattern()
    {
        Debug.Log("Laser Pattern");
        float angle = 0f;
        for (int i = 0; i < bulletCnt; i++)
        {
            // ȸ�� ������ �������� ��ȯ
            float projectileDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float projectileDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 dir = new Vector2(projectileDirX, projectileDirY).normalized;
            for (int j = 0; j < repeatCnt; j++)
            {
                yield return wfsCache;
                FireBullet(dir);
            }
            angle += delta;
            yield return wfsCache2;
        }
    }
}
