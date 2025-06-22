using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlanetMotion : MonoBehaviour
{
    public Transform orbitPivot;      // ���� �߽�(�¾�)
    public float orbitRadius = 10f;   // �¾���� �Ÿ�
    public float orbitSpeed = 10f;    // ���� �ӵ� (��/��)
    public float rotationSpeed = 30f; // ���� �ӵ� (��/��)
    public int orbitSegments = 100;   // �˵� �ð�ȭ�� ���׸�Ʈ ��

    private float angle;
    private LineRenderer lineRenderer;
    private Quaternion orbitRotation; // �˵� ȸ���� ����

    void Start()
    {
        // �˵� ȸ���� �ʱ�ȭ (x�� -20, z�� -10)
        orbitRotation = Quaternion.Euler(-20f, 0f, -10f);

        // ���� ��ġ ��� (ȸ�� ����)
        if (orbitPivot != null)
        {
            Vector3 offset = orbitRotation * new Vector3(orbitRadius, 0, 0);
            transform.position = orbitPivot.position + offset;
        }

        // ���� ������ ������Ʈ ����
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
        // ���� (ȸ�� ����)
        if (orbitPivot != null)
        {
            angle += orbitSpeed * Time.deltaTime;
            float rad = angle * Mathf.Deg2Rad;

            // ���� �˵� ��� �� ȸ�� ����
            Vector3 offset = new Vector3(Mathf.Cos(rad) * orbitRadius, 0, Mathf.Sin(rad) * orbitRadius);
            offset = orbitRotation * offset; // ȸ�� ����

            transform.position = orbitPivot.position + offset;
        }

        // ����
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
            points[i] = orbitPivot.position + orbitRotation * point; // ���� ȸ�� ����
        }
        lineRenderer.SetPositions(points);
        lineRenderer.enabled = true; // �˵� �ð�ȭ Ȱ��ȭ
    }
}
