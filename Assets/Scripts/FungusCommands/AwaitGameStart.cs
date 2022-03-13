using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

namespace Code
{
    [CommandInfo("ShadowRunCommands", "Await For Game Start", "Waits For Game Start")]
    public class AwaitGameStart : Command
    {
        public override void OnEnter()
        {
            base.OnEnter();
            ShadowRunApp.Instance.GameManager.OnGameStart += HandleGameStart;
        }

        private void HandleGameStart(object sender, System.EventArgs e)
        {
            ShadowRunApp.Instance.GameManager.OnGameStart -= HandleGameStart;
            Continue();
        }
    }
}
