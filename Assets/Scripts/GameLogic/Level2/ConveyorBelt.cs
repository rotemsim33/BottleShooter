#pragma warning disable 649

using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private GameObject belt  ;
    [SerializeField] private Transform endpoint  ;
    [SerializeField] private float objectSpeed=0;
    [SerializeField] private Vector2 scrollSpeed= new Vector2(0,0);
    private Renderer myRenderer;

    void Start()
    {
        if (endpoint == null )
        {
            endpoint = belt.transform;
        }
        myRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Vector2 offset = Time.time * scrollSpeed;
        myRenderer.material.SetTextureOffset("_MainTex", offset);
    }
    
    private void OnTriggerStay(Collider other)
    { 
        //move the target on the  ConveyorBelt
        other.transform.position = Vector3.MoveTowards(other.transform.position, endpoint.position, objectSpeed * Time.deltaTime);
    }
}
