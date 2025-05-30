using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 10f;
    [SerializeField] float rotationStrength = 10f;

    Rigidbody rb;

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
