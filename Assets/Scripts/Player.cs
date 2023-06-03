using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] float moveSpeed; 
    [SerializeField] float forceJump; 
    [SerializeField] Animator anim; 
    [SerializeField] string animName;

    private Vector2 checkPoint;

    private bool isDeath = false;
    private bool isDoubleJump = false;

    [SerializeField] ParticleSystem jump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        checkPoint = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (isDeath)
        {
            return;
        }
         
        //di chuyen
        float direct = Input.GetAxis("Horizontal");
        rb.velocity = direct * moveSpeed * Vector2.right + rb.velocity.y * Vector2.up;

        if (Input.GetKeyDown(KeyCode.Space) && !IsGround() && !isDoubleJump)
        {
            ChangeAnim("doublejump");
            Jump(forceJump * Vector2.up);
            isDoubleJump = true;
        }

        //nhay
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.1f && !isDoubleJump)
        {
            Jump(forceJump * Vector2.up);
            Instantiate(jump, transform.position - Vector3.up * 0.1f, quaternion.identity);
            ChangeAnim("jump");
        }

        //xoay nhan vat
        if (direct == 0)
        {

        }
        else if (direct > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); // = quaternion.identity
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0)); 
        }

        //xu ly anim
        if (Mathf.Abs(rb.velocity.y) > 0.05f && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
        }
        else if (Mathf.Abs(rb.velocity.y) > 0.05f && rb.velocity.y > 0)
        {
        }
        else if(IsGround() && Mathf.Abs(rb.velocity.y) < 0.1f && rb.velocity.y <= 0.1f)
        {
            if (direct == 0)
            {
                ChangeAnim("idle");
            }
            else
            {
                ChangeAnim("run");
            }
        }

        if (Mathf.Abs(rb.velocity.y) < 0.1f && rb.velocity.y <= 0.1f && IsGround())
        {
            isDoubleJump = false;
        }

    }

    public LayerMask layerGround;

    private bool IsGround()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.4f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.4f, layerGround);
        return hit.collider != null;
    }

    public void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            anim.ResetTrigger(this.animName);
            this.animName = animName;
            anim.SetTrigger(this.animName);
        }
    }

    public void SavePoint(Vector3 position)
    {
        checkPoint = position;
    }

    public void LoadPoint()
    {
        isDeath = false;
        transform.position = checkPoint;
        ChangeAnim("idle");
    }

    public void Death()
    {
        rb.velocity = Vector2.zero;
        isDeath = true;
        ChangeAnim("hit");
        Invoke(nameof(LoadPoint), 1.2f);
        SoundManager.Instance.Play(FxType.Crash);
    }

    public void Jump(Vector2 force)
    {
        SoundManager.Instance.Play(FxType.Jump);
        rb.velocity = Vector2.zero;
        rb.AddForce(force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !isDeath)
        {
            collision.gameObject.GetComponent<Enemy>().Death();
            Jump(forceJump * Vector2.up);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            //TODO: show UI
            SoundManager.Instance.Play(FxType.Victory);
            UIManager.Instance.Victory();
            enabled = false;
            rb.velocity = Vector2.zero;
            ChangeAnim("idle");
        }
    }

}
