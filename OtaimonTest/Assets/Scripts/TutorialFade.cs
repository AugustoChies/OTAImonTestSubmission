using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialFade : MonoBehaviour
{
    public Transform panel;
    public TextMeshProUGUI text;
    void Start()
    {
        StartCoroutine(Fade(3));
    }

    IEnumerator Fade(float delay)
    {
        yield return new WaitForSeconds(delay);

        for (float i = 0; i < 2; i+=Time.deltaTime)
        {
            panel.Translate(Vector2.up * 300 * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
