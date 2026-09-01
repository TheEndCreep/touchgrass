using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    LayerMask layerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerMask = LayerMask.GetMask("Floor");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AdhereToSurface();
        transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
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
}
