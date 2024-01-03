using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cannon : MonoBehaviour
{
    [Header("CannonMove")]
    [SerializeField] private float maxMoveSpeed;
    private Rigidbody2D rigid;
    Cannon_Action Cannon_Action_Map;
    InputAction moveInput;
    private float h;

    [Header("Bullet")]
    [SerializeField] private float BulletSpeed;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Cannon_Action_Map = new Cannon_Action();
        moveInput = Cannon_Action_Map.Cannon.Move;
        moveInput.started += OnMoveStarted;
        moveInput.performed += OnMovePerfomed;
        moveInput.canceled += OnMoveCanceled;
    }

    public void OnMoveStarted(InputAction.CallbackContext context)
    {

    }
    public void OnMovePerfomed(InputAction.CallbackContext context)
    {
        Vector2 moveInputVector = context.ReadValue<Vector2>();
        h = moveInputVector.x;
    }

    public void OnMoveCanceled(InputAction.CallbackContext context)
    {
        h = 0;
    }

    private void Start()
    {
        moveInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.AddForce(new Vector2(h * maxMoveSpeed, 0));
    }

    // public void OnMove(InputValue value)
    // {
    //     rigid.AddForce(new Vector2(h * maxMoveSpeed, 0));
    // }

    public void OnShoot()
    {
        var bullet = CannonShoot.GetObject();
        bullet.Shoot(Vector3.up, BulletSpeed);
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(rigid.velocity.x) > maxMoveSpeed && h != 0)
            rigid.velocity = new Vector2(Mathf.Sign(h) * maxMoveSpeed, rigid.velocity.y);
    }
}
