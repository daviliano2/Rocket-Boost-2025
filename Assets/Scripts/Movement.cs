using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 10f;
    [SerializeField] float rotationStrength = 10f;
    [SerializeField] AudioClip mainEngine;

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
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput > 0)
        {
            // rb.freezeRotation = true;
            // transform.Rotate(fixedRotation * Vector3.back);
            // rb.freezeRotation = false;
            ApplyRotation(Vector3.back);
        }
        else if (rotationInput < 0)
        {
            ApplyRotation(Vector3.forward);
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
