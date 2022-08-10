using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Invoke("DestroySelf" ,1f);
    }

    private void OnEnable()
    {
        Invoke("DestroySelf", 1f);
    }
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
