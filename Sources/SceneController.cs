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

    // 페이드에 걸리는 시간(초 단위). 필요에 따라 인스펙터에서 조절 가능하게 SerializeField로 노출할 수도 있습니다.
    [SerializeField] private float fadeDuration = 1f;

    private void Start()
    {
        // 버튼을 처음에는 비활성화
        startBtn_1.interactable = false;
    }

    /// <summary>
    /// UI 버튼이 선택될 때 호출됩니다. 
    /// 현재 선택된 버튼의 이름을 가져와서 startBtn을 활성화합니다.
    /// </summary>
    public void CheckBtnName_1()
    {
        btnName = EventSystem.current.currentSelectedGameObject.name;
        startBtn_1.interactable = true;
    }

    /// <summary>
    /// startBtn 클릭 시 호출됩니다.
    /// btnName 값에 따라 Interaction 혹은 Emotion 씬을 로드합니다.
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
    /// 씬이 로드된 직후 호출되는 콜백 함수입니다.
    /// 각 씬 이름에 따라 랜덤 스카이박스를 골라서 페이드 인/아웃을 수행합니다.
    /// </summary>
    private void OnSceneLoaded()
    {
        // Resources/Materials/Skyboxes 폴더에서 모든 머티리얼을 불러옴
        Material[] skyboxMaterials = Resources.LoadAll<Material>("Materials/Skyboxes");
        if (skyboxMaterials.Length > 0)
        {
            // 무작위로 하나 선택
            int randomIndex = Random.Range(0, skyboxMaterials.Length);
            Material newSkybox = skyboxMaterials[randomIndex];

            // 페이드 아웃 → 교체 → 페이드 인 순서로 처리하는 코루틴 실행
            StartCoroutine(FadeSkyboxCoroutine(newSkybox));
        }
        else
        {
            Debug.LogWarning("Resources/Materials/Skyboxes 경로에 스카이박스 머티리얼이 없습니다.");
        }
    }

    public void OnSceneLoaded_Star()
    {
        // Resources/Materials/Skyboxes 폴더에서 모든 머티리얼을 불러옴
        Material[] skyboxMaterials = Resources.LoadAll<Material>("Materials/Star");
        if (skyboxMaterials.Length > 0)
        {
            // 무작위로 하나 선택
            int randomIndex = Random.Range(0, skyboxMaterials.Length);
            Material newSkybox = skyboxMaterials[randomIndex];

            // 페이드 아웃 → 교체 → 페이드 인 순서로 처리하는 코루틴 실행
            StartCoroutine(FadeSkyboxCoroutine(newSkybox));
        }
        else
        {
            Debug.LogWarning("Resources/Materials/Star 경로에 스카이박스 머티리얼이 없습니다.");
        }
    }

    /// <summary>
    /// 스카이박스 페이드 아웃/인 코루틴
    /// 1) 현재 스카이박스 노출(Exposure)을 1→0으로 줄여서 페이드 아웃  
    /// 2) RenderSettings.skybox를 새 머티리얼로 교체  
    /// 3) 새 머티리얼 노출을 0→1로 올려서 페이드 인  
    /// </summary>
    public IEnumerator FadeSkyboxCoroutine(Material newSkybox)
    {
        // 1) 현재 skybox가 없거나, 새로 교체할 머티리얼이 null인 경우 바로 교체
        if (RenderSettings.skybox == null || newSkybox == null)
        {
            RenderSettings.skybox = newSkybox;
            yield break;
        }

        // 2) 페이드 아웃: 기존 머티리얼의 노출(Exposure)을 1→0으로 선형 감소
        Material currentMat = RenderSettings.skybox;

        // **주의**: 원래 머티리얼을 직접 변경하면 모든 씬에서 동일한 머티리얼 인스턴스를 공유하므로,
        // 인스턴스를 복사(Clone)하여 잠깐 사용하고, 다시 원본을 건드리지 않도록 합니다.
        Material tempOldMat = Instantiate(currentMat);
        RenderSettings.skybox = tempOldMat;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration; // 0 ~ 1
            // 노출을 1 → 0으로 보간
            float expoValue = Mathf.Lerp(1f, 0f, t);
            tempOldMat.SetFloat("_Exposure", expoValue);
            elapsed += Time.deltaTime;
            yield return null;
        }
        // 최종적으로 0으로 설정
        tempOldMat.SetFloat("_Exposure", 0f);

        // 3) 머티리얼 교체
        Material tempNewMat = Instantiate(newSkybox);
        // 페이드 인을 위해 노출을 0으로 초기화
        tempNewMat.SetFloat("_Exposure", 0f);
        RenderSettings.skybox = tempNewMat;

        // 4) 페이드 인: 노출을 0 → 1로 선형 증가
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration; // 0 ~ 1
            float expoValue = Mathf.Lerp(0f, 1f, t);
            tempNewMat.SetFloat("_Exposure", expoValue);
            elapsed += Time.deltaTime;
            yield return null;
        }
        // 최종적으로 1로 설정
        tempNewMat.SetFloat("_Exposure", 1f);

        // (선택) 임시 생성했던 구형 머티리얼은 더 이상 필요하지 않으므로 Destroy
        Destroy(tempOldMat);
    }
}
