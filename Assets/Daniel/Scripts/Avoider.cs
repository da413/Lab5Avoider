using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Avoider : MonoBehaviour
{
    public GameObject Avoidee;
    public float range;
    public float speed;

    Vector3 dirFromAvoidee;
    float magnitudeFromAvoidee;
    float dotProd;

    public Transform point;

    public static List<Transform> hidingPoints = new List<Transform>();

    public PoissonDiscSampler sampler;

    //some way to toggle gizmos 
    private void Awake()
    {
        if (this.gameObject.GetComponent<NavMeshAgent>() == null)
        {
            Debug.LogWarning("this gameobject does not have a nav mesh agent component, please set a nav mesh agent onto this game object");
        }
        else
        {
           // Debug.Log("There is a nav mesh component");
        }

        sampler = new PoissonDiscSampler(3, 4, 0.3f);
       
        StartCoroutine(SpawnPoisson());
    }


    private void Update()
    {
        transform.LookAt(Avoidee.transform.position);
        dirFromAvoidee = (Avoidee.transform.position - transform.position).normalized;
        magnitudeFromAvoidee = (Avoidee.transform.position - transform.position).magnitude;
       
        
        isinRange();
        
        
        //dotProd = Vector3.Dot(dirFromAvoidee, transform.forward);
            
    }

    IEnumerator SpawnPoisson()
    {
        yield return new WaitUntil(isinRange);
        
        foreach(Vector2 sample in sampler.Samples())
            {
                Instantiate(point, new Vector3(sample.x, 0, sample.y), Quaternion.identity);
            }

    }

    bool isinRange()
    {
        if(magnitudeFromAvoidee <= 5)
        {
            return true;
        }else{
            return false;
        }

    }
}


