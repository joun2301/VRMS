using UnityEngine;

public class StarAnimation : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem starParticleSystem; // ��Ÿ �ִϸ��̼��� ���� ��ƼŬ �ý���
    [SerializeField]
    private float startSpeed = 3.0f; // ��Ÿ �ִϸ��̼��� ���� �ӵ�
    [SerializeField]
    private float increaseSpeed = 10.0f; // ��Ÿ �ִϸ��̼��� �ӵ� ������

    private float currentSpeed; // ���� ��Ÿ �ִϸ��̼��� �ӵ�
    private float elapsedTime = 0f; // ���� �ð�

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

        // �׶���Ʈ ����
        Gradient grad = new Gradient();
        grad.SetKeys(
            // ������ ������� ���� (�ʿ�� ����)
            new GradientColorKey[] {
                new GradientColorKey(Color.white, 0.0f),
                new GradientColorKey(Color.white, 1.0f)
            },
            // ����: 0���� 1�� ���̵���
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.0f, 0.0f),   // ����: ����
                new GradientAlphaKey(1.0f, 0.2f),   // 20% ����: ������
                new GradientAlphaKey(1.0f, 1.0f)    // ��: ������
            }
        );

        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(grad);
    }

    void Update()
    {
        if (starParticleSystem == null) return;

        // 5�� ���ȸ� �ӵ� ����
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
        // ������Ʈ�� ��Ȱ��ȭ�� �� ���¸� �ʱ�ȭ
        currentSpeed = startSpeed;
        elapsedTime = 0f;
        if (starParticleSystem != null)
        {
            var main = starParticleSystem.main;
            main.simulationSpeed = currentSpeed;
        }
    }
}