using UnityEngine;

public class BackToMain : MonoBehaviour
{
    [SerializeField] private Transform cameraRig;
    [SerializeField] private GameObject Asset;
    [SerializeField] private SceneController sceneController;

    public void OnClickBackToMain()
    {
        Asset.SetActive(false);

        // Resources/Materials/Skyboxes 폴더에서 모든 머티리얼을 불러옴
        Material[] skyboxMaterials = Resources.LoadAll<Material>("Materials/Skyboxes");
        if (skyboxMaterials.Length > 0)
        {
            // 무작위로 하나 선택
            int randomIndex = Random.Range(0, skyboxMaterials.Length);
            Material newSkybox = skyboxMaterials[randomIndex];

            // 페이드 아웃 → 교체 → 페이드 인 순서로 처리하는 코루틴 실행
            StartCoroutine(sceneController.FadeSkyboxCoroutine(newSkybox));
        }
        else
        {
            Debug.LogWarning("Resources/Materials/Skyboxes 경로에 스카이박스 머티리얼이 없습니다.");
        }

        cameraRig.transform.position = new Vector3(0, 0.2f, -2);
        cameraRig.transform.rotation = new Quaternion(0, 0, 0, 1);
    }

    public void OnClickMeditationStart()
    {
        // Resources/Materials/Skyboxes 폴더에서 모든 머티리얼을 불러옴
        Material[] skyboxMaterials = Resources.LoadAll<Material>("Materials/Skyboxes");
        if (skyboxMaterials.Length > 0)
        {
            // 무작위로 하나 선택
            int randomIndex = Random.Range(0, skyboxMaterials.Length);
            Material newSkybox = skyboxMaterials[randomIndex];

            // 페이드 아웃 → 교체 → 페이드 인 순서로 처리하는 코루틴 실행
            StartCoroutine(sceneController.FadeSkyboxCoroutine(newSkybox));
        }
        else
        {
            Debug.LogWarning("Resources/Materials/Skyboxes 경로에 스카이박스 머티리얼이 없습니다.");
        }
    }
}
