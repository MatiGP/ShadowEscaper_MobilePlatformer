using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanel : MonoBehaviour
{
    Animator animator;

    bool isDead;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead) return;

        if (Input.touchCount > 0)
        {
            LoadCurrentLevel();
        }
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EnableAnimatorWithDelay(float delay)
    {
        StartCoroutine(EnableAnimator(delay));
    }

    IEnumerator EnableAnimator(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.enabled = true;
        yield return new WaitForSeconds(0.4f);
        isDead = true;
    }
}
