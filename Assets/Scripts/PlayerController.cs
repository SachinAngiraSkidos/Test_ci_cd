using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform FirePos;
    [SerializeField]
    private GameObject Bullet , gameOverUI;
    [SerializeField]
    private float BulletSpeed;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    List<GameObject> catchedBullets = new List<GameObject>();
    private GameObject RefrencedBullet;
    public Transform cameraHolder;
    public float mouseSensitivity = 2f;
    public float upLimit = -50;
    public float downLimit = 50;

    [SerializeField]
    private Image HealthBar;
    float Health = 1;
    private float gravity = 9.87f;
    private float verticalSpeed = 0;


    public CharacterController characterController;
    public float speed = 3;

    private void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        Move();
        Rotate();
    }

    private void Shoot()
    {
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
       GameObject bullet =  Instantiate(RefrencedBullet, FirePos.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(FirePos.transform.forward * BulletSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Bullet"))
        {
            collision.gameObject.GetComponent<Bullet>().CancelInvoke();
            collision.gameObject.SetActive(false);
          catchedBullets.Add(collision.gameObject);
            Demage(.1f);
        }
    }

        public void Rotate()
    {
        float horizontalRotation = Input.GetAxis("Mouse X");
        float verticalRotation = Input.GetAxis("Mouse Y");
        
        transform.Rotate(0, horizontalRotation * mouseSensitivity, 0);
        cameraHolder.Rotate(-verticalRotation*mouseSensitivity,0,0);

        Vector3 currentRotation = cameraHolder.localEulerAngles;
        if (currentRotation.x > 180) currentRotation.x -= 360;
        currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        cameraHolder.localRotation = Quaternion.Euler(currentRotation);
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (characterController.isGrounded) verticalSpeed = 0;
        else verticalSpeed -= gravity * Time.deltaTime;
        Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);

        Vector3 move = transform.forward * verticalMove + transform.right * horizontalMove;
        characterController.Move(speed * Time.deltaTime * move + gravityMove * Time.deltaTime);

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
            UIController.Instance.SetGameOverText("Game Over");
        }
    }
}
