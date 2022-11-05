using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

namespace TSN_Utility
{
    /**
     * Extended handler for objects search.
     */
    public static class ObjectsFinder
    {
        /**
         * Searches for child objects within provided parent object by provided tag.
         * 
         * @param string tag
         *   Tag to search for.
         *   
         * @param GameObject parent
         *   Parent object to search in.
         *   
         * @return GameObject[]
         *   Search results as array.
         */
        public static GameObject[] FindChildrenInGameObjectByTag(string tag, GameObject parent)
        {
            var results = GameObject.FindGameObjectsWithTag(tag).Where(element => element.transform.IsChildOf(parent.transform)).ToArray(); ;
            return results;
        }
    }
}

