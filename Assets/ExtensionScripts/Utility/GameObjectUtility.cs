using System.Collections.Generic;
using UnityEngine;

namespace Extension.Utility
{
    public class GameObjectUtility
    {
        //do FindGameObjectsInLayers (with layermasks and layer arrays

        /// <summary>
        /// Returns all of the GameObjects in layer "layer" in the current scene
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static GameObject[] FindGameObjectsInLayer(string layer)
        {
            var gameObjArray = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));
            var gameObjList = new List<GameObject>();

            var convertedLayer = LayerMask.NameToLayer(layer);

            foreach(var go in gameObjArray)
            {
                if(go.layer == convertedLayer)
                {
                    gameObjList.Add(go);
                }
            }

            if(gameObjList.Count != 0)
            {
                return gameObjList.ToArray();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns all of the GameObjects in layer "layer" in the current scene
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static GameObject[] FindGameObjectsInLayer(int layer)
        {
            var gameObjArray = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));
            var gameObjList = new List<GameObject>();

            foreach(var go in gameObjArray)
            {
                if(go.layer == layer)
                {
                    gameObjList.Add(go);
                }
            }

            if(gameObjList.Count != 0)
            {
                return gameObjList.ToArray();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the first GameObject it finds in the current scene named "name"
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject FindFirstGameObjectNamed(string name)
        {
            var gos = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));

            foreach(var go in gos)
            {
                if(go.name == name)
                {
                    return go;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns all of the GameObjects it finds in the current scene named "name"
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject[] FindGameObjectsNamed(string name)
        {
            var gosArray = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));
            var gosList = new List<GameObject>();

            foreach(var go in gosArray)
            {
                if(go.name == name)
                {
                    gosList.Add(go);
                }
            }

            if(gosList.Count != 0)
            {
                return gosList.ToArray();
            }
            else
            {
                return new GameObject[0];
            }
        }
    }
}
