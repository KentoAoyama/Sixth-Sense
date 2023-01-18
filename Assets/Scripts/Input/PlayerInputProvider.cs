using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputProvider : IInputProvider
{
    public Vector2 GetMoveDir()
    {
        Vector2 inputDir = new();
        inputDir.x = Input.GetAxisRaw("Horizontal");
        inputDir.y = Input.GetAxisRaw("Vertical");
        
        return inputDir;
    }

    public bool GetFire()
    {
        return Input.GetButton("Fire1");
    }

    public bool GetCloseEye()
    {
        return Input.GetButton("Fire2");
    }
}
