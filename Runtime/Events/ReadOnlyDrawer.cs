using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ReadOnlyAttribute : PropertyAttribute
{

}
public class ClampFloatAttribute : PropertyAttribute
{

    public float max;
    public float min;

    public ClampFloatAttribute(float a,float b)
    {
        min = a;
        max = b;
    }
}

public class ClampIntAttribute : PropertyAttribute
{

    public int max;
    public int min;

    public ClampIntAttribute(int a, int b)
    {
        min = a;
        max = b;
    }
}

public class DirectionIntAttribute : PropertyAttribute
{
    public int value;
    public bool forward;
    public DirectionIntAttribute()
    {
        if (value < 0)
        {
            value = -1;
        }
        else
        {
            value = 1;
        }
    }
}


#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
   
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
       
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}

[CustomPropertyDrawer(typeof(ClampIntAttribute))]
public class ClampIntDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect rect = position;// new Rect(position.position,new Vector2(100,20));
        ClampIntAttribute c = (ClampIntAttribute)base.attribute;
        property.intValue = EditorGUI.IntField(rect, label, Mathf.Clamp(property.intValue, c.min, c.max)) ;
    }
}


[CustomPropertyDrawer(typeof(ClampFloatAttribute))]
public class ClampFloatDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect rect = position;// new Rect(position.position,new Vector2(100,20));
        ClampFloatAttribute c = (ClampFloatAttribute)base.attribute;
        property.floatValue = EditorGUI.FloatField(rect,label, Mathf.Clamp(property.floatValue, c.min, c.max));
    }
}

[CustomPropertyDrawer(typeof(DirectionIntAttribute))]
public class DirectionIntDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect rect = position;// new Rect(position.position,new Vector2(100,20));
        Rect rect1 = position;
        rect.position = rect.position+ new Vector2(20, 0);
        DirectionIntAttribute c = (DirectionIntAttribute)base.attribute;
        c.forward = EditorGUI.Toggle(rect1, c.forward);
        property.boolValue = c.forward;
        if (c.forward) c.value = 1;
        else c.value = -1;
        c.value = EditorGUI.IntField(rect, label, c.value);
    }
}
#endif