using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlanetMotion : MonoBehaviour
{
    public Transform orbitPivot;      // 공전 중심(태양)
    public float orbitRadius = 10f;   // 태양과의 거리
    public float orbitSpeed = 10f;    // 공전 속도 (도/초)
    public float rotationSpeed = 30f; // 자전 속도 (도/초)
    public int orbitSegments = 100;   // 궤도 시각화용 세그먼트 수

    private float angle;
    private LineRenderer lineRenderer;
    private Quaternion orbitRotation; // 궤도 회전값 저장

    void Start()
    {
        // 궤도 회전값 초기화 (x축 -20, z축 -10)
        orbitRotation = Quaternion.Euler(-20f, 0f, -10f);

        // 시작 위치 계산 (회전 적용)
        if (orbitPivot != null)
        {
            Vector3 offset = orbitRotation * new Vector3(orbitRadius, 0, 0);
            transform.position = orbitPivot.position + offset;
        }

        // 라인 렌더러 컴포넌트 참조
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    void OnEnable()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
            lineRenderer.positionCount = 0;
        }
        Invoke("DrawOrbit", 6.0f);
    }

    void OnDisable()
    {
        CancelInvoke("DrawOrbit");
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 0;
            lineRenderer.enabled = false;
        }
    }

    void Update()
    {
        // 공전 (회전 적용)
        if (orbitPivot != null)
        {
            angle += orbitSpeed * Time.deltaTime;
            float rad = angle * Mathf.Deg2Rad;

            // 원형 궤도 계산 후 회전 적용
            Vector3 offset = new Vector3(Mathf.Cos(rad) * orbitRadius, 0, Mathf.Sin(rad) * orbitRadius);
            offset = orbitRotation * offset; // 회전 적용

            transform.position = orbitPivot.position + offset;
        }

        // 자전
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void DrawOrbit()
    {
        if (orbitPivot == null || lineRenderer == null) return;

        lineRenderer.positionCount = orbitSegments + 1;
        lineRenderer.useWorldSpace = true;

        Vector3[] points = new Vector3[orbitSegments + 1];
        for (int i = 0; i <= orbitSegments; i++)
        {
            float theta = (float)i / orbitSegments * 2 * Mathf.PI;
            Vector3 point = new Vector3(Mathf.Cos(theta) * orbitRadius, 0, Mathf.Sin(theta) * orbitRadius);
            points[i] = orbitPivot.position + orbitRotation * point; // 동일 회전 적용
        }
        lineRenderer.SetPositions(points);
        lineRenderer.enabled = true; // 궤도 시각화 활성화
    }
}
