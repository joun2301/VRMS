using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public Button startBtn_1;

    public GameObject corgi;
    public GameObject germanShepherd;
    public GameObject cur;
    public GameObject chihuahua;
    public GameObject pug;
    public GameObject interactionBackground;
    public GameObject cameraRig;

    private string btnName = "";

    // ���̵忡 �ɸ��� �ð�(�� ����). �ʿ信 ���� �ν����Ϳ��� ���� �����ϰ� SerializeField�� ������ ���� �ֽ��ϴ�.
    [SerializeField] private float fadeDuration = 1f;

    private void Start()
    {
        // ��ư�� ó������ ��Ȱ��ȭ
        startBtn_1.interactable = false;
    }

    /// <summary>
    /// UI ��ư�� ���õ� �� ȣ��˴ϴ�. 
    /// ���� ���õ� ��ư�� �̸��� �����ͼ� startBtn�� Ȱ��ȭ�մϴ�.
    /// </summary>
    public void CheckBtnName_1()
    {
        btnName = EventSystem.current.currentSelectedGameObject.name;
        startBtn_1.interactable = true;
    }

    /// <summary>
    /// startBtn Ŭ�� �� ȣ��˴ϴ�.
    /// btnName ���� ���� Interaction Ȥ�� Emotion ���� �ε��մϴ�.
    /// </summary>
    public void Btn_Start()
    {
        // Interaction Scene
        OnSceneLoaded();
        cameraRig.transform.position = new Vector3(0, 0.6f, -1.8f);
        interactionBackground.SetActive(true);
        
        if (btnName == "Button (1)")
        {
            germanShepherd.SetActive(true);
        }
        else if (btnName == "Button (2)")
        {
            corgi.SetActive(true);
        }
        else if (btnName == "Button (3)")
        {
            cur.SetActive(true);
        }
        else if (btnName == "Button (4)")
        {
            chihuahua.SetActive(true);
        }
        else if (btnName == "Button (5)")
        {
            pug.SetActive(true);
        }
    }

    /// <summary>
    /// ���� �ε�� ���� ȣ��Ǵ� �ݹ� �Լ��Դϴ�.
    /// �� �� �̸��� ���� ���� ��ī�̹ڽ��� ��� ���̵� ��/�ƿ��� �����մϴ�.
    /// </summary>
    private void OnSceneLoaded()
    {
        // Resources/Materials/Skyboxes �������� ��� ��Ƽ������ �ҷ���
        Material[] skyboxMaterials = Resources.LoadAll<Material>("Materials/Skyboxes");
        if (skyboxMaterials.Length > 0)
        {
            // �������� �ϳ� ����
            int randomIndex = Random.Range(0, skyboxMaterials.Length);
            Material newSkybox = skyboxMaterials[randomIndex];

            // ���̵� �ƿ� �� ��ü �� ���̵� �� ������ ó���ϴ� �ڷ�ƾ ����
            StartCoroutine(FadeSkyboxCoroutine(newSkybox));
        }
        else
        {
            Debug.LogWarning("Resources/Materials/Skyboxes ��ο� ��ī�̹ڽ� ��Ƽ������ �����ϴ�.");
        }
    }

    public void OnSceneLoaded_Star()
    {
        // Resources/Materials/Skyboxes �������� ��� ��Ƽ������ �ҷ���
        Material[] skyboxMaterials = Resources.LoadAll<Material>("Materials/Star");
        if (skyboxMaterials.Length > 0)
        {
            // �������� �ϳ� ����
            int randomIndex = Random.Range(0, skyboxMaterials.Length);
            Material newSkybox = skyboxMaterials[randomIndex];

            // ���̵� �ƿ� �� ��ü �� ���̵� �� ������ ó���ϴ� �ڷ�ƾ ����
            StartCoroutine(FadeSkyboxCoroutine(newSkybox));
        }
        else
        {
            Debug.LogWarning("Resources/Materials/Star ��ο� ��ī�̹ڽ� ��Ƽ������ �����ϴ�.");
        }
    }

    /// <summary>
    /// ��ī�̹ڽ� ���̵� �ƿ�/�� �ڷ�ƾ
    /// 1) ���� ��ī�̹ڽ� ����(Exposure)�� 1��0���� �ٿ��� ���̵� �ƿ�  
    /// 2) RenderSettings.skybox�� �� ��Ƽ����� ��ü  
    /// 3) �� ��Ƽ���� ������ 0��1�� �÷��� ���̵� ��  
    /// </summary>
    public IEnumerator FadeSkyboxCoroutine(Material newSkybox)
    {
        // 1) ���� skybox�� ���ų�, ���� ��ü�� ��Ƽ������ null�� ��� �ٷ� ��ü
        if (RenderSettings.skybox == null || newSkybox == null)
        {
            RenderSettings.skybox = newSkybox;
            yield break;
        }

        // 2) ���̵� �ƿ�: ���� ��Ƽ������ ����(Exposure)�� 1��0���� ���� ����
        Material currentMat = RenderSettings.skybox;

        // **����**: ���� ��Ƽ������ ���� �����ϸ� ��� ������ ������ ��Ƽ���� �ν��Ͻ��� �����ϹǷ�,
        // �ν��Ͻ��� ����(Clone)�Ͽ� ��� ����ϰ�, �ٽ� ������ �ǵ帮�� �ʵ��� �մϴ�.
        Material tempOldMat = Instantiate(currentMat);
        RenderSettings.skybox = tempOldMat;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration; // 0 ~ 1
            // ������ 1 �� 0���� ����
            float expoValue = Mathf.Lerp(1f, 0f, t);
            tempOldMat.SetFloat("_Exposure", expoValue);
            elapsed += Time.deltaTime;
            yield return null;
        }
        // ���������� 0���� ����
        tempOldMat.SetFloat("_Exposure", 0f);

        // 3) ��Ƽ���� ��ü
        Material tempNewMat = Instantiate(newSkybox);
        // ���̵� ���� ���� ������ 0���� �ʱ�ȭ
        tempNewMat.SetFloat("_Exposure", 0f);
        RenderSettings.skybox = tempNewMat;

        // 4) ���̵� ��: ������ 0 �� 1�� ���� ����
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration; // 0 ~ 1
            float expoValue = Mathf.Lerp(0f, 1f, t);
            tempNewMat.SetFloat("_Exposure", expoValue);
            elapsed += Time.deltaTime;
            yield return null;
        }
        // ���������� 1�� ����
        tempNewMat.SetFloat("_Exposure", 1f);

        // (����) �ӽ� �����ߴ� ���� ��Ƽ������ �� �̻� �ʿ����� �����Ƿ� Destroy
        Destroy(tempOldMat);
    }
}
