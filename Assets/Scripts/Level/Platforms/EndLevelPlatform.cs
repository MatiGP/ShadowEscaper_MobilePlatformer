using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class EndLevelPlatform : MonoBehaviour
    {
        [SerializeField] private List<KeystoneSlotBox> keystoneSlotBoxes = null;
        [SerializeField] private EndPlatform platform = null;

        public void CheckSlotBoxes()
        {
            for (int i = 0; i < keystoneSlotBoxes.Count; i++)
            {
                if (!keystoneSlotBoxes[i].HasKey)
                {
                    return;
                }
            }

            platform.TurnOnEndLevelPlatform();
        }
    }
}
