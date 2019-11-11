using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(EButton), true)]
public class EButtonEditor : ButtonEditor
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        //EditorGUILayout.PropertyField(onDownProperty);
        //EditorGUILayout.PropertyField(onUpProperty);

        serializedObject.ApplyModifiedProperties();
    }
}