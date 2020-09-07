using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeystoneManagerUI : MonoBehaviour
{
    [SerializeField] List<Image> keystones;
    [SerializeField] Color32 obtainedKeystoneColor;

    public void SetKeystoneUI(int keystoneIndex)
    {
        keystones[keystoneIndex].color = obtainedKeystoneColor;
    }

}
