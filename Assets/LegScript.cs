﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegScript : MonoBehaviour
{
    public enum Player { Left, Middle, Right }
    public Player player = Player.Left;

    float inputH, inputV;

    public HingeJoint2D knee, hip;

    public Rigidbody2D foot;

    public float footSpeed = 2;

    public Transform upperLeg, lowerLeg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (player)
        {
            case Player.Left:
                inputH = Input.GetAxis("P1Horizontal");
                inputV = Input.GetAxis("P1Vertical");
                break;
            case Player.Middle:
                inputH = Input.GetAxis("P2Horizontal");
                inputV = Input.GetAxis("P2Vertical");
                break;
            case Player.Right:
                inputH = Input.GetAxis("P3Horizontal");
                inputV = Input.GetAxis("P3Vertical");
                break;
        }
    }

    private void LateUpdate()
    {
        foot.velocity = new Vector2(inputH * footSpeed * Time.deltaTime, foot.velocity.y);

        if (inputV > 0)
        {
            knee.useMotor = true;
            var m = knee.motor;
            m.motorSpeed = -Vector2.SignedAngle(upperLeg.up, lowerLeg.up);
            knee.motor = m;
            
        }
        else if (inputV < 0)
        {

        }
        else
        {
            knee.useMotor = false;
        }
        
    }
}
