using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegScript : MonoBehaviour
{
    public enum Player { Left, Middle, Right }
    public Player player = Player.Left;

    float inputH, inputV;

    public HingeJoint2D knee, hip, foot;

    public Rigidbody2D footRB;

    public float footSpeed = 2, kneeSpeed = 50;

    public Transform upperLeg, lowerLeg, footT;

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
        if (!footT.GetComponent<Collider2D>().IsTouching(GameObject.Find("Ground").GetComponent<Collider2D>()))
        {
            footRB.velocity = new Vector2(inputH * footSpeed * Time.deltaTime, footRB.velocity.y);
        }

        if (inputV > 0)
        {
            knee.useMotor = true;
            var km = knee.motor;
            km.motorSpeed = -Vector2.SignedAngle(upperLeg.up, lowerLeg.up);
            knee.motor = km;

            foot.useMotor = true;
            var fm = foot.motor;
            fm.motorSpeed = Vector2.SignedAngle(lowerLeg.up, footT.up)/5;
            foot.motor = fm;
        }
        else if (inputV < 0)
        {
            knee.useMotor = true;
            var m = knee.motor;
            float b = Vector2.SignedAngle(upperLeg.up, lowerLeg.up);
            b = Mathf.Clamp(b, -1, 1);
            m.motorSpeed = b * kneeSpeed;
            knee.motor = m;
        }
        else
        {
            knee.useMotor = false;
            foot.useMotor = false;
        }
        
    }
}
