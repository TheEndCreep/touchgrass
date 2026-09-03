using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCam : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float mouseSensitivity = 3f;
    [SerializeField] private float camDistance = 5f;
    [SerializeField] private float camHeight = 1f;
    [SerializeField] private float rotationHVerticalMin = -15f;
    [SerializeField] private float rotationVerticalMax = 60f;

    private float rotationVertical = 0f;
    private float rotationHorizontal = 0f;
    private bool isPlayerMoving = false;

    private Vector2 mouseValue;

    //hide cursor
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //rotate Player based on mouse movement
    void Update()
    {
        float mouseX = mouseValue.x * mouseSensitivity;
        rotationHorizontal += mouseX;

        //player.rotation = Quaternion.Euler(0f, rotationHorizontal, 0f);
    }

    //calc Camera looking towards Player
    void LateUpdate()
    {
        float mouseY = mouseValue.y * mouseSensitivity;
        rotationVertical -= mouseY;
        rotationVertical = Mathf.Clamp(rotationVertical, rotationHVerticalMin, rotationVerticalMax);

        float camRotationY = rotationHorizontal;

        Quaternion rotation = Quaternion.Euler(rotationVertical, camRotationY, 0f);
        Vector3 offset = rotation * new Vector3(0f, camHeight, -camDistance);

        transform.position = player.position + offset;
        transform.LookAt(player.position + Vector3.up * camHeight);
    }

    public void setPlayerMoving(bool moving)
    {
        isPlayerMoving = moving;
    }

    void OnLook(InputValue value)
    {
        mouseValue = value.Get<Vector2>();
    }
}

