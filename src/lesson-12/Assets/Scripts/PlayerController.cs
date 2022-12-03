using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    protected NavMeshAgent navAgent;
    public NavMeshAgent NavAgent { get => navAgent ?? GetComponent<NavMeshAgent>(); }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 clickPosition = Input.mousePosition;   

            RaycastHit RayHitData;
            Ray ray = Camera.main.ScreenPointToRay(clickPosition);
            Physics.Raycast(ray, out RayHitData);
            Debug.Log($"RayHitartBody: {RayHitData.articulationBody} / RayHit point: {RayHitData.point}");
            NavAgent.destination = RayHitData.point;
        }
    }
}
