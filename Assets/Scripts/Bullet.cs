using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float movingSpeed;
    public int damage;
    public AudioClip strikeSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // update position, if out of screen then die
        Vector3 newPos = transform.position + new Vector3(0f, 0f, movingSpeed * Time.deltaTime);
        if (newPos.z > 29 || newPos.z < -29) {
            Die();
        } else { 
            transform.position = newPos;
        }
    }
    
    // collide with player spaceship / enemy spacecrafts, deal damage
    private void OnCollisionEnter(Collision collision)
    {
        AudioSource.PlayClipAtPoint(strikeSound, new Vector3(0f, 10f, 0f));
        if (collision.gameObject.CompareTag("spacecraft"))
        {
            Spacecraft s = collision.gameObject.GetComponent<Spacecraft>();
            s.UpdateLife(damage);
            Die();
        }

        if (collision.gameObject.CompareTag("player"))
        {
            PlayerSpaceship s = collision.gameObject.GetComponent<PlayerSpaceship>();
            s.UpdateLife(damage);
            Die();
        }
    }

    void Die() 
    {
        
        Destroy(gameObject);
    }
}
