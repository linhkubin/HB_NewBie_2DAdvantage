using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public CollectVFX collectVFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SoundManager.Instance.Play(FxType.Coin);
            Instantiate(collectVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
