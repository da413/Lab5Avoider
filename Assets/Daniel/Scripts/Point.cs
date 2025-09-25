using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point: MonoBehaviour
{
    [SerializeField] GameObject Avoidee;
    LayerMask Layer;
    
    
    RaycastHit hitInfo;

    void Start()
    {
        Layer = LayerMask.GetMask("Default", "Avoidee");
    }

    void Update()
    {
        transform.LookAt(Avoidee.transform.position); 
        float mag = (Avoidee.transform.position - transform.position).magnitude;

        bool hit = Physics.Raycast(transform.position, transform.forward, out hitInfo, mag, Layer);

        if(hit)
        {
            //Debug.Log(hitInfo.collider.gameObject.name);
            if(hitInfo.collider.gameObject.name != "Avoidee") //this point can't see avoidee
            {
                Avoider.hidingPoints.Add(this.transform);
                Debug.Log(Avoider.hidingPoints.Count);
            }
        }
    }

   
}
