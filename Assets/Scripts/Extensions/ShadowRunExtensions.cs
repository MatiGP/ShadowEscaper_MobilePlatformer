using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShadowRunExtensions
{
    public static string ToLevelName(this int levelNum)
    {        
        return string.Format(LevelLoader.LEVEL_NAME_FORMAT, levelNum.ToString("D2"));
    }
}
