using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmemyController : MonoBehaviour
{
    [SerializeField]
    private Transform FirePos;
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private float BulletSpeed;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float fireThreashHold;
    public Image HealthBar;
    float Health = 1;
    float timespend;
    float Movespeed;
    List<GameObject> catchedBullets = new List<GameObject>();
    private GameObject RefrencedBullet;
    // Update is called once per frame

   
    void FixedUpdate()
    {
        transform.LookAt(player, Vector3.left);

        CheckPlayerHit();
    }

    private void Shoot()
    {
        if (timespend >= fireThreashHold)
        {
            timespend =  0;
            if (catchedBullets.Count > 1)
                {
                    RefrencedBullet = catchedBullets[0];
                    RefrencedBullet.SetActive(true);
                    catchedBullets.Remove(RefrencedBullet);
                }
                else
                {
                    RefrencedBullet = Bullet;
                }
                GameObject bullet = Instantiate(RefrencedBullet, FirePos.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().AddForce(FirePos.transform.forward * BulletSpeed, ForceMode.Impulse);
            
        }
        else
        {
            timespend++;
        }
    }

    private void Update()
    {

        MoveToPlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Bullet"))
        {
            collision.gameObject.GetComponent<Bullet>().CancelInvoke();
            collision.gameObject.SetActive(false);
            catchedBullets.Add(collision.gameObject);
            Demage(.2f);
        }
    }
    private void CheckPlayerHit()
    {

        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(FirePos.transform.position, FirePos.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Shoot();
        }

    }

    private void MoveToPlayer()
    {
        if (!(Mathf.Abs(transform.position.x - player.position.x) < .2f))
        {
            if (transform.position.x - player.position.x < 0)
            {
                Movespeed = 3f;
            }
            else
            {
                Movespeed = -3f;
            }
            transform.position = new Vector3(transform.position.x + Movespeed * Time.deltaTime, transform.position.y, transform.position.z);
        }

    }

    void Demage(float hitPoint)
    {
        if (Health > 0)
        {
            Health -= hitPoint;
            HealthBar.fillAmount = Health;
        }
        else
        {
            Destroy(gameObject);
            UIController.Instance.SetGameOverText("You Won");
        }
    }

}
