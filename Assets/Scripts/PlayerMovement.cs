//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private LayerMask layerMask;
    private Vector2 moveValue;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private GameObject grassObject;
    [SerializeField] private int grassSpawnDelay = 50;
    private int timer = 0;
    private RaycastHit movementHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerMask = LayerMask.GetMask("Floor", "Buildings");
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

        AdhereToSurface();
        if (!HasGround())
        {
            Debug.Log("Ha");
            //AdhereToSurface();
        }
    }

    private void AdhereToSurface()
    {
        RaycastHit hit;
        Vector3 raycastPosition = transform.TransformPoint(0, 0.6f, 0);
        if (Physics.Raycast(raycastPosition, -transform.up, out hit, 1.5f, layerMask))
        {
            Debug.DrawLine(raycastPosition, hit.point, Color.purple, 1f);
            //transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
            transform.position = hit.point;
        }
    }

    private void AdhereMovement()
    {
        movementHit = new RaycastHit();
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
            if (Physics.Raycast(raycastPosition, transform.forward, out movementHit, 0.2f * i, layerMask))
            {
                if (movementHit.transform.tag != "Barrier")
                {
                    raycastNormal = movementHit.normal;
                    Quaternion fromToRotation = Quaternion.FromToRotation(transform.up, raycastNormal);
                    transform.rotation = fromToRotation * transform.rotation;
                    break;
                }
            }
            else if (Physics.Raycast(raycastPosition + transform.forward * (0.2f * i), -transform.up, out movementHit, 0.2f * i, layerMask) && !HasGround())
            {
                if (movementHit.transform.tag != "Barrier")
                {
                    raycastNormal = movementHit.normal;
                    Quaternion fromToRotation = Quaternion.FromToRotation(transform.up, raycastNormal);
                    transform.rotation = fromToRotation * transform.rotation;
                    break;
                }
            }
            else if (Physics.Raycast(raycastPosition + (transform.forward * (0.2f * i)) - (transform.up * (0.2f * i)), -transform.forward, out movementHit, 0.8f * i, layerMask) && !HasGround())
            {
                if (movementHit.transform.tag != "Barrier")
                {
                    raycastNormal = movementHit.normal;
                    Quaternion fromToRotation = Quaternion.FromToRotation(transform.up, raycastNormal);
                    transform.rotation = fromToRotation * transform.rotation;
                    break;
                }
            }
        }
    }

    private bool HasGround()
    {
        Vector3 raycastPos = transform.TransformPoint(0, 0.1f, 0);
        Debug.DrawRay(raycastPos, -transform.up * 0.3f, Color.green, 1f);
        return Physics.Raycast(raycastPos, -transform.up, 0.3f, layerMask);
    }

    private void SpawnGrass()
    {
        if (timer > grassSpawnDelay)
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
