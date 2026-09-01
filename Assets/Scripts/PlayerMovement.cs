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
    }

    // Update is called once per frame
    void Update()
    {
        AdhereToSurface();
        transform.Translate(new Vector3(moveValue.x, 0, moveValue.y) * moveSpeed * Time.deltaTime);
        if (moveValue != Vector2.zero)
        {
            SpawnGrass();
        }
    }

    private void AdhereToSurface()
    {
        RaycastHit hit;
        Vector3 raycastPosition = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        if (Physics.Raycast(raycastPosition, -transform.up, out hit, Mathf.Infinity))
        {
            transform.position = hit.point;
        }
    }

    private void SpawnGrass()
    {
        if (timer > 100)
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
