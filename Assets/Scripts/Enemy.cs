using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public enum State { Idle, Patrol, Attack, Death}

public class Enemy : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D collider;
    [SerializeField] Animator anim;

    private string animName;

    State state;
    float time;

    Player target;

    bool isRight;

    private void Start()
    {
        state = State.Idle;
        time = Random.Range(1f, 4f);
        ChangeDirect(true);
    }

    void Update()
    {
        UpdateState(this.state);
    }

    private void ChangeState(State state) 
    {
        this.state = state;

        switch (state)
        {
            case State.Idle:
                time = Random.Range(1f, 8f);
                ChangeAnim("idle");
                break;
            case State.Patrol:
                time = Random.Range(1f, 4f);
                ChangeAnim("run");
                break;
            case State.Attack:
                break;
            case State.Death:
                break;
            default:
                break;
        }
    }

    public void UpdateState(State state)
    {
        time -= Time.deltaTime;

        switch (state)
        {
            case State.Idle:
                if (time <= 0)
                {
                    ChangeState(State.Patrol);
                }
                break;

            case State.Patrol:
                if (time <= 0)
                {
                    ChangeState(State.Idle);
                }
                rb.velocity = isRight ? Vector2.right : Vector2.left ;
                break;

            case State.Attack:
                if (target != null)
                {
                    ChangeDirect(target.transform.position.x > transform.position.x);
                }
                else
                {
                    ChangeState(State.Patrol);
                }
                rb.velocity = isRight ? Vector2.right : Vector2.left ;
                break;

            case State.Death:
                break;
            default:
                break;
        }
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

    public void ChangeDirect(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.identity : Quaternion.Euler(0,180,0);
        target = null;
    }

    public void Death()
    {
        state = State.Death;
        ChangeAnim("hit");
        collider.isTrigger = true;
        rb.velocity = Vector2.zero;
        Vector2 force = Random.insideUnitCircle;
        force.y = Mathf.Abs(force.y) * 1.2f;
        rb.AddForce(force * 200);
    }

    public void SetTarget(Player target)
    {
        this.target = target;
        state = target != null ? State.Attack : State.Patrol;
    }
}
