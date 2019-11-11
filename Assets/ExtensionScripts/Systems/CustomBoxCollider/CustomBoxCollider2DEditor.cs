using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Extension.Editor
{
    [CustomEditor(typeof(CustomBoxCollider2D))]
    [CanEditMultipleObjects]
    [DisallowMultipleComponent]
    public class CustomBoxCollider2DEditor : UnityEditor.Editor
    {
        private const string UpdateMethodLabel = "Update Method";
        private const string UpdateMethodTooltip = "Update method to use when updating the collider data." +
                                                    "\nOn Frame: The collider data will update every " +
                                                    "frame." +
                                                    "\nOn Use: Update only when used. (Will not update" +
                                                    " twice in a single frame; if such behaviour is needed," +
                                                    " trigger an update manually" +
                                                    " by using CustomBoxCollider.ForceUpdate()." +
                                                    "\nFixed Rate: the collider data will update" +
                                                    " updateRate times per second. If set to 0, the" +
                                                    " collider will only update when an" +
                                                    " update trigger is received. The maximum updates" +
                                                    " possible is still determined by the frames" +
                                                    " processed per second.";
        private const string ShowDebugLabel = "Show Debugging Info";
        private const string ShowDebugTooltip = "When enabled, debugging information will be shown";
        private const string TimeBeforeNextLabel = "Time Before Next Update";
        private const string LastUpdatedLabel = "Last Updated Frame";
        private const string LastDatachangedLabel = "Last Data Changed Frame";

        private SerializedProperty updateMethodProp;
        private SerializedProperty updateRateProp;

        private SerializedProperty showDebugProp;

        private SerializedProperty willFunctionProp;

        private SerializedProperty timeBeforeNextProp;
        private SerializedProperty lastUpdatedProp;
        private SerializedProperty lastDataChangedProp;

        private SerializedProperty scaleToSizeXProp;
        private SerializedProperty scaleToSizeYProp;

        private SerializedProperty showColliderProp;

        private SerializedProperty realSizeXProp;
        private SerializedProperty realSizeYProp;

        private SerializedProperty offsetXProp;
        private SerializedProperty offsetYProp;

        private SerializedProperty cornerAProp;
        private SerializedProperty cornerBProp;
        private SerializedProperty cornerCProp;
        private SerializedProperty cornerDProp;

        private SerializedProperty topMidProp;
        private SerializedProperty bottomMidProp;
        private SerializedProperty leftMidProp;
        private SerializedProperty rightMidProp;

        private SerializedProperty topMostProp;
        private SerializedProperty bottomMostProp;
        private SerializedProperty leftMostProp;
        private SerializedProperty rightMostProp;

        private SerializedProperty drawColliderProp;

        private void OnEnable()
        {

            updateMethodProp = serializedObject.FindProperty("updateMethod");
            updateRateProp = serializedObject.FindProperty("updateRate");

            showDebugProp = serializedObject.FindProperty("showDebuggingInfo");

            willFunctionProp = serializedObject.FindProperty("willSystemFunction");

            timeBeforeNextProp = serializedObject.FindProperty("timeBeforeUpdate");
            lastUpdatedProp = serializedObject.FindProperty("lastUpdatedFrame");
            lastDataChangedProp = serializedObject.FindProperty("lastDataChangedFrame");

            scaleToSizeXProp = serializedObject.FindProperty("scaleToRealSizeX");
            scaleToSizeYProp = serializedObject.FindProperty("scaleToRealSizeY");

            showColliderProp = serializedObject.FindProperty("showColliderInfo");

            realSizeXProp = serializedObject.FindProperty("realSizeX");
            realSizeYProp = serializedObject.FindProperty("realSizeY");

            offsetXProp = serializedObject.FindProperty("offsetX");
            offsetYProp = serializedObject.FindProperty("offsetY");

            cornerAProp = serializedObject.FindProperty("cornerA");
            cornerBProp = serializedObject.FindProperty("cornerB");
            cornerCProp = serializedObject.FindProperty("cornerC");
            cornerDProp = serializedObject.FindProperty("cornerD");

            topMidProp = serializedObject.FindProperty("topEdgeMid");
            bottomMidProp = serializedObject.FindProperty("bottomEdgeMid");
            leftMidProp = serializedObject.FindProperty("leftEdgeMid");
            rightMidProp = serializedObject.FindProperty("rightEdgeMid");

            topMostProp = serializedObject.FindProperty("topMostPoint");
            bottomMostProp = serializedObject.FindProperty("bottomMostPoint");
            leftMostProp = serializedObject.FindProperty("leftMostPoint");
            rightMostProp = serializedObject.FindProperty("rightMostPoint");

            drawColliderProp = serializedObject.FindProperty("drawCollider");
        }

        public override void OnInspectorGUI()
        {
            var editingMultipleObjects = Selection.gameObjects.Length > 1;

            serializedObject.Update();

            var method = (CustomBoxCollider2D.UpdateMethod)updateMethodProp.enumValueIndex;
            updateMethodProp.enumValueIndex = (int)(CustomBoxCollider2D.UpdateMethod)EditorGUILayout.EnumPopup(new GUIContent(UpdateMethodLabel, UpdateMethodTooltip), method);

            if(!editingMultipleObjects)
            {
                if(method == CustomBoxCollider2D.UpdateMethod.FixedRate)
                {
                    updateRateProp.intValue = Mathf.Abs(EditorGUILayout.DelayedIntField("Update Rate", updateRateProp.intValue));
                }

                var showDebug = showDebugProp.boolValue = EditorGUILayout.Toggle(new GUIContent(ShowDebugLabel, ShowDebugTooltip), showDebugProp.boolValue);

                if(showDebug)
                {
                    GUI.enabled = false;

                    EditorGUILayout.Toggle("Will System Function", willFunctionProp.boolValue);

                    if(method == CustomBoxCollider2D.UpdateMethod.FixedRate)
                    {
                        EditorGUILayout.FloatField(TimeBeforeNextLabel, timeBeforeNextProp.floatValue);
                    }
                    EditorGUILayout.IntField(LastUpdatedLabel, lastUpdatedProp.intValue);
                    EditorGUILayout.IntField(LastDatachangedLabel, lastDataChangedProp.intValue);

                    EditorGUILayout.FloatField("Scale To Size X", scaleToSizeXProp.floatValue);
                    EditorGUILayout.FloatField("Scale To Size Y", scaleToSizeYProp.floatValue);

                    GUI.enabled = true;
                }

                var showCollider = showColliderProp.boolValue = EditorGUILayout.Toggle("Show Collider Info", showColliderProp.boolValue);

                if(showCollider)
                {
                    GUI.enabled = false;

                    EditorGUILayout.FloatField("Real Size X", realSizeXProp.floatValue);
                    EditorGUILayout.FloatField("Real Size Y", realSizeYProp.floatValue);

                    EditorGUILayout.Vector3Field("Corner A", cornerAProp.vector3Value);
                    EditorGUILayout.Vector3Field("Corner B", cornerBProp.vector3Value);
                    EditorGUILayout.Vector3Field("Corner C", cornerCProp.vector3Value);
                    EditorGUILayout.Vector3Field("Corner D", cornerDProp.vector3Value);

                    EditorGUILayout.Vector3Field("Top Edge Mid", topMidProp.vector3Value);
                    EditorGUILayout.Vector3Field("Bottom Edge Mid", bottomMidProp.vector3Value);
                    EditorGUILayout.Vector3Field("Left Edge Mid", leftMidProp.vector3Value);
                    EditorGUILayout.Vector3Field("Right Edge Mid", rightMidProp.vector3Value);

                    EditorGUILayout.Vector3Field("Top Most Point", topMostProp.vector3Value);
                    EditorGUILayout.Vector3Field("Bottom Most Point", bottomMostProp.vector3Value);
                    EditorGUILayout.Vector3Field("Left Most Point", leftMostProp.vector3Value);
                    EditorGUILayout.Vector3Field("Right Most Point", rightMostProp.vector3Value);

                    GUI.enabled = true;
                }

                drawColliderProp.boolValue = EditorGUILayout.Toggle("Draw Collider", drawColliderProp.boolValue);
            }

            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            if(GUILayout.Button("Trigger Update"))
            {
                foreach(var cbc in Selection.gameObjects.Select(go => go.GetComponent<CustomBoxCollider2D>()))
                {
                    cbc.TriggerUpdate();
                }
            }
            if(GUILayout.Button("Force Update"))
            {
                foreach(var cbc in Selection.gameObjects.Select(go => go.GetComponent<CustomBoxCollider2D>()))
                {
                    cbc.ForceUpdate();
                }
            }
            if(GUILayout.Button("Check Sprite"))
            {
                foreach(var cbc in Selection.gameObjects.Select(go => go.GetComponent<CustomBoxCollider2D>()))
                {
                    cbc.CheckSprite();
                }
            }
        }
    }
}
