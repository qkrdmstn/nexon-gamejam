using System.Collections;
using UnityEngine;

public class SlowTower : TowerBase
{
    [SerializeField] int slowRate;
    [SerializeField] float fadeDuration;
    [SerializeField] SpriteRenderer iceArea;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<Monster>().DecreaseMoveSpeed(slowRate);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<Monster>().RestoreMoveSpeed();
        }
    }

    protected override IEnumerator ShootTask()
    {
        isReady = false;
        yield return StartCoroutine(IceArea());
        yield return new WaitForSeconds(patternInterval);
        isReady = true;
    }

    private IEnumerator IceArea()
    {
        yield return StartCoroutine(FadeToAlpha(0f, 0.2f, fadeDuration));
        yield return StartCoroutine(FadeToAlpha(0.2f, 0f, fadeDuration));
    }

    private IEnumerator FadeToAlpha(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = new Color(iceArea.color.r, iceArea.color.g, iceArea.color.b, 0);

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            iceArea.color = new Color(color.r, color.g, color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 마지막 프레임 보정
        iceArea.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
