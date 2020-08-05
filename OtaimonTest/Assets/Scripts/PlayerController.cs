using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Controls controls;
    public float speed;

    // 0 == none, 1 == left, 2 == right
    protected short direction;
    protected Rigidbody2D mybody;

    // Touching npc
    protected bool npc;
    // Coins being touched currently
    public List<GameObject> currentCoins;
    void Start()
    {
        mybody = GetComponent<Rigidbody2D>();
        direction = 0;
        Inventory.money = new int[(CurrencyColor.GetValues(typeof(CurrencyColor)).Length)];
        Inventory.items = new List<short>();
    }

    void Update()
    {
        
        if (Input.GetKey(controls.left))
        {
            direction = 1;
        }
        else if (Input.GetKey(controls.right))
        {
            direction = 2;
        }
        else
        {
            direction = 0;
        }

        if (!Shop.open)
        {
            if (Input.GetKeyDown(controls.interact))
            {
                if (currentCoins.Count > 0)
                {
                    for (int i = 0; i < currentCoins.Count; i++)
                    {
                        Inventory.money[(int)currentCoins[i].GetComponent<CurrencyController>().myColor]++;
                        currentCoins[i].GetComponent<CurrencyController>().SetFollow(this.transform);
                    }
                    currentCoins.Clear();
                    Inventory.updated = true;
                }
                else if (npc)
                {
                    Shop.open = true;
                }
            }
        }
        

        if(Input.GetKeyDown(controls.close))
        {
            if(Shop.open)
            {
                Shop.open = false;
            }
        }

        if (Input.GetKeyDown(controls.restart))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void FixedUpdate()
    {
        if (Shop.open)
        {
            mybody.velocity = Vector2.zero;
        }
        else
        {
            switch (direction)
            {
                case 1:
                    mybody.velocity = Vector2.left * speed * Time.deltaTime;
                    break;
                case 2:
                    mybody.velocity = Vector2.right * speed * Time.deltaTime;
                    break;
                default:
                    mybody.velocity = Vector2.zero;
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Money"))
        {
            currentCoins.Add(collision.gameObject);
        }

        if (collision.CompareTag("Shop"))
        {
            npc = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Money"))
        {
            currentCoins.Remove(collision.gameObject);
        }

        if (collision.CompareTag("Shop"))
        {
            npc = false;
        }
    }




}
