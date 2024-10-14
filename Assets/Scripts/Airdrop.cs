using UnityEngine;

public class Airdrop : MonoBehaviour
{
    public float movingSpeed;
    public AudioClip healSound;
    public AudioClip powerSound;

    // Start is called before the first frame update
    void Start()
    {
        // initialize moving speed
        movingSpeed = -10f;
    }

    // Update is called once per frame
    void Update()
    {
        // update position, if out of screen then die
        Vector3 newPos = transform.position + new Vector3(0f, 0f, movingSpeed * Time.deltaTime);
        if (newPos.z < -29)
        {
            Die();
        }
        else
        {
            transform.position = newPos;
        }
    }

    // collide with player spaceship to enpower player
    private void OnCollisionEnter(Collision collision)
    {
        PlayerSpaceship s = collision.gameObject.GetComponent<PlayerSpaceship>();
        if (gameObject.tag == "airdropHeart")
        {
            AudioSource.PlayClipAtPoint(healSound, new Vector3(0f, 2f, 0f));
            s.UpdateLife(-1);
        }
        else if (gameObject.tag == "airdropBullet")
        {
            AudioSource.PlayClipAtPoint(powerSound, new Vector3(0f, 5f, 0f));
            s.BulletUp();
        }
        else {
            AudioSource.PlayClipAtPoint(powerSound, new Vector3(0f, 5f, 0f));
            s.PowerUp();
        }
        Die();
    }

    void Die()
    {

        Destroy(gameObject);
    }
}
