using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 싱글톤 (전역 접근)
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;     // 배경음악
    [SerializeField] private AudioSource sfxSource;     // 일반 효과음 (문 소리 등)
    [SerializeField] private AudioSource footstepSource; // 발소리 전용 (반복용)

    private void Awake()
    {
        // 싱글톤 처리
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // =========================
    // 🎵 BGM
    // =========================

    public void PlayBGM(AudioClip clip)
    {
        if (clip == null) return;

        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // =========================
    // 🔊 일반 SFX (문 소리 등)
    // =========================

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        sfxSource.PlayOneShot(clip);
    }

    // =========================
    // 👣 발소리 (전용)
    // =========================

    public void PlayFootstep(AudioClip clip)
    {
        if (clip == null) return;

        footstepSource.clip = clip;
        footstepSource.loop = false;
        footstepSource.Play();
    }

    public void StopFootstep()
    {
        footstepSource.Stop();
    }
}