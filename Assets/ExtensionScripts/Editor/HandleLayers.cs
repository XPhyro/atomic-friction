using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Extension.Editor
{
    public class HandleLayers : EditorWindow
    {
        private const bool DefaultToggle = false;
        private const string DefaultString = "";
        private const string DefaultTag = "Untagged";
        private const int DefaultLayer = 0;

        private List<GameObject> parents = new List<GameObject>();

        private bool mustHaveRenderer = DefaultToggle;
        private bool mustHaveCollider = DefaultToggle;
        private bool doForParentWhenParentDoesntFit = DefaultToggle;
        private bool doForChildren = DefaultToggle;
        private bool doForChildrenWhenParentDoesntFit = DefaultToggle;
        private bool onlyParentMustFit = DefaultToggle;
        private string mustHaveName = DefaultString;
        private bool mustHaveTag = DefaultToggle;
        private string tagToHave = DefaultTag;
        private bool mustHaveLayer = DefaultToggle;
        private int layerToHave = DefaultLayer;

        private int layerToSet = DefaultLayer;

        [MenuItem("Window/Extensions/Handle Layers")]
        private static void ShowWindow()
        {
            GetWindow<HandleLayers>(true, "Handle Layers", true);
        }

        private void OnGUI()
        {
            int newCount = Mathf.Max(0, EditorGUILayout.IntField("Size", parents.Count));

            while(newCount < parents.Count)
            {
                parents.RemoveAt(parents.Count - 1);
            }
            while(newCount > parents.Count)
            {
                parents.Add(null);
            }

            for(int i = 0; i < parents.Count; i++)
            {
                parents[i] = (GameObject)EditorGUILayout.ObjectField(parents[i], typeof(GameObject), true);
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Object must have a renderer");
            mustHaveRenderer = EditorGUILayout.Toggle(mustHaveRenderer);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Object must have a collider");
            mustHaveCollider = EditorGUILayout.Toggle(mustHaveCollider);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Do for parent even when parent doesn't fit");
            doForParentWhenParentDoesntFit = EditorGUILayout.Toggle(doForParentWhenParentDoesntFit);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Do for children");
            doForChildren = EditorGUILayout.Toggle(doForChildren);
            EditorGUILayout.EndHorizontal();

            if(doForChildren)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("\tDo for children even when parent doesn't fit");
                doForChildrenWhenParentDoesntFit = EditorGUILayout.Toggle(doForChildrenWhenParentDoesntFit);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("\tOnly parent must fit");
                onlyParentMustFit = EditorGUILayout.Toggle(onlyParentMustFit);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Must have the name");
            mustHaveName = EditorGUILayout.TextField(mustHaveName);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Must have a tag");
            mustHaveTag = EditorGUILayout.Toggle(mustHaveTag);
            EditorGUILayout.EndHorizontal();

            if(mustHaveTag)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("\tMust have the tag");
                tagToHave = EditorGUILayout.TagField("", tagToHave);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Must be in a layer");
            mustHaveLayer = EditorGUILayout.Toggle(mustHaveLayer);
            EditorGUILayout.EndHorizontal();

            if(mustHaveLayer)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("\tMust be in the layer");
                layerToHave = EditorGUILayout.LayerField(layerToHave);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Layer to set");
            layerToSet = EditorGUILayout.LayerField("", layerToSet);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            if(GUILayout.Button("Set layer"))
            {
                OnSetLayerButtonDown();
            }
        }

        private void OnSetLayerButtonDown()
        {
            if(parents.Count == 0 && Selection.objects.Length > 0)
            {
                SetLayer(Selection.gameObjects.ToList());
            }
            else if(parents.Count > 0)
            {
                SetLayer(parents);
            }
        }

        private void SetLayer(List<GameObject> parents)
        {
            var preFitGos = new List<GameObject>();

            if(doForChildren)
            {
                foreach(var parent in parents)
                {
                    var parentFits = !CheckForConditions(parent);

                    if(!doForChildrenWhenParentDoesntFit && parentFits)
                    {
                        continue;
                    }

                    var children = parent.GetComponentsInChildren<Transform>();
                    if(!doForParentWhenParentDoesntFit && !parentFits)
                    {
                        var childrenList = children.ToList();
                        childrenList.Remove(parent.transform);
                        children = childrenList.ToArray();
                    }

                    foreach(var child in children)
                    {
                        preFitGos.Add(child.gameObject);
                    }
                }
            }

            foreach(var go in preFitGos)
            {
                if(!onlyParentMustFit && !CheckForConditions(go))
                {
                    continue;
                }

                go.layer = layerToSet;
            }
        }

        private bool CheckForConditions(GameObject go)
        {
            if(!mustHaveRenderer && go.GetComponent<Renderer>()
                    || !mustHaveCollider && go.GetComponent<Collider>()
                    || mustHaveName == "" && mustHaveName != go.name
                    || mustHaveTag && tagToHave == go.tag
                    || mustHaveLayer && layerToHave == go.layer)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
