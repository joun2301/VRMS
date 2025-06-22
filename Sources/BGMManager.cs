using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmSource; // AudioSource 컴포넌트를 Inspector에 할당
    private AudioClip[] bgmClips; // background music

    public void SetAudioSource()
    {
        bgmSource = GetComponent<AudioSource>();
        if (bgmSource == null)
        {
            Debug.LogError("bgmSource component is not assigned or found on this GameObject.");
            return;
        }

        // Resources.LoadAll 호출을 Start 메서드로 이동
        bgmClips = Resources.LoadAll<AudioClip>("Audios/Background");
        if (bgmClips.Length == 0)
        {
            Debug.LogWarning($"No audio clips found in Resources/Audios/Background");
            return;
        }

        Random.state = Random.state;
        AudioClip chosenBGMClip = bgmClips[Random.Range(0, bgmClips.Length)];
        bgmSource.clip = chosenBGMClip;
        bgmSource.Play();

    }

    public void StopAudio()
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
        else
        {
            Debug.LogWarning("BGM AudioSource is not playing or not assigned.");
        }
    }
}
