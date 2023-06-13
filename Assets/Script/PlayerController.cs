using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerController>();
            return instance;
        }
    }

    public enum PlayerState
    {
        IDLE,
        WALKINGFORWARD,
        WALKINGBACKWARD,
        ROTATERIGHT,
        ROTATELEFT,
        RUNNING,
        JUMPING,
        DOUBLEJUMPING,
        ATTACK,
        SPECIALATTACK,
        DEFEND,
    }

    [SerializeField] private float gravityForce;
    [SerializeField] private float jumpingForce;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float runningSpeed;
    [SerializeField] public int myLife;
    [SerializeField] private Vector3 movDirection;

    private Animator anim;
    private CharacterController cc;
    private float rotation;
    private float directionY;
    private bool doubleJumping;
    private bool isHurt;
    private bool isDead;
    private bool isDefend;

    private PlayerState currentState;


    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        SetState(PlayerState.IDLE);
    }

    void Update()
    {
        handleInput();
        movDirection = transform.TransformDirection(movDirection);
        movDirection.y = directionY;
        movDirection *= Time.deltaTime;
        cc.Move(movDirection);
    }

    private void SetState(PlayerState newState)
    {
        if (newState != currentState)
        {
            currentState = newState;
        }
    }
    private void handleInput()
    {

        switch (currentState)
        {
            case PlayerState.IDLE:
                HandleIdleState();
                break;
            case PlayerState.WALKINGFORWARD:
                HandleWalkingForState();
                break;
            //case PlayerState.WALKINGBACKWARD:
            //    HandleWalkingBackState();
            //    break;
            //case PlayerState.ROTATERIGHT:
            //    HandleRotateRightState();
            //    break;
                //case PlayerState.ROTATELEFT:
                //    HandleRotateLeftState();
                //    break;
                //case PlayerState.RUNNING:
                //    HandleRunningState();
                //    break;
                //case PlayerState.JUMPING:
                //    HandleJumpingState();
                //    break;
                //case PlayerState.DOUBLEJUMPING:
                //    HandleDoubleJumpingState();
                //    break;
                //case PlayerState.ATTACK:
                //    HandleAttack1State();
                //    break;
                //case PlayerState.SPECIALATTACK:
                //    HandleAttack2State();
                //    break;
                //case PlayerState.DEFEND:
                //    HandleDefendState();
                //    break;
        }
    }

    private void HandleIdleState()
    {
        anim.SetTrigger("idle");

        if (Input.GetKey(KeyCode.W))
        {
            SetState(PlayerState.WALKINGFORWARD);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            SetState(PlayerState.WALKINGBACKWARD);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            SetState(PlayerState.ROTATERIGHT);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            SetState(PlayerState.ROTATELEFT);
        }
        else if (Input.GetButtonDown("Jump"))
        {
            SetState(PlayerState.JUMPING);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            SetState(PlayerState.ATTACK);
        }
        else if (Input.GetMouseButton(1))
        {
            SetState(PlayerState.DEFEND);
        }
    }
    private void HandleWalkingForState()
    {
        if (Input.GetKey(KeyCode.W))
        {
            movDirection = Vector3.forward * walkingSpeed;
            anim.SetTrigger("walking");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            SetState(PlayerState.WALKINGBACKWARD);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotation += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, rotation, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rotation += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, rotation, 0);
        }
        else if (Input.GetButtonDown("Jump"))
        {
            SetState(PlayerState.JUMPING);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            SetState(PlayerState.ATTACK);
        }
        else if (Input.GetMouseButton(1))
        {
            SetState(PlayerState.DEFEND);
        }
        else
        {
            SetState(PlayerState.IDLE);
        }
    }
    private void HandleWalkingBackState()
    {
        if (Input.GetKey(KeyCode.S))
        {
            movDirection = Vector3.forward * walkingSpeed * -1;
            anim.SetBool("isWalkingBack", true);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            SetState(PlayerState.WALKINGFORWARD);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            SetState(PlayerState.ROTATERIGHT);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rotation += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, rotation, 0);
        }
        else if (Input.GetButtonDown("Jump"))
        {
            SetState(PlayerState.JUMPING);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            SetState(PlayerState.ATTACK);
        }
        else if (Input.GetMouseButton(1))
        {
            SetState(PlayerState.DEFEND);
        }
        else
        {
            anim.SetBool("isWalkingBack", false);
            SetState(PlayerState.IDLE);
        }
    }
    private void HandleRotateRightState()
    {
        if (Input.GetKey(KeyCode.D))
        {

            rotation += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, rotation, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            SetState(PlayerState.WALKINGFORWARD);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            SetState(PlayerState.WALKINGBACKWARD);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            SetState(PlayerState.ROTATELEFT);
        }
        else if (Input.GetButtonDown("Jump"))
        {
            SetState(PlayerState.JUMPING);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            SetState(PlayerState.ATTACK);
        }
        else if (Input.GetMouseButton(1))
        {
            SetState(PlayerState.DEFEND);
        }
        else
        {
            anim.SetBool("isWalkingBack", false);
            SetState(PlayerState.IDLE);
        }

    }
}