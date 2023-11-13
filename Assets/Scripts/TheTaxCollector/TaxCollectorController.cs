using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaxCollectorController : EnemyController
{

    [Header("Phase Control")]
    [SerializeField] private float timePerPhase = 1;
    [SerializeField] private float elapsedTime = 0;
    [SerializeField] private float rageGate = 25;
    [SerializeField] private Phases phase = Phases.SlowSpin;


    private enum Phases
    {
        SlowSpin,
        ShotgunFastChase,
        RadialSlowChase
    }

    void Start()
    {
        base.SetUp();

        elapsedTime = 0;
    }

    void FixedUpdate()
    {
        // running phase control
        DoPhases();

        // running idle animation
        bool hasAnimator = transform.TryGetComponent<Animator>(out Animator animator);
        if (hasAnimator && !animator.GetBool("Moving"))
        {
            Transform target = EnemyAction.GetTarget(transform, out bool foundTarget);
            if (foundTarget)
            {
                Vector3 direction = target.position - transform.position;
                animator.SetFloat("MoveX", direction.x);
                animator.SetFloat("MoveY", direction.y);
            }
        }
    }

    private void DoPhases()
    {
        bool enteredRage = false;

        elapsedTime += Time.deltaTime;
        if (currentHealth < rageGate)
        {      

            if (!enteredRage)
            {
                enteredRage = true;
                // Running Animation
                bool hasSpriteRenderer = transform.TryGetComponent<SpriteRenderer>(out SpriteRenderer renderer);
                if (hasSpriteRenderer)
                {
                    renderer.color = Color.red;
                }
                elapsedTime = timePerPhase + 1;
            }

            if (elapsedTime > timePerPhase)
            {
                // Running Attack
                TryToStartAction("Fast Chase");
                TryToStartAction("Shotgun");
                TryToStartAction("Radial Cannon");
                TryToStartAction("Slow Spin");
            }
            elapsedTime = 0;
        }
        else if (elapsedTime > timePerPhase)
        {
            elapsedTime = 0;
            //randomly select a phase that isn't current
            Phases newPhase;
            do
            {
                newPhase = (Phases)Random.Range(0, 3);
            }
            while (newPhase == phase);
            phase = newPhase;
            switch (phase)
            {
                case Phases.SlowSpin:
                    TryToStartAction("Slow Spin");
                    break;
                case Phases.ShotgunFastChase:
                    TryToStartAction("Fast Chase");
                    TryToStartAction("Shotgun");
                    break;
                case Phases.RadialSlowChase:
                    TryToStartAction("Slow Chase");
                    TryToStartAction("Radial Cannon");
                    break;
                default:
                    Debug.LogError($"TaxCollectorController.DoPhases: Logic Error in deciding phase {phase}!");
                    break;
            }
        }
    }
}
