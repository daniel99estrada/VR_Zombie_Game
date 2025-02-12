using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{   
    [SerializeField]
    private Animator Animator;

    public EnemyState DefaultState;
    private EnemyState _state;
    public EnemyState State
    {
        get
        {
            return _state;
        }
        set
        {   
            OnStateChange?.Invoke(_state, value);
            _state = value;
        }
    }
    public delegate void StateChageEvent(EnemyState oldState, EnemyState newState);
    public StateChageEvent OnStateChange;
    public Transform Player;
    public float updateSpeed = 0.1f;
    public float Health = 100f;

    private UnityEngine.AI.NavMeshAgent agent;

    private const string IsWalking = "IsWalking";
    private const string IsDead = "IsDead";

    private Coroutine FollowCoroutine;

    private void OnDisable()
    {
        _state = DefaultState;
    }

    void Awake() 
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        OnStateChange += HandleStateChange;
    }

    public void Spawn()
    {
        OnStateChange?.Invoke(EnemyState.Spawn, DefaultState);
    }

    void Update()
    {
        Animator.SetBool(IsWalking, agent.velocity.magnitude > 0.01f);  
        Animator.SetBool(IsDead, Health < 0.1f);
    }
    // Update is called once per frame
    private IEnumerator FollowTarget()
    {
        WaitForSeconds Wait = new WaitForSeconds(updateSpeed);

        while(enabled)
        {
            agent.SetDestination(Player.transform.position);

            yield return Wait;
        }
    }

    private void HandleStateChange(EnemyState oldState, EnemyState newState)
    {
        if(FollowCoroutine != null)
        {
            StopCoroutine(FollowTarget());
        }

        switch (newState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Patrol:
                break;
            case EnemyState.Chase:
                FollowCoroutine = StartCoroutine(FollowTarget());
                break;
        }
    } 
}
