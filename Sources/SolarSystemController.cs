using UnityEngine;

public class SolarSystemController : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(0, 0, 500);
    public Vector3 targetPosition = new Vector3(0, 0, 50);
    public float duration = 6.0f; // 이동 시간 (초)

    private float elapsedTime = 0f;

    void Start()
    {
        transform.position = startPosition;
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            transform.position = Vector3.Lerp(startPosition, targetPosition, smoothT);
        }
    }

    private void OnDisable()
    {
        transform.position = startPosition;
        elapsedTime = 0f;
    }
}
