using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.CS
{
    public class CheatInputs : MonoBehaviour
    {
        private const KeyCode CHEAT_RESET_GAME = KeyCode.F2;
        private const KeyCode CHEAT_RESET_BATTLE = KeyCode.F3;
        private const KeyCode CHEAT_NEXT_BATTLE = KeyCode.F4;
        private const KeyCode CHEAT_WIN = KeyCode.F7;
        private const KeyCode CHEAT_LOSE = KeyCode.F8;
        private const KeyCode CHEAT_DMG_PLAYER = KeyCode.Minus;


        private void Update()
        {
            if (Input.GetKeyDown(CHEAT_RESET_GAME))
            {
            }
            else if (Input.GetKeyDown(CHEAT_RESET_BATTLE))
            {
                BattleSystem.GetSingleton().ResetBattle(true);
            }
            else if (Input.GetKeyDown(CHEAT_NEXT_BATTLE))
            {
                BattleSystem.GetSingleton().NextBattle();
            }
            else if (Input.GetKeyDown(CHEAT_WIN))
            {
                BattleSystem.GetSingleton().Win();
            }
            else if (Input.GetKeyDown(CHEAT_LOSE))
            {
                BattleSystem.GetSingleton().Lose();
            }
            else if (Input.GetKeyDown(CHEAT_DMG_PLAYER))
            {
                PlayerHp.GetSingleton()?
                    .GetComponentInChildren<CharacterHp>()
                    .Damage(10);
            }
        }
    }
}
