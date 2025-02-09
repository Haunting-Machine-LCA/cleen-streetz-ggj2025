using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hmlca.CS
{
    public interface IFacingAngleMessageReciever
    {
        public const string MESSAGE = "UpdateFacingAngle";
        void UpdateFacingAngle(float angle);
    }
}
