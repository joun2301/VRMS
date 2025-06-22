using UnityEngine;

/// <summary>
/// 빈 오브젝트에 할당된 Transform 배열을 참조하고,
/// 카메라를 해당 위치로 이동시키는 스크립트.
/// </summary>

public class CameraLocation : MonoBehaviour
{
    public Transform[] locationTransforms = new Transform[10];
    public GameObject cameraRig;
    public int index = 0;
    
    /// <summary>
    /// 지정된 인덱스의 빈 오브젝트 Transform 정보를 카메라에 복사하여 적용.
    /// </summary>
    public void MoveCamera()
    {
        Random.state = Random.state; // 초기화
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
