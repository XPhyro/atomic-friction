using UnityEngine;

namespace Extension.Native
{
    public static class GameObjectExtension
    {
        /// <summary>
        /// Toggles the active status of the GameObject.
        /// </summary>
        /// <param name="go"></param>
        public static void ToggleActive(this GameObject go)
        {
            go.SetActive(!go.activeSelf);
        }

        /// <summary>
        /// Sets the GameObject's active property if it is not null
        /// </summary>
        /// <param name="go"></param>
        /// <param name="active"></param>
        public static void SetActiveIfNotNull(this GameObject go, bool active)
        {
            if(go)
            {
                go.SetActive(active);
            }
        }

        /// <summary>
        /// Sets the GameObjects' active property
        /// </summary>
        /// <param name="gos"></param>
        /// <param name="active"></param>
        public static void SetActive(this GameObject[] gos, bool active)
        {
            foreach(var go in gos)
            {
                go.SetActive(active);
            }
        }

        /// <summary>
        /// Sets the GameObjects' active property respectively
        /// </summary>
        /// <param name="gos"></param>
        /// <param name="actives"></param>
        public static void SetActive(this GameObject[] gos, bool[] actives)
        {
            if(gos.Length != actives.Length)
            {
                throw new System.ArgumentOutOfRangeException("actives", "The lengths of actives and gos do not meet.");
            }
            else
            {
                for(int i = 0; i < gos.Length; i++)
                {
                    gos[i].SetActive(actives[i]);
                }
            }
        }

        /// <summary>
        /// Sets the GameObjects' active property if the element is not null
        /// </summary>
        /// <param name="gos"></param>
        /// <param name="active"></param>
        public static void SetActiveIfNotNull(this GameObject[] gos, bool active)
        {
            foreach(var go in gos)
            {
                if(go)
                {
                    go.SetActive(active);
                }
            }
        }

        /// <summary>
        /// Sets the GameObjects' active property respectively if the element is not null
        /// </summary>
        /// <param name="gos"></param>
        /// <param name="actives"></param>
        public static void SetActiveIfNotNull(this GameObject[] gos, bool[] actives)
        {
            if(gos.Length != actives.Length)
            {
                throw new System.ArgumentOutOfRangeException("actives", "The lengths of actives and gos do not meet.");
            }
            else
            {
                for(int i = 0; i < gos.Length; i++)
                {
                    if(gos[i])
                    {
                        gos[i].SetActive(actives[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the GameObjects' active property if none of the elements is null
        /// </summary>
        /// <param name="gos"></param>
        /// <param name="active"></param>
        public static void SetActiveIfNotNullAll(this GameObject[] gos, bool active)
        {
            foreach(var go in gos)
            {
                if(!go)
                {
                    return;
                }
            }

            foreach(var go in gos)
            {
                go.SetActive(active);
            }
        }

        /// <summary>
        /// Sets the GameObjects' active property respectively if none of the elements is null
        /// </summary>
        /// <param name="gos"></param>
        /// <param name="actives"></param>
        public static void SetActiveIfNotNullAll(this GameObject[] gos, bool[] actives)
        {
            if(gos.Length != actives.Length)
            {
                throw new System.ArgumentOutOfRangeException("actives", "The lengths of actives and gos do not meet.");
            }
            else
            {
                foreach(var go in gos)
                {
                    if(!go)
                    {
                        return;
                    }
                }

                for(int i = 0; i < gos.Length; i++)
                {
                    gos[i].SetActive(actives[i]);
                }
            }
        }

        /// <summary>
        /// Sets the layer of the GameObject and its children to "layer"
        /// </summary>
        /// <param name="go"></param>
        /// <param name="layer"></param>
        public static void SetLayerOfChildrenAndSelf(this GameObject go, int layer)
        {
            foreach(var t in go.transform.GetComponentsInChildren<Transform>())
            {
                t.gameObject.layer = layer;
            }
        }
    }
}
