using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    public bool isFlying = false;

    [Header("Air Resistance")]
    [SerializeField] private float dragCoefficient;
    [SerializeField] private float airDensity;
    [SerializeField] private float crossSectionArea;
    [SerializeField] private float speedMultiplier;

    [Header("Custom Gravity")]
    [SerializeField] private float gravityScale;

    private Rigidbody rb;
    private VelocityCalculator velocityCalculator;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        velocityCalculator = GetComponent<VelocityCalculator>();
    }

    void FixedUpdate()
    {
        if (isFlying)
        {
            ApplyQuadraticDrag();
            ApplyCustomGravity();
        }
    }

    void ApplyQuadraticDrag()
    {
        Vector3 velocity = rb.linearVelocity;
        float speed = velocity.magnitude;

        if (speed > 0.01f)
        {
            float dragMagnitude = 0.5f * airDensity * (speed * speed) * dragCoefficient * crossSectionArea;
            Vector3 dragForce = -velocity.normalized * dragMagnitude;

            rb.AddForce(dragForce, ForceMode.Force);
        }
    }

    void ApplyCustomGravity()
    {
        Vector3 customGravity = Physics.gravity * gravityScale;
        rb.AddForce(customGravity, ForceMode.Acceleration);
    }

    /**
     * Should be called if the object is being thrown. 
     */
    public void TriggerThrow()
    {
        rb.linearVelocity = speedMultiplier * velocityCalculator.GetBufferedVelocity();
    }

    public void OnBallReleased() { isFlying = true; rb.useGravity = false; }
    public void OnBallGrabbed() { isFlying = false; }
}