using UnityEngine;
using UnityEditor;

public class ConditionalHideAttribute : PropertyAttribute
{
    public string[] ConditionalSourceFields;
    public bool[] HideIfTrue;

    public ConditionalHideAttribute(string[] conditionalSourceFields, bool[] hideIfTrue)
    {
        this.ConditionalSourceFields = conditionalSourceFields;
        this.HideIfTrue = hideIfTrue;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
public class ConditionalHidePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

        bool wasEnabled = GUI.enabled;
        GUI.enabled = enabled;

        if (enabled)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }

        GUI.enabled = wasEnabled;
    }

    private bool GetConditionalHideAttributeResult(ConditionalHideAttribute condHAtt, SerializedProperty property)
    {
        for (int i = 0; i < condHAtt.ConditionalSourceFields.Length; i++)
        {
            SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(condHAtt.ConditionalSourceFields[i]);
            if (sourcePropertyValue != null)
            {
                bool conditionMet = sourcePropertyValue.boolValue;
                if (conditionMet == condHAtt.HideIfTrue[i])
                {
                    return false;
                }
            }
        }
        return true;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

        if (enabled)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
        else
        {
            return -EditorGUIUtility.standardVerticalSpacing;
        }
    }
}
#endif