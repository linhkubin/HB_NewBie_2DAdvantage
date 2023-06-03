using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpStage : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float force;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().Jump(force * Vector2.up);
            anim.SetTrigger("active");
        }
    }
}
