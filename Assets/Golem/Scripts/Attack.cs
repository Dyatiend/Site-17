using UnityEngine;

public class Attack : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Animator animator;

    public GameObject hammer;
    private Collider hammerCollider;

    //----- ���� ���� ----------
    private SimpleAttack simpleAttack;
    private MegaPunch megaPunch;
    private SpellAttack spellAttack;

    //------�� -----------------
    public float AttackCoolDown;
    public float AbilityCoolDown;
    public float UltimateCoolDown;

    private float AttackCoolDown2;
    private float AbilityCoolDown2;
    private float UltimateCoolDown2;

    private bool canAttack;
    private bool canAbility;
    private bool canUltimate;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        hammerCollider = hammer.GetComponent<Collider>();

        simpleAttack = GetComponent<SimpleAttack>();
        megaPunch = GetComponent<MegaPunch>();
        spellAttack = GetComponent<SpellAttack>();

        canAbility = true;
        canUltimate = true;
        canAttack = true;

        AbilityCoolDown2 = AbilityCoolDown;
        UltimateCoolDown2 = UltimateCoolDown;
        AttackCoolDown2 = AttackCoolDown;

    }


    void Update()
    {
        Vector3 forward = transform.forward;

        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            canAttack = false;
            simpleAttack.Attack();
        }
        if (Input.GetKeyDown(KeyCode.Q) && canUltimate)
        {
            canUltimate = false;
            megaPunch.Attack();          
        }
        if (Input.GetKeyDown(KeyCode.E) && canAbility)
        {
            canAbility = false;
            spellAttack.Attack();        
        }

        countCooldown();
    }

    private void countCooldown()
    {
        if (!canAbility)
        {
            AbilityCoolDown2 -= Time.deltaTime;
            if (AbilityCoolDown2 < 0)
            {
                canAbility = true;
                AbilityCoolDown2 = AbilityCoolDown;
            }
        }

        if (!canUltimate)
        {
            UltimateCoolDown2 -= Time.deltaTime;
            if (UltimateCoolDown2 < 0)
            {
                canUltimate = true;
                UltimateCoolDown2 = UltimateCoolDown;
            }
        }

        if (!canAttack)
        {
            AttackCoolDown2 -= Time.deltaTime;
            if (AttackCoolDown2 < 0)
            {
                canAttack = true;
                AttackCoolDown2 = AttackCoolDown;
            }
        }

    }

}
