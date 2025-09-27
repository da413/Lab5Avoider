using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Avoider : MonoBehaviour
{
    [Header("Avoidance Settings")]
    public GameObject avoidee;
    public float avoidRange = 5f;
    public float moveSpeed = 10f;
    public bool visualize = true;

    [Header("Poisson Disc Settings")]
    public Vector2 regionSize = new Vector2(20, 20);
    public float radius = 3f;
    private NavMeshAgent navMeshAgent;
    private List<Vector2> candidateSpots = new List<Vector2>();

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(AvoidanceLoop());
    }

    private IEnumerator AvoidanceLoop()
    {
        while (true)
        {
            if (avoidee != null)
            {
                if (CanSeeMe(avoidee.transform))
                {
                    FindCandidateSpots();

                    if (candidateSpots.Count > 0)
                    {
                        Vector3 bestSpot = GetClosestSpot(candidateSpots);
                        navMeshAgent.SetDestination(bestSpot);
                    }
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void FindCandidateSpots()
    {
        candidateSpots.Clear();

        PoissonDiscSampler sampler = new PoissonDiscSampler(regionSize.x, regionSize.y, radius);

        foreach (Vector2 sample in sampler.Samples())
        {
            Vector3 worldPoint = new Vector3(
                sample.x - regionSize.x / 2f + transform.position.x,
                transform.position.y,
                sample.y - regionSize.y / 2f + transform.position.z
                );

            if (!IsVisibleToAvoidee(worldPoint))
            {
                candidateSpots.Add(new Vector2(worldPoint.x, worldPoint.z));

                if (visualize)
                {
                    Debug.DrawLine(transform.position, worldPoint, Color.green, 0.5f);
                }
            }
            else
            {
                if (visualize)
                {
                    Debug.DrawLine(transform.position, worldPoint, Color.red, 0.5f);
                }
            }
        }
    }

    private Vector3 GetClosestSpot(List<Vector2> spots)
    {
        Vector3 closest = transform.position;

        float minDistance = Mathf.Infinity;

        foreach (Vector2 spot in spots)
        {
            Vector3 worldPoint = new Vector3(spot.x, transform.position.y, spot.y);
            float distance = Vector3.Distance(transform.position, worldPoint);

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = worldPoint;
            }
        }

        return closest;
    }

    private bool CanSeeMe(Transform target)
    {
        Vector3 direction = transform.position - target.position;
        if (Physics.Raycast(target.position, direction.normalized, out RaycastHit hit, avoidRange))
        {
            return hit.transform == transform;
        }
        return false;
    }

    private bool IsVisibleToAvoidee(Vector3 point)
    {
        Vector3 direction = point - avoidee.transform.position;

        if (Physics.Raycast(avoidee.transform.position, direction.normalized, out RaycastHit hit, avoidRange))
        {
            if (hit.point != point && hit.collider.transform != this.transform)
            {
                return false;
            }
        }

        return true;
    }
}
