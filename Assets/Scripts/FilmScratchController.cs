using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilmScratchController : MonoBehaviour
{
    public Image filmScratchImage;
    public float minTime = 0.1f;
    public float maxTime = 0.5f;

    private void Start()
    {
        StartCoroutine(FilmScratchRoutine());
    }

    private IEnumerator FilmScratchRoutine()
    {
        while (true)
        {
            filmScratchImage.enabled = !filmScratchImage.enabled;
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
