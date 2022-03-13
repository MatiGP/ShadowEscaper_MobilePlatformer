using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

namespace Code
{
    [CommandInfo("ShadowRunCommands", "Move Tutorial Character", "Moves tutorial character")]
    public class TutorialCharacterMoveCommand : Command
    {
        [SerializeField] private float m_Direction = 0;
        [SerializeField] private ETutorialCharacterAction m_AdditionalAction = ETutorialCharacterAction.None;

        public override void OnEnter()
        {
            base.OnEnter();

            TutorialController tutorialController = GetFlowchart().GetGameObjectVariable("TutorialCharacter").GetComponent<TutorialController>();
            tutorialController.SetDirection(m_Direction);

            switch (m_AdditionalAction)
            {
                case ETutorialCharacterAction.Jump:
                    tutorialController.Jump();
                    break;
                case ETutorialCharacterAction.Slide:
                    tutorialController.GroundSlide();
                    break;
            }

            Continue();
        }

        private enum ETutorialCharacterAction
        {
            None,
            Jump,
            Slide,
        }
    }
}


