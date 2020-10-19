#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsController : MonoBehaviour
{
    [SerializeField] private SpawObj[] targets  ;
    [SerializeField] private Transform prevTarget  ;

    private Vector3 posBuffer;
    private Quaternion rotBuffer;

    public void Start()
    {
        posBuffer = prevTarget.position;
        rotBuffer = prevTarget.rotation;
    }

    public void Update()
    {
        //if there is no bottle so enter and Instantiate another target
        if (prevTarget ==null )
        {
            int i = Random.Range(0, 10);
            Vector3 pos = posBuffer;
            if (targets != null)
            {
                for (int j = 0; j < targets.Length; j++)
                {
                    if (i >= targets[j].minProbRange && i <= targets[j].maxProbRange)
                    {
                        GameObject newTarget = Instantiate(targets[j].spawObj, pos, rotBuffer);
                        newTarget.transform.parent = gameObject.transform;

                        if (j == 1)
                        {
                            newTarget.transform.Rotate(new Vector3(0, 0, -90));
                        }

                        prevTarget = newTarget.GetComponent<Transform>();
                        break;
                    }
                }

            }
        }
    }
}

[System.Serializable]
public class SpawObj
{
    public GameObject spawObj;
    public int minProbRange;
    public int maxProbRange;

}
