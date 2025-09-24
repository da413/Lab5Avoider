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
    float dotProd;

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

        
        
    }


    private void Update()
    {
        transform.LookAt(Avoidee.transform.position);

        /*
        dirFromAvoidee = (Avoidee.transform.position - transform.position).normalized;
        dotProd = Vector3.Dot(dirFromAvoidee, transform.forward);
       */
        
    }




}
