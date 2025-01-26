using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.Untitled
{
    public class BattleCharacterEntity : GridEntity
    {
        protected override void Start()
        {
            base.Start();
            BattleSystem.GetSingleton().RegisterBattler(this);
        }


        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (!isQuitting)
                BattleSystem.GetSingleton().UnregisterBattler(this);
        }


        public void SetCharacter(BattleCharacter character)
        {

        }
    }
}
