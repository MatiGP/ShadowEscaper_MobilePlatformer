using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class UnstablePlatform : MonoBehaviour, IPlatform
    {
        [SerializeField] float slowBreakTime;
        [SerializeField] float fastBreakTime;
        [SerializeField] float disableDuration;
        [SerializeField] List<Sprite> platformSprites;

        bool isBreakingDown;
        SpriteRenderer spriteRenderer;
        BoxCollider2D boxCollider;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();
        }     

        IEnumerator BreakDown()
        {
            isBreakingDown = true;
            for (int i = 0; i < 2; i++)
            {
                spriteRenderer.sprite = platformSprites[i];
                yield return new WaitForSeconds(slowBreakTime);
            }

            for (int i = 2; i < 5; i++)
            {
                spriteRenderer.sprite = platformSprites[i];
                yield return new WaitForSeconds(fastBreakTime);
            }

            spriteRenderer.sprite = null;
            boxCollider.enabled = false;
            yield return new WaitForSeconds(disableDuration);
            spriteRenderer.sprite = platformSprites[0];
            boxCollider.enabled = true;
            isBreakingDown = false;

        }

        public void ActivatePlatform()
        {
            if (!isBreakingDown)
            {
                StartCoroutine(BreakDown());
            }
        }
    }
}
