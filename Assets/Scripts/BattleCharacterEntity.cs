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
            var x = GridPosition.x;
            var y = GridPosition.y;
            var z = GridPosition.z;
            var node = gm.Grid.GetValue(x, y, z);
            if (node.isOccupied)
                DestroyGridObjectAt(GridPosition);
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
