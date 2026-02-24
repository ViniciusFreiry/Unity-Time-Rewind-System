using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PolygonCollider2D), typeof(SpriteRenderer))]
public class SyncColliderWithSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;
    private Sprite currentSprite;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    void LateUpdate()
    {
        if (spriteRenderer.sprite != currentSprite)
        {
            currentSprite = spriteRenderer.sprite;
            UpdateColliderToMatchSprite();
        }
    }

    private void UpdateColliderToMatchSprite()
    {
        List<Vector2> newPath = new List<Vector2>();
        try
        {
            spriteRenderer.sprite.GetPhysicsShape(0, newPath);
        }
        catch
        {

        }

        polygonCollider.pathCount = 1;
        polygonCollider.SetPath(0, newPath.ToArray());
    }
}