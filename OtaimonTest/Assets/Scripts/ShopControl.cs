using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopControl : MonoBehaviour
{    
    public Controls controls;
    public ItemList ItemList;
    public SpriteList currencySprites;

    public Transform itemContainer;
    public GameObject panelPrefab;

    protected List<GameObject> panels;
    protected int selected;
    //corrects bug where first item could be bought on the same key press that entered the store
    protected bool delay;

    
    void Setup()
    {
        panels = new List<GameObject>();
        for (int i = 0; i < Shop.itemIds.Count; i++)
        {
            GameObject temp = Instantiate(panelPrefab,itemContainer);            

            temp.transform.Find("ItemSprite").GetComponent<Image>().sprite = ItemList.items[ItemList.FindItemByID(Shop.itemIds[i])].image;
            temp.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = ItemList.items[ItemList.FindItemByID(Shop.itemIds[i])].name;
            temp.transform.Find("CostCount").GetComponent<TextMeshProUGUI>().text = ItemList.items[ItemList.FindItemByID(Shop.itemIds[i])].costAmount + " x";
            temp.transform.Find("CostColor").GetComponent<Image>().sprite = currencySprites.sprites[(int)ItemList.items[ItemList.FindItemByID(Shop.itemIds[i])].costType];
            panels.Add(temp);
        }        
    }

    void Update()
    {
        if (Shop.ready)
        {
            Setup();
            Shop.ready = false;
        }
        else
        {
            if (Shop.open && !this.gameObject.GetComponent<Canvas>().enabled)
            {
                this.gameObject.GetComponent<Canvas>().enabled = true;
                selected = 0;
                panels[selected].transform.Find("Selector").gameObject.SetActive(true);
                
                for (int i = 1; i < panels.Count; i++)
                {
                    panels[i].transform.Find("Selector").gameObject.SetActive(false);
                }

                StartCoroutine(InputDelay(0.1f));
            }
            else if (!Shop.open && this.gameObject.GetComponent<Canvas>().enabled)
            {
                this.gameObject.GetComponent<Canvas>().enabled = false;
            }

            if (Shop.open)
            {
                if (Input.GetKeyDown(controls.left))
                {
                    panels[selected].transform.Find("Selector").gameObject.SetActive(false);
                    selected--;
                    if (selected < 0)
                    {
                        selected = panels.Count - 1;
                    }
                    panels[selected].transform.Find("Selector").gameObject.SetActive(true);
                }
                else if (Input.GetKeyDown(controls.right))
                {
                    panels[selected].transform.Find("Selector").gameObject.SetActive(false);
                    selected++;
                    if (selected > panels.Count - 1)
                    {
                        selected = 0;
                    }
                    panels[selected].transform.Find("Selector").gameObject.SetActive(true);
                }
                if (Input.GetKeyDown(controls.interact))
                {
                    if (Shop.itemIds[selected] != -1 && !delay)
                    {
                        Item boughtItem = ItemList.items[ItemList.FindItemByID(Shop.itemIds[selected])];
                        if (boughtItem.costAmount <= Inventory.money[(int)boughtItem.costType])
                        {
                            Inventory.money[(int)boughtItem.costType] -= boughtItem.costAmount;
                            Inventory.updated = true;
                            panels[selected].transform.Find("Black").gameObject.SetActive(true);
                            Inventory.items.Add(Shop.itemIds[selected]);
                            Inventory.gotItem = true;
                            Shop.itemIds[selected] = -1;
                        }
                    }
                }
            }
        }
    }

    IEnumerator InputDelay(float time)
    {
        delay = true;
        yield return new WaitForSeconds(time);
        delay = false;
    }
}
