using UnityEditor;
using UnityEngine;

namespace Extension.Editor
{
    public static class MoveInHierarchy
    {
        [MenuItem("Shortcuts/Move Up In Hierarchy _#&D")]
        public static void MoveUpwards()
        {
            if(Application.isPlaying)
            {
                return;
            }

            foreach(var go in Selection.gameObjects)
            {
                var t = go.transform;

                if(t.parent)
                {
                    if(t.GetSiblingIndex() == 0)
                    {
                        var parent = t.parent;

                        t.parent = t.parent.parent;
                        t.SetSiblingIndex(parent.GetSiblingIndex());
                    }
                    else
                    {
                        t.SetSiblingIndex(t.GetSiblingIndex() - 1);
                    }
                }
                else
                {
                    var newIndex = t.GetSiblingIndex() - 1;
                    t.SetSiblingIndex(Mathf.Clamp(newIndex, 0, newIndex));
                }
            }
        }

        [MenuItem("Shortcuts/Move Down In Hierarchy _#&V")]
        public static void MoveDownwards()
        {
            if(Application.isPlaying)
            {
                return;
            }

            for(int i = Selection.gameObjects.Length - 1; i >= 0; i--)
            {
                var t = Selection.gameObjects[i].transform;

                if(t.parent)
                {
                    if(t.GetSiblingIndex() == t.parent.childCount - 1)
                    {
                        var parent = t.parent;

                        t.parent = t.parent.parent;
                        t.SetSiblingIndex(parent.GetSiblingIndex() + 1);
                    }
                    else
                    {
                        t.SetSiblingIndex(t.GetSiblingIndex() + 1);
                    }
                }
                else
                {
                    t.SetSiblingIndex(t.GetSiblingIndex() + 1);
                }
            }
        }

        #region Deprecated
        //private void OnSceneGUI()
        //{
        //    if(Selection.gameObjects.Length == 0)
        //        return;

        //    Event e = Event.current;

        //    switch(e.type)
        //    {
        //        case EventType.keyDown:
        //            if(Event.current.keyCode == KeyCode.DownArrow && Event.current.alt)
        //            {
        //                foreach(var go in Selection.gameObjects)
        //                {
        //                    Transform t = go.transform;

        //                    t.SetSiblingIndex(t.GetSiblingIndex() + 1);
        //                }
        //            }
        //            else if(Event.current.keyCode == KeyCode.UpArrow && Event.current.alt)
        //            {
        //                foreach(var go in Selection.gameObjects)
        //                {
        //                    Transform t = go.transform;

        //                    t.SetSiblingIndex(t.GetSiblingIndex() - 1);
        //                }
        //            }
        //            break;
        //    }
        //}
        #endregion
    }
}
