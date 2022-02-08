using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMouseHover : MonoBehaviour
{
    public Animator HoverAnimation; //Assign hoverable object here, with animator that has a "IsHovering" bool prop and proper animations for start state, end state, start -> end, end -> start.

    public void OnHoverEnter()
    {
        //If player mouse hovering over credits hitbox, start hover animation.
        Debug.Log("Hovering over hitbox.");
        HoverAnimation.SetBool("IsHovered", true);
    }

    public void OnHoverExit()
    {
        //If player mouse no longer hovering over credits hitbox, start un-hover animation.
        Debug.Log("Not hovering over hitbox.");
        HoverAnimation.SetBool("IsHovered", false);
    }
}
