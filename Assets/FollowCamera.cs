using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -7);

    void Start()
    {
        if (target == null)
        {
            GameObject Player = GameObject.FindWithTag("Player");
            if (Player != null)
                target = Player.transform;
        }
    }

    void Update()
    {
        
    }
    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }
}
