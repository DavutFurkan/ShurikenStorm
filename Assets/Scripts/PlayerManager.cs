using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float health;
    public float bulletSpeed;
    public Animator attackAnimator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public GameObject restartMenu;
    public LayerMask enemyLayers;
    public int attackDamage = 10;
    private bool dead = false;
    public Transform bullet, floatingText;
    private Transform muzzle;
    public Slider slider;
    

    void Start()
    {
        muzzle = transform.GetChild(2);
        slider.maxValue = health;
        slider.value = health;
        restartMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Attack();
        }

        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }
    }

    void Attack()
    {
        attackAnimator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyManager>().GetDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void GetDamage(float damage)
    {
        Instantiate(floatingText, transform.position, Quaternion.identity).GetComponent<TextMesh>().text = damage.ToString();
        if ((health - damage) >= 0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }
        slider.value = health;
        AmIDead();
    }

    void AmIDead()
    {
        if (health <= 0 && !dead)
        {
            dead = true;
            ShowRestartMenu();
        }
    }

    void ShowRestartMenu()
    {
        Time.timeScale = 0;
        restartMenu.SetActive(true);
    }

    void ShootBullet()
    {
        Transform tempBullet;
        tempBullet = Instantiate(bullet, muzzle.position, Quaternion.identity);
        tempBullet.GetComponent<Rigidbody2D>().AddForce(muzzle.forward * bulletSpeed);
        
    }
}