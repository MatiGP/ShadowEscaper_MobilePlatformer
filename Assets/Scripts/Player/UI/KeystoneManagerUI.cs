using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeystoneManagerUI : MonoBehaviour
{
    [SerializeField] List<Image> keystones;
    [SerializeField] Color32 obtainedKeystoneColor;

    private void Start()
    {
        /*
        int numberOfKeys = GameManager.instance.GetNumOfKeysInLevel();

        for(int i = 0; i < keystones.Count; i++)
        {
            if(i < numberOfKeys)
            {
                keystones[i].gameObject.SetActive(true);
            }
            else
            {
                keystones[i].gameObject.SetActive(false);
            }
        }
        */
    }

    public void SetKeystoneUI(int keystoneIndex)
    {
        keystones[keystoneIndex].color = obtainedKeystoneColor;
    }

}
