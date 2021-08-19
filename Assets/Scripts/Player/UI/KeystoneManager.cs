using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeystoneManager : MonoBehaviour
{
    [SerializeField] KeystoneManagerUI keystoneUI;

    int currentKeystones = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Keystone"))
        {
            keystoneUI.SetKeystoneUI(currentKeystones);
            //SoundManager.instance.PlaySoundEffect(SoundType.Key_Collect);
            currentKeystones++;
            collision.gameObject.SetActive(false);
        }
    }

    public bool UseKey()
    {
        if(currentKeystones > 0)
        {
            currentKeystones--;
            return true;
        }
        else
        {
            return false;
        }
    }
}
