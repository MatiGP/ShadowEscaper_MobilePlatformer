using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;
    PlayerController playerController;

    //Animation states names;
    const string JUMP = "Jump";
    const string IN_AIR = "InAir";
    const string WALL_JUMP = "WallJump";
    const string JUMP_INCREASING_HEIGHT = "IncreasingHeight";
    const string WALL_SLIDE = "WallSlide";
    const string FALLING = "Falling";
    const string LANDING = "Landing";
    const string RUN = "PlayerRun";
    const string IDLE = "PlayerIdle";
    const string JUMP_PREP = "JumpPrep";
    const string SLIDE_START = "SlideStart";
    const string GROUND_SLIDE = "GroundSlide";
    const string SLIDE_END = "SlideEnd";

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        SetAnimations();
    }

    void SetAnimations()
    {
        if (playerController.IsGrounded)
        {
            if (playerController.MovementVector.x == 0)
            {
                animator.Play(IDLE);
            }
            else
            {
                animator.Play(RUN);
            }
        }
        else
        {
            
            if(playerController.MovementVector.y > 0)
            {
                animator.Play(JUMP_INCREASING_HEIGHT);
            }
            else if(playerController.MovementVector.y == 0) 
            {
                animator.Play(IN_AIR);
            }
            else
            {
                animator.Play(FALLING);
                animator.Play(LANDING);
            }
            
        }      
    }
}
