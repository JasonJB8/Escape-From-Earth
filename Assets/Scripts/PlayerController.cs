using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10;
    public float jumpHeight = 10;
    public float sprintMultiplier = 2;
    public float gravity = 9.81f;
    public float airControl = 10;
    //Add fuel to jetpack?
    public float jetpackCharges = 1;
    public float jetpackMultiplier = 2;
    public AudioClip jetpackSFX;
    CharacterController controller;
    Vector3 input, moveDirection;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenuBehavior.isGamePaused)
        {
            if(!LevelManager.gameOver && !PlayerHealth.isDead)
            {
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");

                input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
                input *= moveSpeed;

                if(controller.isGrounded)
                {
                    jetpackCharges = 1;
                    if(Input.GetKey(KeyCode.LeftShift))
                    {
                        input *= sprintMultiplier;
                    }
                    moveDirection = input;
                    if(Input.GetButtonDown("Jump"))
                    {
                        moveDirection.y = Mathf.Sqrt(2 * jumpHeight);
                    }
                    else
                    {
                        moveDirection.y = 0.0f;
                    }
                }
                else
                {
                    if(Input.GetButtonDown("Jump") && jetpackCharges > 0)
                    {
                        jetpackCharges--;
                        AudioSource.PlayClipAtPoint(jetpackSFX, transform.position);
                        moveDirection.y = Mathf.Sqrt(2 * jumpHeight * jetpackMultiplier);
                    }
                    input.y = moveDirection.y;
                    moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
                }

                moveDirection.y -= gravity * Time.deltaTime;
                controller.Move(moveDirection * Time.deltaTime);
            }
        }
    }
}
