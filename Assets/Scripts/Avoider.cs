using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Avoider : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public GameObject Avoidee;
    public float avoidRange = 5f;
    public float moveSpeed = 10f;
    private bool seenByPlayer = false;

    private void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        if (CheckPointVisibility(transform.position))
        {
            if ()
            {

            }
        }
    }

    void FindSpot(float size_x, float size_y, float cellSize)
    {
        var sampler = new PoissonDiscSampler(size_x, size_y, cellSize);
        List<Vector2> hidingCandidates = new List<Vector2>();

        foreach (var point in sampler.Samples())
        {

        }

        foreach (var point in sampler.Samples())
        {
            if (!CheckPointVisibility(point, ))
            {
                hidingCandidates.Add(point);
            }
        }
    }

    private bool CheckPointVisibility(Vector2 point)
    {

        if ()
        {
            seenByPlayer = false;
        }
        return seenByPlayer;
    }
}
