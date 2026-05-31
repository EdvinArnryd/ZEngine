using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    private Vector3[] _destinations;
    private int _currentDestinationIndex = 0;
    private NavMeshAgent _agent;

    #region Movement Variables
    [SerializeField] private float _walkingSpeed = 10f;
    [SerializeField] private float _chasingSpeed = 10f;
    [SerializeField] private float _idleDuration = 1f;
    [SerializeField] private float _destinationRange = 5f;

    #endregion

    // Update is called once per frame
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _walkingSpeed;
        _destinations = new Vector3[transform.childCount];
    }

    void Start()
    {
        StartCoroutine(WalkToDestination());

        GetChildrenPositions();
    }

    private void GetChildrenPositions()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _destinations[i] = transform.GetChild(i).position;
        }
    }

    IEnumerator WalkToDestination()
    {
        while (GetDestinationInRange())
        {
            SetAgentDestination();
            yield return null;
        }
        GetNewDestination();
        StartCoroutine(StayIdle());
    }

    IEnumerator StayIdle()
    {
        yield return new WaitForSeconds(_idleDuration);
        StartCoroutine(WalkToDestination());
    }

    private void SetAgentDestination()
    {
        _agent.SetDestination(_destinations[_currentDestinationIndex]);
    }

    private void GetNewDestination()
    {
        if(_currentDestinationIndex + 1 >= _destinations.Length)
        {
            _currentDestinationIndex = 0;
            return;
        }
        _currentDestinationIndex += 1;
    }

    private bool GetDestinationInRange()
    {
        return Vector3.Distance(_destinations[_currentDestinationIndex], transform.position) > _destinationRange;
    }
}
