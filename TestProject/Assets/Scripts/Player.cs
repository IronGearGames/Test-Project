using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask platformslayerMask;
    // private Player_Base playerBase;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;

    public float speed = 100f;

    // Start is called before the first frame update (Game Physics)
    void Start()
    {
        // playerBase = gameObject.GetComponent<Player_Base>();
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Update is called once per frame
    /*void Update()
    {
        
    }*/


    // 3D Example Code
    // float moveHorizontal = Input.GetAxis("Horizontal");                     // (Move A or D)
    // float moveVertical = Input.GetAxis("Vertical");                         // (Move W or S)

    // Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

    // rb.AddForce(movement * speed * Time.deltaTime);
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------

    // Allows the player to jump (Routine to check Player throughout the game)
    void FixedUpdate()
    {
        // Gravity Scale = 25 value-range
        // Freeze Z-Scale
        // Add Layer for Platforms (Set Platform sprite to Platform)
        // Set Layer Mask to Platforms

        // 2D Code for Player to jump
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            float jumpVelocity = 25f;
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
        }

        // DirectionControl???
        // HandleMovement_[Full/Some/No]MidAirControl();

        // Set Animations
        /*if (IsGrounded())
        {
            if (rigidbody2d.velocity.x == 0) {
                playerBase.PlayIdleAnim();
            }
            else {
                playerBase.PlayMoveAnim(new Vector2(rigidbody2d.velocity.x, 0f));
            }
        }
        else {
            playerBase.PlayJumpAnim(rigidbody2d.velocity);
        }*/
    }

    private bool IsGrounded() {
        // Checks if player is grounded by boxcast method instead of raycast
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down * .1f, platformslayerMask);
        // BoxcastHit2D boxcastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down * .1f, platformslayerMask);
        // Debug.Log(raycastHit2d.collider);
        // Debug.Log(boxcastHit2d.collider);
        return raycastHit2d.collider != null;
        // return boxcastHit2d.collider != null;
    }


    // Issue of Friction (Create New Physics_Material: NoFriction - Set to 0 => Set on Rigidbody 2D)
    private void HandleMovement_FullMidAirControl()
    {
        // Moves player midair left or right according to the key pressed
        float moveSpeed = 10f;
        if (Input.GetKey(KeyCode.LeftArrow)) {
            rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
        }
        else {
            if (Input.GetKey(KeyCode.RightArrow)) {
                rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
            }
            else {
                // No keys pressed
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
            }
        }
    }

    private void HandleMovement_SomeMidAirControl() {
        // Moves player midair left or right according to the key pressed
        float moveSpeed = 10f;
        float midAirControl = 3f;
        if (Input.GetKey(KeyCode.LeftArrow)) {
            if (IsGrounded()) {
                rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
            }
            else {
                rigidbody2d.velocity += new Vector2(-moveSpeed * midAirControl * Time.deltaTime, 0);
                rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed), rigidbody2d.velocity.y);
            }
        }
        else {
            if (Input.GetKey(KeyCode.RightArrow)) {
                if (IsGrounded()) {
                    rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
                }
                else {
                    rigidbody2d.velocity += new Vector2(+moveSpeed * midAirControl * Time.deltaTime, 0);
                    rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed), rigidbody2d.velocity.y);
                }
            }
            else {
                // No keys pressed
                if (IsGrounded()) {
                    rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                } 
            }
        }
    }

    private void HandleMovement_NoMidAirControl()
    {
        if (IsGrounded()) {
            // Moves player midair left or right according to the key pressed
            float moveSpeed = 10f;
            if (Input.GetKey(KeyCode.LeftArrow)) {
                rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
            }
            else {
                if (Input.GetKey(KeyCode.RightArrow)) {
                    rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
                }
                else {
                    // No keys pressed
                    rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                }
            }
        }
    }



}