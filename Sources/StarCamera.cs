using UnityEngine;

public class StarCamera : MonoBehaviour
{
    public Transform mainTarget;      // �׻� �ٶ� ���� Ÿ�� (��: �¾�)
    public Transform target;          // ���� �̵��� Ÿ�� (�༺)
    public Transform controlledCamera; // ������ ī�޶� (�ν����� �Ҵ�)
    public float orbitRadius = 20f;    // �⺻ �˵� �ݰ�
    public float orbitSpeed = 20f;     // �⺻ �˵� �ӵ�
    public float transitionTime = 2f;  // �̵�/��ȯ �ð�

    // �ʱⰪ ���
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // �˵� ���� ����
    private float currentRadius;
    private float currentAngle;
    private bool isOrbiting;
    private bool isTransitioning;

    // Lerp �̵� ���� ����
    private Vector3 lerpStartPos;
    private Transform lerpTarget;
    private float lerpElapsed;
    private bool isLerping;

    // ��� ����
    private float initialOrbitRadius;
    private float transitionStartTime;

    void Start()
    {
        if (controlledCamera == null)
        {
            Debug.LogError("Camera not assigned!");
            enabled = false;
            return;
        }

        // �ʱ� ��ġ/ȸ�� ����
        initialPosition = controlledCamera.position;
        initialRotation = controlledCamera.rotation;
    }

    void OnEnable() // �߰��� �κ�
    {
        if (controlledCamera != null)
        {
            controlledCamera.position = initialPosition;
            controlledCamera.rotation = initialRotation;
        }

        // ���� �ʱ�ȭ
        isOrbiting = false;
        isLerping = false;
        isTransitioning = false;

        // target �� lerpTarget�� �ʱ�ȭ
        target = null;
        lerpTarget = null;
    }

    void OnDisable()
    {
        // ī�޶� ��ġ/ȸ�� �ʱ�ȭ
        if (controlledCamera != null)
        {
            controlledCamera.position = initialPosition;
            controlledCamera.rotation = initialRotation;
        }

        // ��� ���� ����
        isOrbiting = false;
        isLerping = false;
        isTransitioning = false;

        // target �� lerpTarget�� �ʱ�ȭ
        target = null;
        lerpTarget = null;
    }

    void Update()
    {
        HandleOrbit();
        HandleLerpMovement();
    }

    void HandleOrbit()
    {
        if (!isOrbiting || target == null) return;

        if (isTransitioning)
        {
            float t = (Time.time - transitionStartTime) / transitionTime;
            t = Mathf.SmoothStep(0, 1, t);

            currentRadius = Mathf.Lerp(initialOrbitRadius, orbitRadius, t);
            currentAngle += orbitSpeed * Time.deltaTime * Mathf.Deg2Rad;
            UpdateOrbitPosition(t);

            if (t >= 1f) isTransitioning = false;
        }
        else
        {
            currentAngle += orbitSpeed * Time.deltaTime * Mathf.Deg2Rad;
            UpdateOrbitPosition(1f);
        }
    }

    void HandleLerpMovement()
    {
        if (!isLerping || lerpTarget == null) return;

        lerpElapsed += Time.deltaTime;
        float t = Mathf.Clamp01(lerpElapsed / transitionTime);
        float smoothT = Mathf.SmoothStep(0f, 1f, t);

        // ��ġ �̵�
        controlledCamera.position = Vector3.Lerp(
            lerpStartPos,
            lerpTarget.position + new Vector3(0, 2.0f, 0),
            smoothT
        );

        // �ε巯�� ȸ��: Quaternion.RotateTowards ���
        if (mainTarget != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(mainTarget.position - controlledCamera.position);
            controlledCamera.rotation = Quaternion.RotateTowards(controlledCamera.rotation, targetRotation, 90f * Time.deltaTime);
        }
    }

    void UpdateOrbitPosition(float t)
    {
        Vector3 orbitPos = new Vector3(
            Mathf.Cos(currentAngle) * currentRadius, 0, Mathf.Sin(currentAngle) * currentRadius);

        controlledCamera.position = Vector3.Lerp(controlledCamera.position, target.position + orbitPos, t * 0.5f);

        // �ε巯�� ȸ�� ����
        if (mainTarget != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(mainTarget.position - controlledCamera.position);
            controlledCamera.rotation = Quaternion.Slerp(controlledCamera.rotation, targetRotation, t * 0.5f);
        }
    }

    public void StartOrbit(Transform newTarget)
    {
        if (newTarget == null) return;

        target = newTarget;
        initialOrbitRadius = (controlledCamera.position - target.position).magnitude;
        transitionStartTime = Time.time;

        isOrbiting = true;
        isTransitioning = true;
    }

    public void MoveToTarget(Transform newTarget)
    {
        if (newTarget == null) return;

        lerpTarget = newTarget;
        lerpStartPos = controlledCamera.position;
        lerpElapsed = 0f;

        isLerping = true;
        isOrbiting = false;
    }    
}
