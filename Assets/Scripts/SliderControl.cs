using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    public Slider volumeSlider; // Ссылка на слайдер для громкости
    public AudioSource targetAudioSource; // Ссылка на целевой AudioSource
    private AudioSource backgroundMusic;

    private const string VolumePlayerPrefsKey = "Volume"; // Ключ для сохранения громкости

    void Start()
    {
        // Присваиваем метод обработчика изменения слайдера
        volumeSlider.onValueChanged.AddListener(ChangeVolume);

        // Пытаемся загрузить предыдущее значение громкости из PlayerPrefs
        if (PlayerPrefs.HasKey(VolumePlayerPrefsKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumePlayerPrefsKey);
            volumeSlider.value = savedVolume;
            ChangeVolume(savedVolume);
        }

        // Находим Background-music по имени
        backgroundMusic = GameObject.Find("Background-music").GetComponent<AudioSource>();
    }


    // Метод для изменения громкости целевого AudioSource на основе значения слайдера
    private void ChangeVolume(float volume)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = volume;
            
            // Сохраняем значение громкости в PlayerPrefs
            PlayerPrefs.SetFloat(VolumePlayerPrefsKey, volume);
            PlayerPrefs.Save(); // Обязательно сохраните изменения
        }
    }
}
