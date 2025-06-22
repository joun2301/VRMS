using UnityEngine;

/// <summary>
/// �� ������Ʈ�� �Ҵ�� Transform �迭�� �����ϰ�,
/// ī�޶� �ش� ��ġ�� �̵���Ű�� ��ũ��Ʈ.
/// </summary>

public class CameraLocation : MonoBehaviour
{
    public Transform[] locationTransforms = new Transform[10];
    public GameObject cameraRig;
    public int index = 0;
    
    /// <summary>
    /// ������ �ε����� �� ������Ʈ Transform ������ ī�޶� �����Ͽ� ����.
    /// </summary>
    public void MoveCamera()
    {
        Random.state = Random.state; // �ʱ�ȭ
        index = Random.Range(0, locationTransforms.Length);

        if (index < 0 || index >= locationTransforms.Length)
        {
            Debug.LogError("Index out of bounds: " + index);
            return;
        }

        Transform targetTransform = locationTransforms[index];
        if (cameraRig != null && targetTransform != null)
        {
            cameraRig.transform.position = targetTransform.position;
            cameraRig.transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y - 1.8f, targetTransform.position.z);
            cameraRig.transform.rotation = targetTransform.rotation;
        }
        else
        {
            Debug.LogError("Camera rig or target transform is not assigned.");
        }
    }
}
