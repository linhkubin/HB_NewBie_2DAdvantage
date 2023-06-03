using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Snail : MonoBehaviour
{
    [SerializeField] Transform snail;
    
    [SerializeField] Animator anim;

    [SerializeField] Transform left, right, movingTarget;

    private string animName;

    public enum State { Run, Idle, Blink }

    State state = State.Idle;


    // Start is called before the first frame update
    void Start()
    {
        movingTarget = right;
        ChangeFace(true);

        NextState();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Run:

                ChangeAnim("run");
                snail.position = Vector3.MoveTowards(snail.position, movingTarget.position, Time.deltaTime * 0.1f);

                if (Vector3.Distance(snail.position, right.position) < 0.1f)
                {
                    movingTarget = left;
                    ChangeFace(false);
                }
                else
                if (Vector3.Distance(snail.position, left.position) < 0.1f)
                {
                    movingTarget = right;
                    ChangeFace(true);
                }
                break;

            case State.Idle:
                ChangeAnim("idle");
                break;

            case State.Blink:
                ChangeAnim("blink");
                break;

        }
    }


    public void NextState()
    {
        state++;
        if ((int)state > 2)
        {
            state = 0;
        }

        Invoke(nameof(NextState), Random.Range(2f, 6f));
    }

    private void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            anim.ResetTrigger(this.animName);
            this.animName = animName;
            anim.SetTrigger(this.animName);
        }
    }

    private void ChangeFace(bool isRight)
    {
        snail.transform.localRotation = Quaternion.Euler(Vector3.up * (isRight ? 180 : 0));
    }
}
