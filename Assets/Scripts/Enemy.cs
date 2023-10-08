using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public enum State
    {
        Idle,
        Chase,
        Attack
    }

    private State curState = State.Idle;

    [Header("Ranges")]
    [SerializeField] private float chaseRange;
    [SerializeField] private float attackRange;

    [Header("Attack")]
    [SerializeField] private float attackRate;
    private float lastAttackTime;
    [SerializeField] private GameObject attackPrefab;

    private float targetDistance;

    void Start ()
    {
        target = Player.Current;
    }

    void Update ()
    {
        if(target == null)
            return;

        // Calculate distance to target.
        targetDistance = Vector3.Distance(transform.position, target.transform.position);
        
        // Run the respective update function based on our current state.
        switch(curState)
        {
            case State.Idle:
                IdleUpdate();
                break;
            case State.Attack:
                AttackUpdate();
                break;
            case State.Chase:
                ChaseUpdate();
                break;
        }
    }

    // Called when we want to change our state.
    void SetState (State newState)
    {
        curState = newState;

        // On enter state.
        switch(curState)
        {
            case State.Idle:
                Controller.StopMovement();
                break;
            case State.Chase:
                Controller.MoveToTarget(target.transform);
                break;
            case State.Attack:
                Controller.StopMovement();
                break;
        }
    }

    // Called every frame while in the IDLE state.
    void IdleUpdate ()
    {
        if(targetDistance < chaseRange && targetDistance > attackRange)
            SetState(State.Chase);
        else if(targetDistance < attackRange)
            SetState(State.Attack);
    }

    // Called every frame while in the CHASE state.
    void ChaseUpdate ()
    {
        if(targetDistance > chaseRange)
            SetState(State.Idle);
        else if(targetDistance < attackRange)
            SetState(State.Attack);
    }

    // Called every frame while in the ATTACK state.
    void AttackUpdate ()
    {
        if(targetDistance > attackRange)
            SetState(State.Chase);

        Controller.LookTowards(target.transform.position - transform.position);

        if(Time.time - lastAttackTime > attackRate)
        {
            lastAttackTime = Time.time;
            AttackTarget();
        }
    }

    // Create a projectile and shoot it at our target.
    void AttackTarget ()
    {
        GameObject proj = Instantiate(attackPrefab, transform.position + Vector3.up, Quaternion.LookRotation(target.transform.position - transform.position));
        proj.GetComponent<Projectile>().Setup(this);
    }
}