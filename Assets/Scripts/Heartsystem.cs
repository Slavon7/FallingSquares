using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Heartsystem : MonoBehaviour
{
    public static int health = 3;
    public GameObject Heart1, Heart2, Heart3;
    public GameObject PlayerChar;
    private bool isDead = false;
    private bool isShieldActive = false;
    private bool isSpeedActive = false;
    private float shieldDuration = 15.0f;
    private float speedDuration = 15.0f;
    private float shieldEndTime = 0.0f;
    private float speedEndTime = 0.0f;
    private int originalHealth;
    private UnityEngine.Object explosion;
    private bool explosionCreated = false;

    [SerializeField]
    private GameObject objectToHide1;

    [SerializeField]
    private GameObject objectToHide2;

    [SerializeField]
    private GameObject gameover;

    [SerializeField]
    private AudioClip deathAudioClip;

    [SerializeField]
    private MusicManager musicManager;

    [SerializeField]
    private Image shieldImage;

    [SerializeField]
    private Image speedImage;

    [SerializeField]
    private PlayerMove playerMove;

    void Start()
    {
        gameover.SetActive(false);
        Time.timeScale = 1;
        musicManager = FindObjectOfType<MusicManager>();
        shieldImage.gameObject.SetActive(false);
        speedImage.gameObject.SetActive(false);
        explosion = Resources.Load("Explosion");
    }

    public void AddLife()
    {
        health++;
    }

    void Update()
    {
        if (!isShieldActive)
        {
            UpdateHeartsUI();
        }
        else
        {
            CheckShieldDuration();
        }

        if (isSpeedActive)
        {
            CheckSpeedDuration();
        }

        if (health == 0 && !isShieldActive)
        {
            HandleDeath();
        }
    }

    private void UpdateHeartsUI()
    {
        Heart1.SetActive(health >= 1);
        Heart2.SetActive(health >= 2);
        Heart3.SetActive(health >= 3);
    }

    private void CheckShieldDuration()
    {
        shieldEndTime = Mathf.Max(shieldEndTime, Time.time);

        if (Time.time >= shieldEndTime)
        {
            isShieldActive = false;
            health = originalHealth;

            if (shieldImage != null)
            {
                shieldImage.gameObject.SetActive(false);
            }
        }
    }

    private void CheckSpeedDuration()
    {
        speedEndTime = Mathf.Max(speedEndTime, Time.time);

        if (Time.time >= speedEndTime)
        {
            isSpeedActive = false;
            playerMove.moveSpeed /= 2.0f;

            if (speedImage != null)
            {
                speedImage.gameObject.SetActive(false);
            }
        }
    }

    private void HandleDeath()
    {
        // Проверяем, был ли объект Explosion уже создан, и если нет, то создаем его.
        if (!explosionCreated)
        {
            Score scoreScript = FindObjectOfType<Score>();
            scoreScript.BestScore();
            gameover.SetActive(true);
            isDead = true;
            GameObject explosionRef = (GameObject)Instantiate(explosion);
            explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            StartCoroutine(DeathAnimation(explosionRef));
            
            // Проиграть звук смерти в месте смерти игрока
            if (deathAudioClip != null)
            {
                AudioSource.PlayClipAtPoint(deathAudioClip, transform.position);
            }

            musicManager.ReduceVolume();

            if (PlayerChar != null)
            {
                Animator playerAnimator = PlayerChar.GetComponent<Animator>();
                if (playerAnimator != null)
                {
                    playerAnimator.enabled = true;
                }
                Invoke("HidePlayer", 0.4f);
            }

            if (objectToHide1 != null)
            {
                objectToHide1.SetActive(false);
            }

            if (objectToHide2 != null)
            {
                objectToHide2.SetActive(false);
            }
            
            // Устанавливаем флаг, что объект Explosion уже был создан.
            explosionCreated = true;
        }
    }
    
    IEnumerator DeathAnimation(GameObject explosionRef)
    {
        yield return new WaitForSeconds(explosionRef.GetComponent<ParticleSystem>().main.duration);
        Destroy(explosionRef);
        isDead = false;
    }

    // Метод для приховання PlayerChar
    private void HidePlayer()
    {
        if (PlayerChar != null)
        {
            PlayerChar.SetActive(false);
        }
    }

    public void ActivateShield()
    {
        isShieldActive = true;
        shieldEndTime = Time.time + shieldDuration;
        originalHealth = health;

        if (shieldImage != null)
        {
            shieldImage.gameObject.SetActive(true);
        }
    }

    public void ActivateSpeed()
    {
        // Увеличиваем значение moveSpeed только если скорость не активирована
        if (!isSpeedActive && playerMove != null)
        {
            playerMove.moveSpeed *= 2.0f;
            isSpeedActive = true; // Устанавливаем флаг активации скорости
            speedEndTime = Time.time + speedDuration;
            if (speedImage != null)
            {
                speedImage.gameObject.SetActive(true);
            }
        }
    }

    public void RestartScene(int sceneIndex)
    {
        Debug.Log("Рестарт запущен!");
        health = 3;
        SceneManager.LoadScene(sceneIndex);
        PlayerChar.gameObject.SetActive(true);
        gameover.SetActive(false);
        shieldImage.gameObject.SetActive(false);
        speedImage.gameObject.SetActive(false);
    }
}
