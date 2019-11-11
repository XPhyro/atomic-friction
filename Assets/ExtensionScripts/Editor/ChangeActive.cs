using UnityEditor;
using UnityEngine;

namespace Extension.Editor
{
    public static class ChangeActive
    {
        //[MenuItem("Shortcuts/Toggle Active _F4")]
        //public static void ToggleActive()
        //{
        //    if(Application.isPlaying)
        //    {
        //        return;
        //    }

        //    Undo.RecordObjects(Selection.gameObjects, "Enable Selected GameObjects");

        //    foreach(var go in Selection.gameObjects)
        //    {
        //        go.SetActive(!go.activeSelf);
        //    }
        //}

        [MenuItem("Shortcuts/Activate All _F4")]
        public static void Activateall()
        {
            if(Application.isPlaying)
            {
                return;
            }

            Undo.RecordObjects(Selection.gameObjects, "Enable All Children");

            foreach(var go in Selection.gameObjects)
            {
                ChangeActiveRecursively(go, true);
            }
        }

        [MenuItem("Shortcuts/Deactivate All _F5")]
        public static void Deactivateall()
        {
            if(Application.isPlaying)
            {
                return;
            }

            Undo.RecordObjects(Selection.gameObjects, "Disable All Children");

            foreach(var go in Selection.gameObjects)
            {
                ChangeActiveRecursively(go, false);
            }
        }

        private static void ChangeActiveRecursively(GameObject go, bool active)
        {
            foreach(Transform t in go.transform.GetComponentInChildren<Transform>())
            {
                ChangeActiveRecursively(t.gameObject, active);
            }

            Undo.RecordObject(go, active ? "Enable" : "Disable" + " All Children");
            go.SetActive(active);
        }
    }
}
