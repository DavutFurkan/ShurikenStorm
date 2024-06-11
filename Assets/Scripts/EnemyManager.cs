using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    Animator enemyAnimator;
    Rigidbody2D enemyRB;
    public float health;
    public float damage;
    public float speed = 3f; 
    public Transform target; 

    bool colliderBusy = false;

    public Slider slider;

    private bool facingRight = true;

    
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();

        
        if (enemyRB != null)
        {
            enemyRB.freezeRotation = true;
        }

        if (slider == null)
        {
            Debug.LogError("Slider is not assigned!", this);
        }

        if (target == null)
        {
            Debug.LogError("Target is not assigned!", this);
        }

        slider.maxValue = health;
        slider.value = health;

        GameManager.Instance.RegisterZombie(gameObject);  
    }

    void OnDestroy()
    {
        GameManager.Instance.UnregisterZombie(gameObject); 
    }

    
    void Update()
    {
        if (target == null)
        {
            Debug.LogError("Target is not assigned in Update method!", this);
            return;
        }

        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            
            if ((target.position.x < transform.position.x && facingRight) || (target.position.x > transform.position.x && !facingRight))
            {
                Flip();
            }

            
            enemyAnimator.SetFloat("Speed", Mathf.Abs(direction.x));
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !colliderBusy)
        {
            colliderBusy = true;
            other.GetComponent<PlayerManager>().GetDamage(damage);
        }
        else if (other.tag == "Bullet")
        {
            GetDamage(other.GetComponent<BulletManager>().bulletDamage);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            colliderBusy = false;
        }
    }

    public void GetDamage(float damage)
    {
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

    private void AmIDead()
    {
        if (health <= 0)
        {
            //DataManager.Instance.EnemyKilled++;
            GameManager.Instance.UnregisterZombie(gameObject);
            Destroy(gameObject);
        }
    }
}