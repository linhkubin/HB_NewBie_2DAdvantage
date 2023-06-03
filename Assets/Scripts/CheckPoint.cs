using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    bool isChecked = false;
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isChecked)
        {
            isChecked = true;
            anim.SetTrigger("flag");
            collision.GetComponent<Player>().SavePoint(transform.position);
        }
    }
}
