#pragma warning disable 649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateBottle : MonoBehaviour
{
    [SerializeField] private GameObject bottle;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = bottle.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        bottle.transform.Rotate(0, 2, 0, Space.Self);

    }
}