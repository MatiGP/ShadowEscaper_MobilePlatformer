using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private string m_LevelName = "";
    public string LevelName => m_LevelName;

    [SerializeField] private int m_KeysCount = -1;
    public int KeysCount => m_KeysCount;

    [SerializeField] private int m_ItemsCount = -1;
    public int ItemsCount => m_ItemsCount;

    [SerializeField] private bool m_IsTimed = true;
    public bool IsTimed => m_IsTimed;

    [SerializeField] private float m_LevelDurationInSeconds = -1f;
    public float LevelDurationInSeconds => m_LevelDurationInSeconds;
}
