using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    Rigidbody2D _rgbd2D;
    [SerializeField] float force = 150f;
    public void Shoot()
    {
        _rgbd2D = GetComponent<Rigidbody2D>();
        _rgbd2D.AddForce(-transform.right * force);
    }
}
