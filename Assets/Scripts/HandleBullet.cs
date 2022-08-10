using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Bullet"))
        {
        
            Destroy(collision.gameObject); 
        }
    }


}
