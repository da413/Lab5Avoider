using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public GameObject avoidee;
    public GameObject avoider;
    public RaycastHit hitInfo;


    LayerMask layer;

    public float distanceFromAvoider { get; private set; }
    public bool isVisible { get; private set; }
    void Awake()
    {
        layer = LayerMask.GetMask("Default", "Avoidee");
        distanceFromAvoider = (avoider.transform.position - transform.position).magnitude; //used to find the closest point for avoider
    }
    void Update()
    {
        transform.LookAt(avoidee.transform.position); //look at avoidee
        Vector3 distanceFromAvoidee = avoidee.transform.position - transform.position;
        float magnitudeFromAvoidee = distanceFromAvoidee.magnitude; //estimate distance from avoidee

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, layer))
        {
            StartCoroutine(IsVisibleCoroutine(hitInfo.collider));

        }
    }//the point can see the player

    IEnumerator IsVisibleCoroutine(Collider collider)
    {
        if (collider != avoidee.GetComponent<Collider>())
        {

            isVisible = false;
           // Debug.Log(isVisible);
        }
        else
        {
            isVisible = true;
          //  Debug.Log(isVisible);
        }
        yield return null;
    }

}    

    




