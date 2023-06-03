using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject victory;

    public void Victory()
    {
        victory.SetActive(true);
    }
}
