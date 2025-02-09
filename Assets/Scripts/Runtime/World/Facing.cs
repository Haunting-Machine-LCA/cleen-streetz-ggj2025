using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTween;


namespace Hmlca.CS.World
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class Facing : EntityComponent
    {
        [SerializeField] private float fullTurnSeconds;
        [SerializeField, Range(0f, 360f)] private float facingAngle;
        [SerializeField] private FloatTween tween = new FloatTween();


        public float FacingAngle => facingAngle;
        public Direction FacingDirection => facingAngle.ToDirection();


        private void Update()
        {
            if (tween.State == TweenState.Running && facingAngle != tween.EndValue)
            {
                tween.Update(Time.deltaTime);
                float targetAngle = tween.CurrentValue;
                float delta = Mathf.DeltaAngle(facingAngle, targetAngle);
                float newAngle = facingAngle + delta;
                facingAngle = newAngle % 360;
                if (delta < float.Epsilon)
                    tween.Stop(StopBehavior.ForceComplete);
                SendMessage(IFacingAngleMessageReciever.MESSAGE, facingAngle);
            }
        }


        public void SetFacing(Direction direction)
        {
            float angle = direction.ToAngle();
            SetFacing(angle);
        }


        public void SetFacing(float angle)
        {
            float delta = Mathf.DeltaAngle(facingAngle, angle);
            if (delta != 0f)
                Rotate(delta);
        }


        public void Rotate(float delta)
        {
            float startAngle = facingAngle % 360;
            float targetAngle = (facingAngle + delta) % 360f;
            float timeSeconds = Mathf.Abs(delta / 360f) * fullTurnSeconds;
            tween.Start(startAngle, targetAngle, timeSeconds, ScaleFuncs.Linear);
            if (timeSeconds <= 0f && targetAngle == startAngle)
                tween.Stop(StopBehavior.ForceComplete);
        }
    }
}
