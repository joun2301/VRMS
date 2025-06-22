using UnityEngine;

public class StarCamera : MonoBehaviour
{
    public Transform mainTarget;      // 항상 바라볼 메인 타겟 (예: 태양)
    public Transform target;          // 현재 이동할 타겟 (행성)
    public Transform controlledCamera; // 제어할 카메라 (인스펙터 할당)
    public float orbitRadius = 20f;    // 기본 궤도 반경
    public float orbitSpeed = 20f;     // 기본 궤도 속도
    public float transitionTime = 2f;  // 이동/전환 시간

    // 초기값 백업
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // 궤도 관련 변수
    private float currentRadius;
    private float currentAngle;
    private bool isOrbiting;
    private bool isTransitioning;

    // Lerp 이동 관련 변수
    private Vector3 lerpStartPos;
    private Transform lerpTarget;
    private float lerpElapsed;
    private bool isLerping;

    // 백업 변수
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

        // 초기 위치/회전 저장
        initialPosition = controlledCamera.position;
        initialRotation = controlledCamera.rotation;
    }

    void OnEnable() // 추가된 부분
    {
        if (controlledCamera != null)
        {
            controlledCamera.position = initialPosition;
            controlledCamera.rotation = initialRotation;
        }

        // 상태 초기화
        isOrbiting = false;
        isLerping = false;
        isTransitioning = false;

        // target 및 lerpTarget도 초기화
        target = null;
        lerpTarget = null;
    }

    void OnDisable()
    {
        // 카메라 위치/회전 초기화
        if (controlledCamera != null)
        {
            controlledCamera.position = initialPosition;
            controlledCamera.rotation = initialRotation;
        }

        // 모든 상태 리셋
        isOrbiting = false;
        isLerping = false;
        isTransitioning = false;

        // target 및 lerpTarget도 초기화
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

        // 위치 이동
        controlledCamera.position = Vector3.Lerp(
            lerpStartPos,
            lerpTarget.position + new Vector3(0, 2.0f, 0),
            smoothT
        );

        // 부드러운 회전: Quaternion.RotateTowards 사용
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

        // 부드러운 회전 유지
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
