using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.Untitled
{
    [RequireComponent(typeof(GridEntity))]
    public class EnemyAI : MonoBehaviour
    {
        public class Plan
        {
            public GridEntity target;
            public Queue<Action> actions = new Queue<Action>();
            public bool failed;


            public IEnumerator ExecutePlan()
            {
                while (actions.Count > 0)
                {
                    yield return actions.Dequeue()
                        .Execute(this);
                    if (failed)
                        yield break;
                }
            }
        }


        public abstract class Action
        {
            public GridEntity target;
            public abstract IEnumerator Execute(Plan plan);
        }


        public class MoveTowardTarget : Action
        {
            public override IEnumerator Execute(Plan plan)
            {
                throw new System.NotImplementedException();
            }
        }


        public abstract class AttackTarget : Action
        {
            public override IEnumerator Execute(Plan plan)
            {
                throw new System.NotImplementedException();
            }
        }


        public static Queue<Plan> currentTurnPlans = new Queue<Plan>();


        public bool executePlan;


        public bool TryReadyPlan(out Plan plan)
        {
            executePlan = false;
            plan = ReadyPlan();
            return true;
        }


        public Plan ReadyPlan()
        {
            return null;
        }
    }
}
