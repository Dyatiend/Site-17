using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    private Animator animator;
    private State state;

    public GameObject ability;
    public Transform abilityPoint;

    public float timeBeforeAttack;
    public float timeBetweenAttacks;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        state = GetComponent<State>();
    }

    public void MakeAbility()
    {
        state.ChangeState(State.States.ABILITY);

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        animator.SetFloat("Velocity", 0);
        animator.SetTrigger("Ability");

        Invoke(nameof(Reload), timeBetweenAttacks);
        Invoke(nameof(CreateAbilityEffect), timeBeforeAttack);
    }

    public void Reload()
    {
        state.ChangeState(State.States.IDLE);
    }

    public void CreateAbilityEffect()
    {
        Instantiate(ability, abilityPoint.position, abilityPoint.rotation);
    }
}