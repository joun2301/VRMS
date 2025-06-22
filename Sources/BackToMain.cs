using UnityEngine;

public class BackToMain : MonoBehaviour
{
    [SerializeField] private Transform cameraRig;
    [SerializeField] private GameObject Asset;
    [SerializeField] private SceneController sceneController;

    public void OnClickBackToMain()
    {
        Asset.SetActive(false);

        // Resources/Materials/Skyboxes �������� ��� ��Ƽ������ �ҷ���
        Material[] skyboxMaterials = Resources.LoadAll<Material>("Materials/Skyboxes");
        if (skyboxMaterials.Length > 0)
        {
            // �������� �ϳ� ����
            int randomIndex = Random.Range(0, skyboxMaterials.Length);
            Material newSkybox = skyboxMaterials[randomIndex];

            // ���̵� �ƿ� �� ��ü �� ���̵� �� ������ ó���ϴ� �ڷ�ƾ ����
            StartCoroutine(sceneController.FadeSkyboxCoroutine(newSkybox));
        }
        else
        {
            Debug.LogWarning("Resources/Materials/Skyboxes ��ο� ��ī�̹ڽ� ��Ƽ������ �����ϴ�.");
        }

        cameraRig.transform.position = new Vector3(0, 0.2f, -2);
        cameraRig.transform.rotation = new Quaternion(0, 0, 0, 1);
    }

    public void OnClickMeditationStart()
    {
        // Resources/Materials/Skyboxes �������� ��� ��Ƽ������ �ҷ���
        Material[] skyboxMaterials = Resources.LoadAll<Material>("Materials/Skyboxes");
        if (skyboxMaterials.Length > 0)
        {
            // �������� �ϳ� ����
            int randomIndex = Random.Range(0, skyboxMaterials.Length);
            Material newSkybox = skyboxMaterials[randomIndex];

            // ���̵� �ƿ� �� ��ü �� ���̵� �� ������ ó���ϴ� �ڷ�ƾ ����
            StartCoroutine(sceneController.FadeSkyboxCoroutine(newSkybox));
        }
        else
        {
            Debug.LogWarning("Resources/Materials/Skyboxes ��ο� ��ī�̹ڽ� ��Ƽ������ �����ϴ�.");
        }
    }
}
