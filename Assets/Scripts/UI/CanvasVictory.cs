using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasVictory : MonoBehaviour
{
    [SerializeField] GameObject[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IEShow(hearts, 1f));
    }

    private IEnumerator IEShow(GameObject[] hearts, float delay)
    {
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < hearts.Length; i++)
        {
            yield return new WaitForSeconds(0.05f);
            hearts[i].SetActive(true);
            SoundManager.Instance.Play(FxType.Coin);
        }

        yield return new WaitForSeconds(0.2f);
        SoundManager.Instance.Play(FxType.Victory);

    }
}
