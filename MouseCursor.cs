using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseCursor : MonoBehaviour
{
    SpriteRenderer swordSpriteRenderer;
    SpriteRenderer wandSpriteRenderer;
    public Texture2D wandTexture;
    public Texture2D swordTexture;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.SetCursor(swordTexture, new Vector2(12, 11), CursorMode.Auto);

        //wandSpriteRenderer = gameObject.transform.Find("WandSprite").GetComponent<SpriteRenderer>();
        //swordSpriteRenderer = gameObject.transform.Find("SwordSprite").GetComponent<SpriteRenderer>();

        //Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(cursorPos.x, cursorPos.y, 1);
    }

    public void CastingSpell()
    {
        UnityEngine.Cursor.SetCursor(wandTexture, new Vector2(18,19), CursorMode.Auto);
        //swordSpriteRenderer.enabled = false;
        //wandSpriteRenderer.enabled = true;
    }
    public void StoppedCastingSpell()
    {
        UnityEngine.Cursor.SetCursor(swordTexture, new Vector2(12, 11), CursorMode.Auto);
        //swordSpriteRenderer.enabled = true;
        //wandSpriteRenderer.enabled = false;
    }
    private void OnLevelWasLoaded(int level)
    {
        //Cursor.visible = false;
    }
}
