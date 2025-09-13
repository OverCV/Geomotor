using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float motorForce = 1500f;
    public float brakeForce = 3000f;
    public float maxSteerAngle = 30f;
    
    [Header("Wheel Colliders")]
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    
    [Header("Wheel Meshes")]
    public Transform frontLeftWheelMesh;
    public Transform frontRightWheelMesh;
    public Transform rearLeftWheelMesh;
    public Transform rearRightWheelMesh;
    
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private bool isBraking;
    
    private Rigidbody carRigidbody;
    
    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        carRigidbody.centerOfMass = new Vector3(0, -0.5f, 0);
    }
    
    void Update()
    {
        GetInput();
    }
    
    void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
    
    void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);
    }
    
    void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        
        if (isBraking)
        {
            ApplyBraking();
        }
    }
    
    void ApplyBraking()
    {
        frontRightWheelCollider.brakeTorque = brakeForce;
        frontLeftWheelCollider.brakeTorque = brakeForce;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;
    }
    
    void HandleSteering()
    {
        steerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }
    
    void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelMesh);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelMesh);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelMesh);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelMesh);
    }
    
    void UpdateWheelPos(WheelCollider wheelCollider, Transform wheelMesh)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelMesh.rotation = rot;
        wheelMesh.position = pos;
    }
}