using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float Speed = 250f;
    public float JumpForce = 12f;
    private Animator _animator;
    private Rigidbody2D _rigid { get; set; }
    private BoxCollider2D _box { get; set; }

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;

        Vector2 corner1 = new Vector2(_box.bounds.max.x, _box.bounds.min.y - 0.1f);
        Vector2 corner2 = new Vector2(_box.bounds.min.x, _box.bounds.min.y - 0.2f);

        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = false;
        MovingPlatform movingPlatform = null;
        Vector3 pScale = Vector3.one;

        if (hit != null)
        {
            grounded = true;
            movingPlatform = hit.GetComponent<MovingPlatform>();
        }
        if (movingPlatform != null)
        {
            pScale = movingPlatform.transform.localScale;
            transform.parent = movingPlatform.transform;
        }
        else
        {
            transform.parent = null;
        }

        _rigid.gravityScale = grounded && Mathf.Approximately(deltaX, 0) ? 0 : 1;

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            _rigid.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }



        _animator.SetFloat("Speed", Mathf.Abs(deltaX));

        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / pScale.x, 1 / pScale.y, 1);
        }
        Vector2 movement = new Vector2(deltaX, _rigid.velocity.y);


        _rigid.velocity = movement;
    }
}
