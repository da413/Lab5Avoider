using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Avoider : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject Avoidee;
    public float range;
    public float speed;

    Vector3 dirFromAvoidee;
    float magnitudeFromAvoidee;
    float dotProd;

    public Point point;
    private Point pointInst;
    private Point closestPoint;
    private float distanceFromPoint;

    public static List<Point> PointsList = new List<Point>();


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
            agent = this.gameObject.GetComponent<NavMeshAgent>();
            agent.speed = speed;
        }

        sampler = new PoissonDiscSampler(5, 5, 0.3f);



        foreach (Vector2 sample in sampler.Samples())
        {
            pointInst = Instantiate(point, new Vector3(sample.x, 0, sample.y), Quaternion.identity);
            PointsList.Add(pointInst);

        }

        //distanceFromPoint = (transform.position - pointInst.transform.position).magnitude;


    }


    private void Update()
    {
        transform.LookAt(Avoidee.transform.position);
        dirFromAvoidee = (Avoidee.transform.position - transform.position).normalized;
        magnitudeFromAvoidee = (Avoidee.transform.position - transform.position).magnitude;


        dotProd = Vector3.Dot(dirFromAvoidee, transform.forward);

        if (dotProd > 0.9) //the avoider can see the avoidee
        {

            for (int i = 0; i < PointsList.Count; i++) //search through point list
            {

                if (!PointsList[i].isVisible) //if a point gameobject not visible to the avoidee
                {

                    float closestDistance = 1000;
                    if (PointsList[i].distanceFromAvoider < closestDistance)
                    {
                        closestDistance = PointsList[i].distanceFromAvoider; //find the the point with the minimum distance

                    }
                    
                }
            }
        }
       
        
        

        

    }

    


}


