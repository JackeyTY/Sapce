using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerSpaceship : MonoBehaviour
{
    public int life;
    public float firingSpeed;
    public int bulletNumber;
    public float bulletSpeed;
    public int bulletDamage;
    public float timer;
    public GameObject bullet;
    public GameObject heart;
    private List<GameObject> hearts;
    public AudioClip bulletSound;
    private Vector3 refPos;
    bool onTouch;

    // Start is called before the first frame update
    void Start()
    {
        life = 4;
        bulletNumber = 1;
        firingSpeed = 0.5f;
        bulletSpeed = 50.0f;
        bulletDamage = 1;
        timer = 0;
        InstantiateHearts();
        refPos = new Vector3(0, 0, 0);
        onTouch = false;
    }

    public void UpdateLife(int change) 
    {
        if (life <= 0)
        {
            return;
        }

        life -= change;

        if (change > 0)
        {
            DestroyHeart(change);
        }
        else {
            AddHeart(-change);
        }
        
        if (life <= 0) 
        {
            Die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= firingSpeed) {
            timer = 0;
            if (bulletNumber == 1)
            {
                Vector3 spawnPos = transform.position + new Vector3(0f, 0f, 3f);
                GameObject obj = Instantiate(bullet, spawnPos, Quaternion.Euler(90f, 0f, 0f)) as GameObject;
                SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName("GameplayScene"));
                Bullet b = obj.GetComponent<Bullet>();
                b.movingSpeed = bulletSpeed;
                b.damage = bulletDamage;
            }
            else 
            {
                Vector3 spawnPos = new Vector3(-1.2f, 0f, 3f) + transform.position;
                Vector3 gap = new Vector3(2.4f / (bulletNumber - 1), 0f, 0f);
                for (int i = 0; i < bulletNumber; i++) {
                    GameObject obj = Instantiate(bullet, spawnPos, Quaternion.Euler(90f, 0f, 0f)) as GameObject;
                    SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName("GameplayScene"));
                    Bullet b = obj.GetComponent<Bullet>();
                    b.movingSpeed = bulletSpeed;
                    b.damage = bulletDamage;
                    spawnPos += gap;
                }
            }
            AudioSource.PlayClipAtPoint(bulletSound, new Vector3(0f, 10f, 0f));
        }
    }

    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.transform.position.y));
            touchPosition.y = 0f;
            if (onTouch == false)
            {
                onTouch = true;
            }
            else 
            { 
                Vector3 newPos = transform.position + touchPosition - refPos;
                newPos.x = Mathf.Clamp(newPos.x, -11.5f, 11.5f);
                newPos.z = Mathf.Clamp(newPos.z, -25f, 25f);
                transform.position = newPos;
            }
            refPos = touchPosition;
        }
        else {
            onTouch = false;
        }
        Vector3 pos = transform.position;
        pos.y = 0;
        transform.position = pos;
        transform.rotation = Quaternion.identity;
    }

    void Die() {
        GameObject.Find("GlobalObject").GetComponent<Global>().gameover();
    }

    void InstantiateHearts()
    {
        hearts = new List<GameObject>();
        Vector3 pos = new Vector3(-12f, 0f, 25f);
        for (int i = 0; i < life; i++)
        {
            GameObject obj = Instantiate(heart, pos, Quaternion.Euler(90f, 0f, 0f)) as GameObject;
            SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName("GameplayScene"));
            hearts.Add(obj);
            pos += new Vector3(0f, 0f, -2f);
        }
    }

    void DestroyHeart(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject obj = hearts[hearts.Count - 1];
            Destroy(obj);
            hearts.RemoveAt(hearts.Count - 1);
        }
    }

    void AddHeart(int num) {
        Vector3 pos = hearts[hearts.Count - 1].transform.position + new Vector3(0f, 0f, -2f);
        for (int i = 0; i < num; i++)
        {
            GameObject obj = Instantiate(heart, pos, Quaternion.Euler(90f, 0f, 0f)) as GameObject;
            SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName("GameplayScene"));
            hearts.Add(obj);
            pos += new Vector3(0f, 0f, -2f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Vector3 pos = transform.position;
        pos.y = 0;
        transform.position = pos;
        transform.rotation = Quaternion.identity;
    }

    public void BulletUp()
    {
        if (bulletNumber == bulletDamage)
        {
            bulletNumber++;
        }
        else {
            bulletDamage++;
        }
    }

    public void PowerUp() {
        firingSpeed *= 0.8f ;
    }
}
