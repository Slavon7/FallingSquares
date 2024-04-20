using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] musicTracks; // Массив с аудиотреками

    private AudioSource audioSource;

    private bool isVolumeReduced = false; // Флаг для отслеживания уменьшения громкости
    private float originalVolume; // Исходная громкость аудио

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayRandomMusicTrack();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayRandomMusicTrack(); // Воспроизвести новый трек, если текущий закончился
        }
    }

    void PlayRandomMusicTrack()
    {
        if (musicTracks.Length > 0)
        {
            int randomIndex = Random.Range(0, musicTracks.Length);
            audioSource.clip = musicTracks[randomIndex];
            audioSource.Play();
        }
    }

    public void ReduceVolume()
    {
        if (!isVolumeReduced)
        {
            originalVolume = audioSource.volume;
            audioSource.volume = 0.2f; // Уменьшение громкости на 10%
            isVolumeReduced = true;
        }
    }

    public void RestoreVolume()
    {
        if (isVolumeReduced)
        {
            audioSource.volume = originalVolume; // Восстановление исходной громкости
            isVolumeReduced = false;
        }
    }
}