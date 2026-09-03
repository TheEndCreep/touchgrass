using UnityEngine;

public class RainFollow : MonoBehaviour
{
    public Transform player;
    public float offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + offset, player.position.z);
    }
}
