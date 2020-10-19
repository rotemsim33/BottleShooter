#pragma warning disable 649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    [SerializeField] private GameContoller gameContoller  ;
    [SerializeField] private Camera mainCamera  ;
    [SerializeField] private GameObject bloodCanvas  ;

    [Header ("Audio")]
    [SerializeField] private AudioSource glassBrokeSound  ;
    [SerializeField] private AudioSource explosionSound  ;


    [Header("Gun Details")]
    [SerializeField] private ParticleSystem shootEfect  ;
    [SerializeField] private AudioSource shootGunSound  ;
    [SerializeField] private Animator transition  ;
    [SerializeField] private float range = 100f;


    //Updated from other script
    public bool gameStart;
    

    void Update()
    {
        if (Input.anyKeyDown && gameStart)
        {
            transition.SetTrigger("Shot1");
            StartCoroutine(Shoot());
        }
    }
    
    IEnumerator Shoot()
    {
        
        shootEfect.Play();
        shootGunSound.Play();

        RaycastHit hit;
        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range)){
            BreakableObject breakableObject = hit.transform.GetComponent<BreakableObject>();
            Bomb bombObj = hit.transform.GetComponent<Bomb>();

            if (breakableObject !=null )
            {
                glassBrokeSound.Play();
                breakableObject.triggerBreak();
                gameContoller.AddScore();
            }
            else if(bombObj != null)
            {

                explosionSound.Play();
                bombObj.triggerBreak();
                bloodCanvas.SetActive(true);
                yield return new WaitForSeconds(1);
                gameContoller.EndLevel(true);

            }
        }
    }
}
