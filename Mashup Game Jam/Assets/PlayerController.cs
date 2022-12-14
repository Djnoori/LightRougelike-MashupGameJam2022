using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public int money = 0;
    public int health;
    public int maxHealth;
    public float speed;

    public bool flipped;

    private bool active = true;

    private Rigidbody2D rb;

    [SerializeField] private GameObject shopUI;
    [SerializeField] private TextMeshProUGUI shopMoneyText;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    private GameManager manager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<GameManager>();

        // Things needed to be done on initialization (not variable caching)
        UpdateHealth();
    }

    void Update()
    {
        Move();
        if (health <= 0)
        {
            active = false;
        }
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;
        UpdateHealth();
    }

    void OnCollisionStay2D(Collision2D other)
    {
        // If colliding with the shop, open the shop UI
        if (other.gameObject.tag == "Shop" && Input.GetKey(KeyCode.Space))
        {
            shopMoneyText.text = $"You have {money} diamonds";
            shopUI.SetActive(true);
            active = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // If colliding with a portal, go to the room assigned to the portal
        if (other.tag == "Portal" && Input.GetKey(KeyCode.Space))
        {
            manager.ToRoom(other.GetComponent<Portal>().portalNum);
        }
    }

    // Handles movement
    void Move()
    {
        float hMove = Input.GetAxisRaw("Horizontal");
        float vMove = Input.GetAxisRaw("Vertical");

        if (active == true)
        {
            rb.velocity = new Vector2(hMove * speed, vMove * speed);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    void Look()
    {
        if (Input.mousePosition.x > transform.position.x)
        {
            flipped = false;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            flipped = true;
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    // Closes the shop ui (called from the close button)
    public void CloseShop()
    {
        shopUI.SetActive(false);
        active = true;
    }

    public void UpdateHealth()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
        healthText.text = $"{health} / {maxHealth}";
    }
}
