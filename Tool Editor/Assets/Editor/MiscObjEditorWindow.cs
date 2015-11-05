using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;


public enum MovementPattern
{
    Stationary,
    Straight,
    SinCurve,
    Circular
}

public enum ObjectType
{
    Null = -1,
    Mesh,
    Sprite

}

public class MiscObjEditorWindow : EditorWindow 
{
    Transform parentTransform;
    GameObject baseObject;
    ObjectType objType;
    
    float size;
    string objName;
    bool manualDirection;
    Vector3 direction;
    MovementPattern movementPattern;

    Color color;
    int spawnAmount;
    
    private List<GameObject> collection = new List<GameObject>();

    private bool mSpawned;
    private int mNumSpawned;


    private Vector2 scrolling = Vector2.zero;
    private MessageType baseObjectWarning;

    [MenuItem("Tools/Misc Obj Gen")]
    public static void showWindow()
    {
        EditorWindow.GetWindow(typeof(MiscObjEditorWindow));

    }

    void OnGUI()
    {
        //OBject and Parent
        scrolling = EditorGUILayout.BeginScrollView(scrolling);
        {
            parentTransform = (Transform)EditorGUILayout.ObjectField("Parent", parentTransform, typeof(Transform));

            
            baseObject = (GameObject)EditorGUILayout.ObjectField("Base Object", baseObject, typeof(GameObject));
            if (baseObject)
            {
                if (baseObject.GetComponent<MeshRenderer>())
                {
                    objType = ObjectType.Mesh;
                    baseObjectWarning = MessageType.None;
                }
                else if (baseObject.GetComponent<SpriteRenderer>())
                {
                    objType = ObjectType.Sprite;
                    baseObjectWarning = MessageType.None;
                }
                else
                {
                    objType = ObjectType.Null;
                    baseObjectWarning = MessageType.Error;
                }
            }
            else
            {
                objType = ObjectType.Null;
                baseObjectWarning = MessageType.Error;
            }
            EditorGUILayout.HelpBox("This object is of " + objType.ToString() + " type", baseObjectWarning);

            EditorGUILayout.Space();

            objName = EditorGUILayout.TextField("Object Name", objName);

            spawnAmount = EditorGUILayout.IntField("Spawn Amount", spawnAmount);
            

            movementPattern = (MovementPattern)EditorGUILayout.EnumPopup("Movement Style", movementPattern);

            EditorGUILayout.Space();
            
            if(!manualDirection) EditorGUILayout.HelpBox("Will randomly set direction if unchecked", MessageType.Info);
            
            manualDirection = EditorGUILayout.BeginToggleGroup(new GUIContent("Set Manual Direction"), manualDirection);
            {
                direction = EditorGUILayout.Vector3Field("Direction", direction);
            }
            
            EditorGUILayout.EndToggleGroup();

            color = EditorGUILayout.ColorField("Object Color", color);




            //Buttons

            EditorGUILayout.BeginHorizontal();
            {
                if (baseObject && GUILayout.Button("Spawn Objects"))
                    SpawnObjects();
                if (GUILayout.Button("Undo Spawn"))
                    UndoSpawn();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Remove All Objects"))
                    DeleteAllObjects();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Keep and Close"))
                    Close();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();
        }
    }




    public void SpawnObjects()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject miscObject = Instantiate(baseObject);
            collection.Add(miscObject);
            miscObject.name = objName + (i + 1);

            if (parentTransform != null)    
                miscObject.transform.parent = parentTransform.transform;

            if(objType == ObjectType.Mesh)
                miscObject.GetComponent<MeshRenderer>().material.color = color;
            if(objType == ObjectType.Sprite)
                miscObject.GetComponent<SpriteRenderer>().color = color;
            
            
            if(!manualDirection)
                direction = new Vector3((float)Random.Range(0, 360),(float)Random.Range(0, 360),(float)Random.Range(0, 360));
            miscObject.transform.forward = direction.normalized;


        }

        mSpawned = true;
        mNumSpawned = spawnAmount;
    }
    public void UndoSpawn()
    {
        if(!mSpawned) return;

        int end = collection.Count - 1;
        for (int i = end; i > end - mNumSpawned; i--) 
        {
            DestroyImmediate(collection[i]);
            collection.RemoveAt(i);
        }

        mSpawned = false;
    }

    public void DeleteAllObjects()
    {
        for (int i = 0; i < collection.Count; i++)
        {
            DestroyImmediate(collection[i]);
        }
        collection.Clear();
        mSpawned = false;

    }
}
