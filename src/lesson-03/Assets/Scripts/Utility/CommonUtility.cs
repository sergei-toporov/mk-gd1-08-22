using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lesson3
{
    /**
     * Utility class for keeping different useful stuff.
     */ 
    public static class CommonUtility
    {

        /**
         * Checks the state of the Game Manager object.
         * 
         * @param IGameManager
         *   An instance of IGameManager.
         *   
         * @return bool
         *   TRUE - everything is OK, FALSE - something is wrong.
         */
        public static bool IsLaunchable(GameManager gameManager)
        {
            if (!gameManager.GameUI)
            {
                Debug.LogError("GameUI is not assigned!");
                return false;
            }

            if (gameManager.ShipPrefabs == null || gameManager.ShipPrefabs.Length == 0)
            {
                Debug.LogError("Ship Prefabs are either empty or faulted!");
                return false;
            }
          
            return true;
        }
    }
}