using UnityEngine;

public class StarAnimation : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem starParticleSystem; // 스타 애니메이션을 위한 파티클 시스템
    [SerializeField]
    private float startSpeed = 3.0f; // 스타 애니메이션의 시작 속도
    [SerializeField]
    private float increaseSpeed = 10.0f; // 스타 애니메이션의 속도 증가량

    private float currentSpeed; // 현재 스타 애니메이션의 속도
    private float elapsedTime = 0f; // 누적 시간

    void Start()
    {
        if (starParticleSystem == null)
        {
            Debug.LogError("Star Particle System is not assigned!");
            return;
        }
        currentSpeed = startSpeed;
        var main = starParticleSystem.main;
        main.simulationSpeed = currentSpeed;

        var colorOverLifetime = starParticleSystem.colorOverLifetime;
        colorOverLifetime.enabled = true;

        // 그라디언트 생성
        Gradient grad = new Gradient();
        grad.SetKeys(
            // 색상은 흰색으로 고정 (필요시 변경)
            new GradientColorKey[] {
                new GradientColorKey(Color.white, 0.0f),
                new GradientColorKey(Color.white, 1.0f)
            },
            // 알파: 0에서 1로 페이드인
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.0f, 0.0f),   // 시작: 투명
                new GradientAlphaKey(1.0f, 0.2f),   // 20% 시점: 불투명
                new GradientAlphaKey(1.0f, 1.0f)    // 끝: 불투명
            }
        );

        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(grad);
    }

    void Update()
    {
        if (starParticleSystem == null) return;

        // 5초 동안만 속도 증가
        if (elapsedTime <= 5.0f)
        {
            currentSpeed += increaseSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            var main = starParticleSystem.main;
            main.simulationSpeed = currentSpeed;
            Debug.Log("Current Speed: " + currentSpeed);
            Debug.Log("Elapsed Time: " + elapsedTime);
        }
        else
        {
            starParticleSystem.Stop();
        }
    }

    void OnDisable()
    {
        // 오브젝트가 비활성화될 때 상태를 초기화
        currentSpeed = startSpeed;
        elapsedTime = 0f;
        if (starParticleSystem != null)
        {
            var main = starParticleSystem.main;
            main.simulationSpeed = currentSpeed;
        }
    }
}