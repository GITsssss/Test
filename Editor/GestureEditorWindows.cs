using UnityEngine;
using UnityEditor;
using UnityEngine.Animations.Rigging;
public class GestureEditorWindows : EditorWindow
{
    float thumb_L;
    float thumb_R;
    float indexfinger_L;
    float indexfinger_R;
    float middlefinger_L;
    float middlefinger_R;
    float ringfinger_L;
    float ringfinger_R;
    float littlefinger_L;
    float littlefinger_R;
    float inverti_L;//手指间隔
    float inverti_R;//手指间隔
    float lastsetl;
    float lastsetr; 
    float SETL;
    float SETR;
    public GameObject lefthand;
    public Transform[] leftbones;
    public Transform leftBone;//骨骼父物体
    public Quaternion[] leftori;

    public Transform hr;
    SerializedObject gew;
    SerializedProperty handl;

    SerializedProperty leftbones_sp;
    SerializedProperty handr;

    Vector2 rectarea;
    [MenuItem("HLVR/Tool/GestureEditorWindow")]
    public static void Win()
    { 
        EditorWindow.GetWindow<GestureEditorWindows>();
    }

    private void OnEnable()
    {
        gew=new SerializedObject(this);
        handl = gew.FindProperty("leftBone");
        handr = gew.FindProperty("hr");
        leftbones_sp = gew.FindProperty("leftbones");
    }

    GUIStyle gUIStyle = new GUIStyle();
    
    private void OnGUI()
    {
        string btpath = "Packages/com.hlvr/ButtonIcon";
        string[] btnames = AssetDatabase.FindAssets("", new[] { btpath });
        Texture bt1 = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath(btnames[2]));
        Texture bt2 = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath(btnames[3]));



        gUIStyle.normal.textColor= Color.white;
        gUIStyle.fontSize = 60;
        gUIStyle.fontStyle = FontStyle.Bold;
        EditorGUILayout.BeginHorizontal();
        EditorGUI.DrawPreviewTexture(new Rect(100, 50, 300, 300), bt1);
        thumb_L = GUI.VerticalSlider(new Rect(320, 180, 10, 90), thumb_L, 0, 1);
        indexfinger_L = GUI.VerticalSlider(new Rect(285, 120, 10, 150), indexfinger_L, 0, 1);
        middlefinger_L = GUI.VerticalSlider(new Rect(245, 90, 10, 180), middlefinger_L, 0, 1);
        ringfinger_L = GUI.VerticalSlider(new Rect(210, 100, 10, 170), ringfinger_L, 0, 1);
        littlefinger_L = GUI.VerticalSlider(new Rect(170, 135, 10, 135), littlefinger_L, 0, 1);
      
        inverti_L = GUI.HorizontalSlider(new Rect(200, 280, 100, 10), inverti_L, 0, 1);
        EditorGUI.LabelField(new Rect(100, 100, 50, 50), "L", new GUIStyle(gUIStyle));
        SETL = GUI.VerticalSlider(new Rect(110, 170, 10, 135), SETL, 0, 1);
        if (lastsetl != SETL) 
        {
            thumb_L= SETL;
            indexfinger_L = SETL;
            middlefinger_L= SETL;
            ringfinger_L = SETL;
            littlefinger_L=SETL;
            lastsetl = SETL;
        }
        EditorGUI.PropertyField(new Rect(110, 350, 200, 20),handl,GUIContent.none);
        rectarea= GUILayout.BeginScrollView(rectarea);
        GUILayout.EndScrollView();
        GUI.Button(new Rect(110, 400, 70, 20),"重置拉杆值");
        GUI.Button(new Rect(190, 400, 100, 20), "重置手值弯度值");

        if (GUI.Button(new Rect(290, 400, 100, 20), "加载骨骼")) 
        {
            GameObject ins_LEft = Resources.Load("LeftHand") as GameObject;
            lefthand = Instantiate(ins_LEft);
            lefthand.AddComponent<BoneRenderer>();
            leftBone = lefthand.transform.GetChild(0).GetChild(5);

            GetHandBones();

            leftori = new Quaternion[leftbones.Length];
            for (int i = 0; i < leftbones.Length; i++)
            {
                leftori[i] = leftbones[i].localRotation;
            }
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        EditorGUI.DrawPreviewTexture(new Rect(100+300, 50, 300, 300), bt2);
        thumb_R = GUI.VerticalSlider(new Rect(170 + 300, 180, 10, 90), thumb_R, 0, 1);
        indexfinger_R = GUI.VerticalSlider(new Rect(210 + 300, 120, 10, 150), indexfinger_R, 0, 1);
        middlefinger_R = GUI.VerticalSlider(new Rect(245 + 300, 90, 10, 180), middlefinger_R, 0, 1);
        ringfinger_R = GUI.VerticalSlider(new Rect(285 +300, 100, 10, 170), ringfinger_R, 0, 1);
        littlefinger_R = GUI.VerticalSlider(new Rect(320 + 300, 135, 10, 135), littlefinger_R, 0, 1);
        inverti_R = GUI.HorizontalSlider(new Rect(200 + 300, 280, 100, 10), inverti_R, 0, 1);
        EditorGUI.LabelField(new Rect(360 + 300, 100, 50, 50), "R", new GUIStyle(gUIStyle) );
        SETR = GUI.VerticalSlider(new Rect(370 + 300, 170, 10, 135), SETR, 0, 1);
        if (lastsetr != SETR)
        {
            thumb_R = SETR;
            indexfinger_R = SETR;
            middlefinger_R = SETR;
            ringfinger_R = SETR;
            littlefinger_R = SETR;
            lastsetr = SETR;
        }
        EditorGUI.PropertyField(new Rect(150+300, 350, 200, 20), handr, GUIContent.none);
        EditorGUILayout.EndHorizontal();
    }

    public void GetHandBones()
    {
        leftbones = new Transform[24];
        for (int i=0;i< leftBone.childCount;i++)
        {
            leftbones = leftBone.GetComponentsInChildren<Transform>() ;
        }

        lefthand.GetComponent<BoneRenderer>().transforms = leftbones;
        //rightbones = new Transform[24];
        //for (int i = 0; i < leftBone.childCount; i++)
        //{
        //   rightbones = rightBone.GetComponentsInChildren<Transform>();
        //}
        //righthand.GetComponent<BoneRenderer>().transforms = rightbones;

    }
}
