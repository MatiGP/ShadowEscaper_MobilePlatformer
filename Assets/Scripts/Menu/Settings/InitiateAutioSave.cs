using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateAutioSave : MonoBehaviour
{
    [SerializeField] Settings settings;

    private void Start()
    {
        settings.SaveSettings();
    }
}
