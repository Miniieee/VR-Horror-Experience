using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMover : MonoBehaviour
{
    private new Rigidbody rigidbody = null;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void MoveTo(Vector3 newPosition)
    {
        rigidbody.MovePosition(newPosition);
    }
}
