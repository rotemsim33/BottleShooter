#pragma warning disable 649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private AudioSource explosionSound;
    [SerializeField] private GameObject exp  ;
    [SerializeField] private float m_expForce=900, m_raduis=10;
    [SerializeField] private float waitForDestroy = 4f;             //Delay before removing objects 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(distroyObject());
    }
    public void triggerBreak()
    {
        GameObject _exp = Instantiate(exp, transform.position, transform.rotation);
        Destroy(_exp, 3);
        KnockBack();
        Destroy(gameObject, 1);

    }

    void KnockBack()
    {
        Collider[] colliders= Physics.OverlapSphere(transform.position, m_raduis);
        foreach(Collider coll in colliders)
        {
            Rigidbody rigg = coll.GetComponent<Rigidbody>();
            if (rigg !=null )
            {
                rigg.AddExplosionForce(m_expForce, transform.position, m_raduis);
            }
        }
    }

    public IEnumerator distroyObject()
    {
        yield return new WaitForSeconds(waitForDestroy);
        GameObject.Destroy(transform.gameObject);

    }

    

}
