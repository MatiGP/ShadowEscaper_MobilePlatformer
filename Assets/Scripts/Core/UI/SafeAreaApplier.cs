﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaApplier : MonoBehaviour
{
    private RectTransform m_RectTransform = null;
    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    private void ApplySafeArea()
    {
        Vector2 anchorMin = Screen.safeArea.position;
        Vector2 anchorMax = Screen.safeArea.position + Screen.safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;

        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        m_RectTransform.anchorMin = anchorMin;
        m_RectTransform.anchorMax = anchorMax;
    }
}
