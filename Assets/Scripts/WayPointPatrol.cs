using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WayPointPatrol : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    public Transform[] wayPoints;

    private int currentWayPointIndex;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(wayPoints[0].position);
    }

    private void Update()
    {
        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            //si hay 5 de lenght, cuando llegemos al elemento 4: 4+1 % 5 =0, asiq ue volveria a empezar
            //Y en los otros: b = divisor , a = dividendo.
            // a mod b <- esto es lo mismo que decir -> el modulo de a / b
            //Regla de modulo: Si b > a > 0, entonces a mod b  = a
            //Por lo tanto:
            //currentWayPointIndex when index = 0 -> (0+1) mod 5 = 1
            //currentWayPointIndex when index = 1 -> (1+1) mod 5 = 2
            //currentWayPointIndex when index = 2 -> (2+1) mod 5 = 3
            //currentWayPointIndex when index = 3 -> (3+1) mod 5 = 4
            //currentWayPointIndex when index = 4 -> (4+1) mod 5 = 0
            currentWayPointIndex = (currentWayPointIndex + 1) % wayPoints.Length; 
            navMeshAgent.SetDestination(wayPoints[currentWayPointIndex].position);
        }
    }


}
