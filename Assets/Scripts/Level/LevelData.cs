using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private int m_LevelIndex = -1;
    public int LevelIndex => m_LevelIndex;

    [SerializeField] private int m_KeysCount = -1;
    public int KeysCount => m_KeysCount;

    [SerializeField] private int m_ItemsCount = -1;
    public int ItemsCount => m_ItemsCount;

    [SerializeField] private float m_LevelDurationInSeconds = -1f;
    public TimeSpan LevelDuration => TimeSpan.FromSeconds(m_LevelDurationInSeconds);
}
