using UnityEngine;

public class GrowMoss : MonoBehaviour
{
    public SpriteRenderer[] spriteRenderers;
    public Sprite[] foliageSprites;
    private float finalScale;
    private float startScale = 0.01f;
    private float t = 0f;
    void Start()
    {
        finalScale = Random.Range(0.2f, 0.4f);
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        Sprite grassSprite = foliageSprites[Random.Range(0, foliageSprites.Length - 1)];
        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            renderer.sprite = grassSprite;
        }
    }

    void Update()
    {
        float nextScale = Mathf.Lerp(startScale, finalScale, t);
        transform.localScale = new Vector3(nextScale, 1f, nextScale);

        t += 0.5f * Time.deltaTime;

        if (t > 1f)
        {
            Destroy(this);
        }
    }
}
