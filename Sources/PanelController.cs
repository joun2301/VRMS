using UnityEngine;
using System.Collections;

public class PanelController : MonoBehaviour
{
    public float time = 0f;
    public float fadeInTime = 1.0f; // Fade In 시간 설정

    void Update()
    {
        time += Time.deltaTime; // 시간 업데이트
        if (time > 6.0f)
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        StartCoroutine(Coroutine_FadeIn());
    }

    // Fade In 코루틴
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
        renderer.alpha = 1f; // 최종적으로 완전히 불투명하게 설정
    }

    private void OnEnable()
    {
        var renderer = gameObject.GetComponent<CanvasGroup>();
        if (renderer != null)
        {
            renderer.alpha = 0f; // 초기 투명도 설정
        }
        time = 0f; // 시간 초기화
    }

    private void OnDisable()
    {
        time = 0f;
        var renderer = gameObject.GetComponent<CanvasGroup>();
        if (renderer != null)
        {
            renderer.alpha = 0f; // 비활성화 시 투명도 초기화
        }
    }
}
