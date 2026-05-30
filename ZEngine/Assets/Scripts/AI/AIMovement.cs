using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [SerializeField] private List<GameObject> _destinations;
    private int _currentDestinationIndex = 0;
    private NavMeshAgent _agent;

    #region Movement Variables
    [SerializeField] private float _walkingSpeed = 5f;
    [SerializeField] private float _chasingSpeed = 10f;
    [SerializeField] private float _idleDuration = 4f;
    [SerializeField] private float _destinationRange = 5f;

    #endregion

    // Update is called once per frame
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _walkingSpeed;
    }

    void Start()
    {
        StartCoroutine(WalkToDestination());
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
        _agent.SetDestination(_destinations[_currentDestinationIndex].transform.position);
    }

    private void GetNewDestination()
    {
        if(_currentDestinationIndex + 1 >= _destinations.Count)
        {
            _currentDestinationIndex = 0;
            return;
        }
        _currentDestinationIndex += 1;
    }

    private bool GetDestinationInRange()
    {
        return Vector3.Distance(_destinations[_currentDestinationIndex].transform.position, transform.position) > _destinationRange;
    }
}
