using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Vector3 offset;
    private Transform parent;

    void Start() {
        parent = transform.parent;
        offset = new Vector3(0.8f, 0.0f, 0.0f);
    }

    void Update()
    {
        transform.position = parent.position + offset;
    }
}
