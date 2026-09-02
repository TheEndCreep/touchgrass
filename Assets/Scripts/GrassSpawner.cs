using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    [SerializeField] private float spawnAttempts = 10f;
    [SerializeField] private float spawnRadius = 3f;
    [SerializeField] private GameObject grass;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        CheckForSurfaces();
    }

    private void CheckForSurfaces()
    {
        Vector3 p = transform.position;
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(p.x, p.y + 0.25f, p.z), Vector3.down, out hit, spawnRadius - 0.5f))
        {
            if (hit.transform.tag == "Floor")
            {
                SpawnGrass(Vector3.down, "Floor");
                Debug.DrawLine(transform.position, hit.point, Color.green, 2f);
            }
        }

        if (Physics.Raycast(new Vector3(p.x, p.y - 0.25f, p.z), Vector3.up, out hit, spawnRadius - 0.5f))
        {
            if (hit.transform.tag == "Building")
            {
                SpawnGrass(Vector3.up, "Building");
                Debug.DrawLine(transform.position, hit.point, Color.green, 2f);
            }
        }

        if (Physics.Raycast(new Vector3(p.x, p.y, p.z - 0.25f), Vector3.forward, out hit, spawnRadius - 0.5f))
        {
            if (hit.transform.tag == "Building")
            {
                SpawnGrass(Vector3.forward, "Building");
                Debug.DrawLine(transform.position, hit.point, Color.green, 2f);
            }
        }

        if (Physics.Raycast(new Vector3(p.x, p.y, p.z + 0.25f), Vector3.back, out hit, spawnRadius - 0.5f))
        {
            if (hit.transform.tag == "Building")
            {
                SpawnGrass(Vector3.back, "Building");
                Debug.DrawLine(transform.position, hit.point, Color.green, 2f);
            }
        }

        if (Physics.Raycast(new Vector3(p.x - 0.25f, p.y, p.z), Vector3.right, out hit, spawnRadius - 0.5f))
        {
            if (hit.transform.tag == "Building")
            {
                SpawnGrass(Vector3.right, "Building");
                Debug.DrawLine(transform.position, hit.point, Color.green, 2f);
            }
        }

        if (Physics.Raycast(new Vector3(p.x + 0.25f, p.y, p.z), Vector3.left, out hit, spawnRadius - 0.5f))
        {
            if (hit.transform.tag == "Building")
            {
                SpawnGrass(Vector3.left, "Building");
                Debug.DrawLine(transform.position, hit.point, Color.green, 2f);
            }
        }
    }

    private void SpawnGrass(Vector3 direction, string terrainTag)
    {
        for (int i = 0; i < spawnAttempts; i++)
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
            Vector3 raycastPos = Vector3.zero;
            if (direction == Vector3.down)
            {
                raycastPos = new Vector3(randomPos.x, transform.position.y + 3f, randomPos.z);
            }
            else if (direction == Vector3.forward)
            {
                raycastPos = new Vector3(randomPos.x, randomPos.y, transform.position.z - 1f);
            }
            else if (direction == Vector3.back)
            {
                raycastPos = new Vector3(randomPos.x, randomPos.y, transform.position.z + 1f);
            }
            else if (direction == Vector3.right)
            {
                raycastPos = new Vector3(transform.position.x - 1f, randomPos.y, randomPos.z);
            }
            else if (direction == Vector3.left)
            {
                raycastPos = new Vector3(transform.position.x + 1f, randomPos.y, randomPos.z);
            }
            RaycastHit hit;
            if (Physics.Raycast(raycastPos, direction, out hit, 4f))
            {
                Debug.DrawLine(raycastPos, hit.point, Color.red, 2f);
                if (hit.transform.tag == terrainTag)
                {
                    Instantiate(grass, hit.point, Quaternion.identity);
                }
            }
        }
    }
}
