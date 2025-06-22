using UnityEngine;
using System.Collections;

public class PanelController : MonoBehaviour
{
    public float time = 0f;
    public float fadeInTime = 1.0f; // Fade In �ð� ����

    void Update()
    {
        time += Time.deltaTime; // �ð� ������Ʈ
        if (time > 6.0f)
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        StartCoroutine(Coroutine_FadeIn());
    }

    // Fade In �ڷ�ƾ
    IEnumerator Coroutine_FadeIn()
    {
        gameObject.SetActive(true);
        var renderer = gameObject.GetComponent<CanvasGroup>();

        float elapsed = 0f;

        while (elapsed < fadeInTime)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsed / fadeInTime);
            renderer.alpha = alpha;
            elapsed += Time.deltaTime;
            yield return null;
        }
        renderer.alpha = 1f; // ���������� ������ �������ϰ� ����
    }

    private void OnEnable()
    {
        var renderer = gameObject.GetComponent<CanvasGroup>();
        if (renderer != null)
        {
            renderer.alpha = 0f; // �ʱ� ���� ����
        }
        time = 0f; // �ð� �ʱ�ȭ
    }

    private void OnDisable()
    {
        time = 0f;
        var renderer = gameObject.GetComponent<CanvasGroup>();
        if (renderer != null)
        {
            renderer.alpha = 0f; // ��Ȱ��ȭ �� ���� �ʱ�ȭ
        }
    }
}
