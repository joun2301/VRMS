using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // AudioSource 컴포넌트를 Inspector에 할당
    private AudioClip[] audioClips; // main audio
    private string folderName = "";

    public void SetAudioSource()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is not assigned or found on this GameObject.");
            return;
        }
        
        float currentTime = TimeController.currentTime;
        int timeInt = Mathf.RoundToInt(currentTime);

        switch (timeInt)
        {
            case 5:
                folderName = "5min";
                break;
            case 10:
                folderName = "10min";
                break;
            case 15:
                folderName = "15min";
                break;
            case 20:
                folderName = "20min";
                break;
            case 25:
                folderName = "25min";
                break;
            case 30:
                folderName = "30min";
                break;
            case 35:
                folderName = "35min";
                break;
            case 40:
                folderName = "40min";
                break;
            case 45:
                folderName = "45min";
                break;
            case 50:
                folderName = "50min";
                break;
            case 55:
                folderName = "50min";
                break;
            case 60:
                folderName = "50min";
                break;
            default:
                Debug.LogWarning("TimeController.currentTime is not within the expected range.");
                return;
        }

        // Resources.LoadAll 호출을 Start 메서드로 이동
        audioClips = Resources.LoadAll<AudioClip>($"Audios/{folderName}");
        if (audioClips.Length == 0)
        {
            Debug.LogWarning($"No audio clips found in Resources/Audios/{folderName}");
            return;
        }

        // 무작위로 오디오 클립 선택
        Random.state = Random.state;
        AudioClip chosenClip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.clip = chosenClip;
        audioSource.Play();
    }

    public void StopAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            Debug.LogWarning("AudioSource is not playing or not assigned.");
        }
    }
}
