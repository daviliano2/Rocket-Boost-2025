using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 10f;
    [SerializeField] float rotationStrength = 10f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;


    Rigidbody rb;
    AudioSource audioSource;

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource.Stop();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!mainBoosterParticles.isPlaying)
            {
                mainBoosterParticles.Play();
            }
        }
        else
        {
            mainBoosterParticles.Stop();
            audioSource.Stop();
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput > 0)
        {
            if (!leftBoosterParticles.isPlaying)
            {
                rightBoosterParticles.Stop();
                leftBoosterParticles.Play();
            }
            // rb.freezeRotation = true;
            // transform.Rotate(fixedRotation * Vector3.back);
            // rb.freezeRotation = false;
            ApplyRotation(Vector3.back);
        }
        else if (rotationInput < 0)
        {
            if (!rightBoosterParticles.isPlaying)
            {
                leftBoosterParticles.Stop();
                rightBoosterParticles.Play();
            }
            ApplyRotation(Vector3.forward);
        }
        else
        {
            leftBoosterParticles.Stop();
            rightBoosterParticles.Stop();

        }
    }

    private void ApplyRotation(Vector3 direction)
    {
        float fixedRotation = rotationStrength * Time.fixedDeltaTime;
        rb.freezeRotation = true;
        transform.Rotate(fixedRotation * direction);
        rb.freezeRotation = false;
    }
}
