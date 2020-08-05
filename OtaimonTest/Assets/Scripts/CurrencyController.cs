using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrencyColor { orange, yellow, blue, green, white }

public class CurrencyController : MonoBehaviour
{
    public SpriteList currencySprites;
    public CurrencyColor myColor;

    protected bool got;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = currencySprites.sprites[(int)myColor];
        StartCoroutine(SpawnPulse(7f, 1.5f));
        StartCoroutine(SelfDestruct(5));
    }

    public void SetFollow(Transform t)
    {
        got = true;
        StartCoroutine(Follow(t,0.5f));
    }

    IEnumerator SpawnPulse(float speed,float expansion)
    {
        float originalS = this.transform.localScale.x;
        
        while(this.transform.localScale.x < originalS + expansion)
        {
            this.transform.localScale += Vector3.one * Time.deltaTime * speed;
            if (got)
            {
                break;
            }
            yield return null;
        }

        if (!got)
        {
            while (this.transform.localScale.x > originalS)
            {
                this.transform.localScale -= Vector3.one * Time.deltaTime * speed;
                yield return null;
            }
        }

        this.transform.localScale = Vector3.one * originalS;        
    }

    IEnumerator Follow(Transform t, float time)
    {
        this.transform.parent = t;

        Vector2 originalT = this.transform.localPosition;
        Vector2 originalS = this.transform.localScale;
        for (float i = 0; i < time; i+= Time.deltaTime)
        {
            this.transform.localPosition = Vector3.Lerp(originalT, Vector3.zero, i / time);
            this.transform.localScale = Vector3.Lerp(originalS, Vector3.zero, i / time);
            yield return null;
        }
        Destroy(this.gameObject);
    }

    IEnumerator SelfDestruct(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (!got)
        {
            Destroy(this.gameObject);
        }
    }

}
