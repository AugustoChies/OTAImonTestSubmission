using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySpawner : MonoBehaviour
{
    public ItemList ItemList;
    public float minTime, maxTime;
    public GameObject currencyPrefab;
    public float leftLimit, rightLimit;

    protected GameObject temp;

    private void Awake()
    {
        Shop.Setup(ItemList);
    }

    private void Start()
    {        
        float time = Random.Range(minTime, maxTime);
        StartCoroutine(SpawnMoney(time));
    }

    IEnumerator SpawnMoney(float time)
    {
        yield return new WaitForSeconds(time);
        while (Shop.open) { yield return null; }
        if (!Shop.CheckEmpty())
        {
            float spawnx = Random.Range(leftLimit, rightLimit);
            temp = Instantiate(currencyPrefab, new Vector2(spawnx, this.transform.position.y), Quaternion.identity);
            temp.GetComponent<CurrencyController>().myColor = (CurrencyColor)Random.Range(0, (CurrencyColor.GetValues(typeof(CurrencyColor)).Length));
            StartCoroutine(SpawnMoney(Random.Range(minTime, maxTime)));
        }
    }

}
