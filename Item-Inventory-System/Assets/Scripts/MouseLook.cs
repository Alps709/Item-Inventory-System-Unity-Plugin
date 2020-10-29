using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float MouseSensitivity = 100f;
    public Transform PlayerBody;
    public float CameraAngle = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        CameraAngle -= MouseY;
        CameraAngle = Mathf.Clamp(CameraAngle, -90.0f, 90.0f);
        transform.localRotation = Quaternion.Euler(CameraAngle, 0.0f, 0.0f);
        PlayerBody.Rotate(Vector3.up, MouseX);
    }
}
