using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectVFX : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroyThis), 1f);
    }

    public void DestroyThis() 
    {
        Destroy(gameObject);
    }

}
