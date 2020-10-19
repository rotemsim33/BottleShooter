#pragma warning disable 649

using UnityEngine;
using System.Collections;


public class BreakableObject : MonoBehaviour {

    [SerializeField] public Transform fragments;                     //Place the fractured object
    [SerializeField] public float waitForRemoveCollider = 1.0f;      //Delay before removing collider (negative/zero = never)
    [SerializeField] public float waitForRemoveRigid = 10.0f;        //Delay before removing rigidbody (negative/zero = never)
    [SerializeField] public float waitForDestroy = 2.0f;             //Delay before removing objects (negative/zero = never)
    [SerializeField] public float explosiveForce = 350.0f;           //How much random force applied to each fragment
    [SerializeField] public float durability = 2f;                 //How much physical force the object can handle before it breaks
    [SerializeField] public ParticleSystem breakParticles;           //Assign particle system to apear when object breaks
	private Transform fragmentd;                            //Stores the fragmented object after break
	private bool broken;                                    //Determines if the object has been broken or not 
    private Transform frags;

	public void OnCollisionEnter(Collision collision) {
		if (collision.relativeVelocity.magnitude > durability) {
			triggerBreak();
        }
	}

	public void triggerBreak() {

        Destroy(transform.Find("object").gameObject);
		Destroy(transform.GetComponent<Collider>());
		Destroy(transform.GetComponent<Rigidbody>());
		StartCoroutine(breakObject());
	}

	// breaks object
	public IEnumerator breakObject() {
		if (!broken) {
			if (this.GetComponent<AudioSource>() != null) {
				GetComponent<AudioSource>().Play();
			}
			broken = true;

			// adds fragments to stage (!memo:consider adding as disabled on start for improved performance > mem)
			fragmentd = (Transform)Instantiate(fragments, transform.position, transform.rotation);
			// set size of fragments
			fragmentd.localScale = transform.localScale;
			frags = fragmentd.Find("fragments");
			foreach (Transform child in frags) {
				Rigidbody cr = child.GetComponent<Rigidbody>();
				cr.AddForce(Random.Range(-explosiveForce, explosiveForce), Random.Range(-explosiveForce, explosiveForce), Random.Range(-explosiveForce, explosiveForce));
				cr.AddTorque(Random.Range(-explosiveForce, explosiveForce), Random.Range(-explosiveForce, explosiveForce), Random.Range(-explosiveForce, explosiveForce));
			}
			StartCoroutine(removeColliders());
			StartCoroutine(removeRigids());
			// destroys fragments after "waitForDestroy" delay
			if (waitForDestroy > 0) {
				foreach (Transform child in transform) {
					child.gameObject.SetActive(false);
				}
				yield return new WaitForSeconds(waitForDestroy);
				GameObject.Destroy(fragmentd.gameObject);
				GameObject.Destroy(transform.gameObject);
				// destroys gameobject
			} else if (waitForDestroy <= 0) {
				foreach (Transform child in transform) {
					child.gameObject.SetActive(false);
				}
				yield return new WaitForSeconds(1.0f);
				GameObject.Destroy(transform.gameObject);
			}
		}
	}

	// removes rigidbodies from fragments after "waitForRemoveRigid" delay
	public IEnumerator removeRigids() {
		if (waitForRemoveRigid > 0 && waitForRemoveRigid != waitForDestroy) {
			yield return new WaitForSeconds(waitForRemoveRigid);
			foreach (Transform child in frags) {
				child.GetComponent<Rigidbody>().isKinematic = true;
			}
		}
	}

	// removes colliders from fragments "waitForRemoveCollider" delay
	public IEnumerator removeColliders() {
		if (waitForRemoveCollider > 0) {
			yield return new WaitForSeconds(waitForRemoveCollider);
			foreach (Transform child in frags) {
				child.GetComponent<Collider>().enabled = false;
			}
		}
	}
}