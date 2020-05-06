using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace com.leothelegion.Nav
{
    [CustomEditor(typeof(NavMap))]
    public class NavMapEditior : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            NavMap map = (NavMap)target;

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }

            if (!map.isbaked)
                EditorGUILayout.HelpBox("Bake Me----Preview", MessageType.Info);
            else
                EditorGUILayout.HelpBox("I'm baked, bro", MessageType.Info);

            var x = map.mapSize.x;// = EditorGUILayout.IntField("X:", map.mapSize.x);
            var y = map.mapSize.y;// = EditorGUILayout.IntField("Y:", map.mapSize.y);

            if (GUILayout.Button("Bake"))
            {
                map.Bake(new Vector2Int(x, y));
            }
        }
    }

}