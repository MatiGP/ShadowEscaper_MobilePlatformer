using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnstablePlatform : MonoBehaviour
{
    [SerializeField] float slowBreakTime;
    [SerializeField] float fastBreakTime;
    [SerializeField] float disableDuration;
    [SerializeField] List<Sprite> platformSprites;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StartCoroutine(BreakDown());
        }
    }

    IEnumerator BreakDown()
    {
        for(int i = 0; i < 2; i++)
        {
            spriteRenderer.sprite = platformSprites[i];
            yield return new WaitForSeconds(slowBreakTime);
        }

        for(int i = 2; i < 5; i++)
        {
            spriteRenderer.sprite = platformSprites[i];
            yield return new WaitForSeconds(fastBreakTime);
        }

        spriteRenderer.sprite = null;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(disableDuration);
        spriteRenderer.sprite = platformSprites[0];
        boxCollider.enabled = true;

    }
}
