using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hmlca.CS
{
    public class Battle
    {
        public List<BattleCharacter> battlers = new List<BattleCharacter>();
        public int width;
        public int height;


        public static Battle Tutorial()
        {
            Battle battle = new Battle();
            battle.width = 12;
            battle.height = 12;
            battle.battlers.Add(BattleCharacter.Player());
            battle.battlers.Add(BattleCharacter.Enemy1());
            return battle;
        }


        public static Battle FinalBoss()
        {
            Battle battle = new Battle();
            battle.width = 16;
            battle.height = 16;
            battle.battlers.Add(BattleCharacter.Enemy3());
            return battle;
        }


        public List<GameObject> Spawn()
        {
            List<GameObject> battlerGOs = new List<GameObject>();
            var gridManager = Object.FindObjectOfType<GridManager>();//GridManager.GetSingleton();

            foreach (BattleCharacter battler in battlers)
            {
                GameObject gameObject = battler.Spawn();

                // Choose spot to spawn
                bool validPos = false;
                int xCoord = 0;
                int yCoord = 0;
                while (!validPos)
                {
                    // Spawn characters on the sidewalk only
                    xCoord = Random.Range(0, width);
                    if (xCoord == 0 || xCoord == width - 1) 
                    yCoord = Random.Range(0, gridManager.depth);
                    else
                        yCoord = Random.Range(0, 2);
                    GridNode node = gridManager.Grid.GetValue(xCoord, 1, yCoord);
                    if (!node.isOccupied)
                        validPos = true;
                }

                Vector3Int gridPos = new Vector3Int(xCoord, 1, yCoord);
                gridManager.PlaceObject(gameObject, gridPos);
                battlerGOs.Add(gameObject);
            }

            return battlerGOs;
        }
    }
}
