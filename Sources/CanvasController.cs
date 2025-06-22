using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject[] Panels; // 4개의 Panel을 Inspector에 할당

    [SerializeField] private float fadeInTime = 1.0f;
    [SerializeField] private float fadeOutTime = 1.0f;

    void Start()
    {
        // 모든 패널을 비활성화
        foreach (GameObject panel in Panels)
        {
            if (panel != null)
            {
                panel.SetActive(false);
                var renderer = panel.GetComponent<CanvasGroup>();
                if (renderer != null)
                {
                    renderer.alpha = 0f; // 초기 투명도 설정
                }
            }
        }
        FadeIn(0); // 첫 번째 패널을 Fade In
    }

    // 인덱스로 Fade In
    public void FadeIn(int panelIndex)
    {
        StartCoroutine(Coroutine_FadeIn(panelIndex));
    }

    // 인덱스로 Fade Out
    public void FadeOut(int panelIndex)
    {
        StartCoroutine(Coroutine_FadeOut(panelIndex));
    }

    // Fade In 코루틴
    IEnumerator Coroutine_FadeIn(int panelIndex)
    {
        GameObject panel = Panels[panelIndex];
        panel.SetActive(true);
        var renderer = panel.GetComponent<CanvasGroup>();

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

    // Fade Out 코루틴
    IEnumerator Coroutine_FadeOut(int panelIndex)
    {
        GameObject panel = Panels[panelIndex];
        var renderer = panel.GetComponent<CanvasGroup>();

        float elapsed = 0f;

        while (elapsed < fadeOutTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeOutTime);
            renderer.alpha = alpha;
            elapsed += Time.deltaTime;
            yield return null;
        }

        renderer.alpha = 0f; // 최종적으로 완전히 투명하게 설정
        panel.SetActive(false); // 완전히 투명해지면 비활성화
    }
}
