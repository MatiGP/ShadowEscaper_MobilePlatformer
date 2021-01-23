using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelRow : MonoBehaviour
{

    [SerializeField] GameObject[] buttonsGOs;
    
    public GameObject GetButtonGO(int index)
    {
        return buttonsGOs[index]; 
    }
}
