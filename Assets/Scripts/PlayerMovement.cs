using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private LayerMask layerMask;
    private Vector2 moveValue;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private GameObject grassObject;
    private int timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerMask = LayerMask.GetMask("Floor");
        AdhereToSurface();
    }

    // Update is called once per frame
    void Update()
    {
        AdhereMovement();
        //transform.Translate(new Vector3(moveValue.x, 0, moveValue.y) * moveSpeed * Time.deltaTime);
        if (moveValue.x > 0)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (moveValue.y != 0)
        {
            transform.Rotate(new Vector3(0, moveValue.y, 0));
        }
        if (moveValue != Vector2.zero)
        {
            SpawnGrass();
        }
        if (!HasGround())
        {
            Debug.Log("Ha");
            AdhereToSurface();
        }
    }

    private void AdhereToSurface()
    {
        RaycastHit hit;
        Vector3 raycastPosition = transform.TransformPoint(0, 0.6f, 0);
        if (Physics.Raycast(raycastPosition, -transform.up, out hit, 1.5f))
        {
            Debug.DrawLine(raycastPosition, hit.point, Color.purple, 1f);
            //transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
            transform.position = hit.point;
        }
    }

    private void AdhereMovement()
    {
        Vector3 savedRotation = transform.localEulerAngles;
        float savedYRotation = transform.localEulerAngles.y;
        RaycastHit hit;
        Vector3 raycastNormal;
        Vector3 raycastPosition = transform.TransformPoint(0, 0.1f, 0);
        for (int i = 1; i < 4; i++)
        {
            Debug.DrawRay(raycastPosition, transform.forward * (0.2f * i), Color.blue, 2f);
            if (!HasGround())
            {
                Debug.DrawRay(raycastPosition + transform.forward * (0.2f * i), -transform.up * (0.2f * i), Color.blue, 2f);
                Debug.DrawRay(raycastPosition + (transform.forward * (0.2f * i)) - (transform.up * (0.2f * i)), -transform.forward * (0.8f * i), Color.blue, 2f);
            }
            if (Physics.Raycast(raycastPosition, transform.forward, out hit, 0.2f * i))
            {
                raycastNormal = hit.normal;
                transform.up = raycastNormal;
                transform.position = hit.point;
                /**Vector3 newRotation = transform.localEulerAngles;
                newRotation.y = savedYRotation;
                transform.localEulerAngles = newRotation;**/
                break;

                //AdhereToSurface();
            }
            else if (Physics.Raycast(raycastPosition + transform.forward * (0.2f * i), -transform.up, out hit, 0.2f * i) && !HasGround())
            {
                raycastNormal = hit.normal;
                transform.up = raycastNormal;
                transform.position = hit.point;
                /**Vector3 newRotation = transform.localEulerAngles;
                newRotation.y = savedYRotation;
                transform.localEulerAngles = newRotation;**/
                break;
                //AdhereToSurface();
            }
            else if (Physics.Raycast(raycastPosition + (transform.forward * (0.2f * i)) - (transform.up * (0.2f * i)), -transform.forward, out hit, 0.8f * i) && !HasGround())
            {
                raycastNormal = hit.normal;
                transform.up = raycastNormal;
                transform.position = hit.point;
                /**Vector3 newRotation = transform.localEulerAngles;
                newRotation.y = savedYRotation;
                transform.localEulerAngles = newRotation;**/
                break;
                //AdhereToSurface();
            }
        }
    }

    private bool HasGround()
    {
        Vector3 raycastPos = transform.TransformPoint(0, 0.1f, 0);
        Debug.DrawRay(raycastPos, -transform.up * 0.3f, Color.green, 1f);
        return Physics.Raycast(raycastPos, -transform.up, 0.3f);
    }

    private void SpawnGrass()
    {
        if (timer > 200)
        {
            Instantiate(grassObject, transform.position, Quaternion.identity);
            timer = 0;
        }
        timer++;
    }

    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }
}
