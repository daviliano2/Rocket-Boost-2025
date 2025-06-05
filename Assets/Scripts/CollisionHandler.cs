using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;

    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.Stop();
    }

    void Update()
    {
        Debugkeys();
    }


    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        // Scene scene = SceneManager.GetActiveScene();
        // SceneManager.LoadScene(scene.buildIndex);
        // OR like below
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX, 0.1f);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        // Invoke is not the best way of doing this but for this project should be ok
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }

    void StartSuccessSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), levelLoadDelay);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isControllable || !isCollidable) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //* Do nothing when friendly
                break;
            case "Finish":
                // Invoke(nameof(ReloadLevel), levelLoadDelay);
                StartSuccessSequence();
                break;
            default:
                // ReloadLevel();
                StartCrashSequence();
                break;
        }
    }

    void Debugkeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            Debug.Log("Loading next level.");
            LoadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            Debug.Log("Collision is active: " + isCollidable);
            isCollidable = !isCollidable;
        }
    }
}
