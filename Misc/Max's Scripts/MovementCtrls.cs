using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementCtrls : MonoBehaviour {
    public float move, strafe, yRot, rotSpeed, yRotSens;
    public float speed = 0.1f;
    [Space(20)]
    private GameObject ctrlCanvas;
    public Slider yRotSlider;
    [Space(15)]

    public bool canJump = true;
    public float jumpTimer = 0;
    public float jumpTimerMeet = 0.01f;
    public int jumpCounter = 0;
    public float jumpForce = 500;
    public float reach = 0.1f;
    public bool grounded;

    [Space(5)]
    public int dblActive = 2;

    private Rigidbody rb_Player;

    // Use this for initialization
    void Start ()
    {
        grounded = true;
        canJump = true;
        jumpCounter = 0;
        rb_Player = GetComponent<Rigidbody>();

        if (PlayerPrefs.HasKey("yRot_Slider") == false)
        {
            PlayerPrefs.SetFloat("yRot_Slider", 0.5f);
        }

        //yRotSlider/*ctrlCanvas*/ =  /*GameObject.Find("ySlider").GetComponent<Slider>();*/
        //yRotSlider = ctrlCanvas.GetComponentInChildren<Slider>().GetComponent<>();


    }
	
	// Update is called once per frame
	void Update ()
    {

        if (PlayerPrefs.GetInt("dblActive") > 0)
        {
            dblActive = 2;
        }
        else dblActive = 1;

        yRotSens = PlayerPrefs.GetFloat("yRot_Slider");/*yRotSlider.value;*/
        move = (Input.GetAxis("Vertical") * speed);
        strafe = (Input.GetAxis("Horizontal") * speed);

        

        //transform.forward += new Vector3(strafe, 0, move);
        if (Input.GetAxis("HorizontalRot") > 0.1 || -0.1 > Input.GetAxis("HorizontalRot"))
        {
            yRot = (Input.GetAxis("HorizontalRot") * rotSpeed * yRotSens);
            //SoundManager.PlaySfx("");
        }
        else yRot = 0;
        {
            if ((Input.GetKeyDown("joystick 1 button 0") || Input.GetButtonDown("Jump"))&& jumpCounter < dblActive ) //jumps
            {
                print("up");
                //play partical jump
                jumpCounter = jumpCounter + 1;
                rb_Player.AddForce(Vector3.up * jumpForce);
                canJump = false;
                //play jump animation
            }
            RaycastHit hit = new RaycastHit();

            if (canJump == false)
            {
                jumpTimer += Time.deltaTime;
                grounded = false;
            }


            {
                //var distanceToGround = hit.distance;  //var nameTag = hit.transform.tag;  //print(distanceToGround.ToString("F0"));

                Debug.DrawRay(transform.position, new Vector3(0, -reach, 0), Color.yellow);

                
                if (jumpTimer > jumpTimerMeet)   //if jump timer is > 0.1 then allow reset of jump
                {
                    if (Physics.Raycast(transform.position, -Vector3.up, out hit, reach) && ((hit.transform.tag == "Ground") || (hit.transform.tag == "Box")))
                    {
                        Grounded();
                    }
                }

            }
        } //jumping

        //shoot raycast, add force in vector of ray

        transform.localEulerAngles+= new Vector3(0,yRot,0);
    }

    private void FixedUpdate()
    {
        transform.localPosition += (transform.forward * move) + (transform.right * strafe);
    }
    public void Grounded()
    {
        canJump = true;
        jumpTimer = 0;
        jumpCounter = 0;
        grounded = true;
        
    }
}
