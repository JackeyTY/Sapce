using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spacecraft : MonoBehaviour
{
    public int bulletNumber;
    public float firingSpeed;
    public float bulletSpeed;
    public int bulletDamage;
    public float movingSpeed;
    public int life;
    public int point;
    public float timer;
    public GameObject bullet;
    public AudioClip deathKnell;
    public AudioClip bulletSound;
    private bool falling;
    private Vector3 rotationAxis;
    public GameObject explosionEffect;
    Global global;

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(0f, firingSpeed);
        falling = false;
        global = GameObject.Find("GlobalObject").GetComponent<Global>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!falling) {
            timer += Time.deltaTime;
            if (timer >= firingSpeed)
            {
                timer = 0;
                Vector3 spawnPos = transform.position + new Vector3(0f, 0f, -2f);
                GameObject obj = Instantiate(bullet, spawnPos, Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
                SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName("GameplayScene"));
                Bullet b = obj.GetComponent<Bullet>();
                b.movingSpeed = bulletSpeed;
                b.damage = bulletDamage;
                AudioSource.PlayClipAtPoint(bulletSound, new Vector3(0f, 10f, 0f));
            }
        }
    }

    private void FixedUpdate()
    {
        if (falling)
        {
            transform.Rotate(rotationAxis, 180f * Time.deltaTime);
            transform.position -= new Vector3(0f, 15f * Time.deltaTime, 0f);
            if (transform.position.y < -30) 
            {
                GameObject obj = Instantiate(explosionEffect, transform.position, Quaternion.identity) as GameObject;
                SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName("GameplayScene"));
                AudioSource.PlayClipAtPoint(deathKnell, new Vector3(0f, 10f, 0f));
                global.destroyExplosionHelper(obj);
                Die();
            }
        }
        else 
        {
            Vector3 newPos = transform.position + new Vector3(0f, 0f, -movingSpeed * Time.deltaTime);
            if (newPos.z < -30)
            {
                Die();
            }
            else
            {
                newPos.y = 0f;
                transform.position = newPos;
            }
        }
    }

    public void UpdateLife(int change)
    {
        life -= change;
        if (life <= 0)
        {
            falling = true;
            rotationAxis = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            if (change != 99) {
                global.updateScore(point);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            PlayerSpaceship s = collision.gameObject.GetComponent<PlayerSpaceship>();
            s.UpdateLife(1);
            AudioSource.PlayClipAtPoint(deathKnell, new Vector3(0f, 10f, 0f));
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
