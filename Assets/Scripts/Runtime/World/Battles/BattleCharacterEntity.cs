using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.CS
{
    public class BattleCharacterEntity : GridEntity
    {
        protected override void Start()
        {
            forceSpawn = true;
            base.Start();
            if (failedSpawn)
            {
                var x = GridPosition.x;
                var y = GridPosition.y;
                var z = GridPosition.z;
                var node = gm.Grid.GetValue(x, y, z);
                if (node.isOccupied)
                    DestroyGridObjectAt(GridPosition);
                if (!RegisterGridObject(this, gm, GetGridPositions()))
                {
                    Debug.LogError($"Failed to register CHARACTER {gameObject.name} @{GridPosition}: {reasonForFail}");
                    Destroy(gameObject);
                    return;
                }
            }
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
