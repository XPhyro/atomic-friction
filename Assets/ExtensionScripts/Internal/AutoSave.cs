#if UNITY_EDITOR

using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;


namespace Extension.Internal
{
    [InitializeOnLoad]
    public static class AutoSaveHandler
    {

        //* private *//
        private static float m_saveInterval = 60;
        private static bool m_debug = false;
        private static double? launchTime;
        private static bool m_enable = false;
        private static int m_filesCount = 10;

        //* private *//



        //* editorprefs *//
        private static float GET_FLOAT(string key)
        {
            if(EditorPrefs.HasKey("EModules/_Scenes/AutoSaves/" + key)) return EditorPrefs.GetFloat("EModules/_Scenes/AutoSaves/" + key);
            if(EditorPrefs.HasKey("_Scenes/AutoSaves/" + key)) return EditorPrefs.GetFloat("_Scenes/AutoSaves/" + key);
            return EditorPrefs.GetFloat(key);
        }

        private static void SET_FLOAT(string key, float value)
        {
            EditorPrefs.SetFloat("EModules/_Scenes/AutoSaves/" + key, value);
        }

        private static string GET_STRING(string key)
        {
            if(EditorPrefs.HasKey("EModules/_Scenes/AutoSaves/" + key)) return EditorPrefs.GetString("EModules/_Scenes/AutoSaves/" + key);
            if(EditorPrefs.HasKey("_Scenes/AutoSaves/" + key)) return EditorPrefs.GetString("_Scenes/AutoSaves/" + key);
            return EditorPrefs.GetString(key);
        }

        private static void SET_STRING(string key, string value)
        {
            EditorPrefs.SetString("EModules/_Scenes/AutoSaves/" + key, value);
        }

        private static int GET_INT(string key)
        {
            if(EditorPrefs.HasKey("EModules/_Scenes/AutoSaves/" + key)) return EditorPrefs.GetInt("EModules/_Scenes/AutoSaves/" + key);
            if(EditorPrefs.HasKey("_Scenes/AutoSaves/" + key)) return EditorPrefs.GetInt("_Scenes/AutoSaves/" + key);
            return EditorPrefs.GetInt(key);
        }

        private static void SET_INT(string key, int value)
        {
            EditorPrefs.SetInt("EModules/_Scenes/AutoSaves/" + key, value);
        }

        private static bool GET_BOOL(string key)
        {
            if(EditorPrefs.HasKey("EModules/_Scenes/AutoSaves/" + key)) return EditorPrefs.GetBool("EModules/_Scenes/AutoSaves/" + key);
            if(EditorPrefs.HasKey("_Scenes/AutoSaves/" + key)) return EditorPrefs.GetBool("_Scenes/AutoSaves/" + key);
            return EditorPrefs.GetBool(key);
        }

        private static void SET_BOOL(string key, bool value)
        {
            EditorPrefs.SetBool("EModules/_Scenes/AutoSaves/" + key, value);
        }

        private static bool HAS_KEY(string key)
        {
            if(EditorPrefs.HasKey("EModules/_Scenes/AutoSaves/" + key)) return true;
            return false;
            /* if (EditorPrefs.HasKey( "_Scenes/AutoSaves/" + key )) return true;
             return EditorPrefs.HasKey( key );*/
        }

        //* editorprefs *//



        //* props *//
        private static float lastSave
        {
            get { return GET_FLOAT("nextsave"); }
            set { EditorPrefs.SetFloat("nextsave", value); }

        }

        private static string AutoSaveFolder
        {
            get { return string.IsNullOrEmpty(GET_STRING("Auto-Save Location")) ? "_Scenes/AutoSaves/" : GET_STRING("Auto-Save Location"); }
            set { SET_STRING("Auto-Save Location", value); }
        }

        private static string AutoSaveFileName(UnityEngine.SceneManagement.Scene currentScene)
        {
            if(!System.IO.Directory.Exists(UnityEngine.Application.dataPath + "/" + AutoSaveFolder))
            {
                System.IO.Directory.CreateDirectory(UnityEngine.Application.dataPath + "/" + AutoSaveFolder);
                AssetDatabase.Refresh();
            }
            var currentSceneName = currentScene.name;
            //if (!AssetDatabase.IsValidFolder("Assets/" + _Scenes/AutoSaves/Folder)) AssetDatabase.CreateFolder("Assets", _Scenes/AutoSaves/Folder);
            var files = System.IO.Directory.GetFiles(Application.dataPath + "/" + AutoSaveFolder).Select(f => f.Replace('\\', '/')).Where(f =>
                    f.EndsWith(".unity") && f.Substring(f.LastIndexOf('/') + 1).StartsWith(currentSceneName + "_")).ToArray();
            if(files.Length == 0) return currentSceneName + "_00";

            var times = files.Select(System.IO.File.GetCreationTime).ToList();
            var max = times.Max();
            var ind = times.IndexOf(max);
            var count = 0;
            files = files.Select(n => n.Remove(n.LastIndexOf('.'))).ToArray();
            if(int.TryParse(files[ind].Substring(files[ind].Length - 2), out count))
            {
                count = (count + 1) % m_filesCount;
                return currentSceneName + "_" + count.ToString("D2");
            }
            return currentSceneName + "_00";
        }





        //* INITIALIZATION *//
        static AutoSaveHandler()
        {
            EditorApplication.update -= UpdateCS;
            EditorApplication.update += UpdateCS;

            resetSet();
        }

        private static void resetSet()
        {
            if(!HAS_KEY("enablesave")) SET_INT("enablesave", 1);
            m_enable = GET_INT("enablesave") == 1;

            if(HAS_KEY("auto1"))
            {
                m_filesCount = GET_INT("auto1");
                m_saveInterval = GET_INT("auto2") * 60;
                m_debug = GET_BOOL("auto3");
            }
        }
        //* INITIALIZATION *//






        //* GUI *//
        [PreferenceItem("Auto-Save")]
        public static void OnPreferencesGUI()
        {
            EditorGUILayout.LabelField("Assets/" + AutoSaveFolder + " - Auto-Save Location");
            var R = EditorGUILayout.GetControlRect(GUILayout.Height(30));
            GUI.Box(R, "");
            R.x += 7;
            R.y += 7;
            m_enable = EditorGUI.ToggleLeft(R, "Enable", m_enable);
            GUI.enabled = m_enable;



            m_filesCount = Mathf.Clamp(EditorGUILayout.IntField("Maximum Files Version", m_filesCount), 1, 99);
            m_saveInterval = Mathf.Clamp(EditorGUILayout.IntField("Save Every (Minutes)", (int)(m_saveInterval / 60)), 1, 60) * 60;

            var location = EditorGUILayout.TextField("Location", AutoSaveFolder).Replace('\\', '/');
            if(location.IndexOfAny(System.IO.Path.GetInvalidPathChars()) >= 0) location = AutoSaveFolder;

            m_debug = EditorGUILayout.Toggle("Log", m_debug);

            if(GUI.changed)
            {
                AutoSaveFolder = location;
                SET_INT("enablesave", m_enable ? 1 : 0);
                SET_INT("auto1", m_filesCount);
                SET_INT("auto2", (int)(m_saveInterval / 60));
                SET_BOOL("auto3", m_debug);
                lastSave = (float)EditorApplication.timeSinceStartup;
                resetSet();
            }
            GUI.enabled = true;
        }
        //* GUI *//





        //* UPDATER *//
        public static void UpdateCS()
        {
            if(!m_enable) return;
            if(UnityEngine.Application.isPlaying)
            {
                if(launchTime == null) launchTime = EditorApplication.timeSinceStartup;
                return;
            }

            if(launchTime != null)
            {
                lastSave += (float)(EditorApplication.timeSinceStartup - launchTime.Value);
                launchTime = null;
            }

            if(Mathf.Abs(lastSave - (float)EditorApplication.timeSinceStartup) >= m_saveInterval * 2)
            {
                lastSave = (float)EditorApplication.timeSinceStartup;
                resetSet();
            }

            if(Mathf.Abs(lastSave - (float)EditorApplication.timeSinceStartup) >= m_saveInterval)
            {
                SaveScene();
                EditorApplication.update -= UpdateCS;
                EditorApplication.update += UpdateCS;
            }
        }

        private static void SaveScene()
        {
            if(!System.IO.Directory.Exists(UnityEngine.Application.dataPath + "/" + AutoSaveFolder))
            {
                System.IO.Directory.CreateDirectory(UnityEngine.Application.dataPath + "/" + AutoSaveFolder);
                AssetDatabase.Refresh();
            }

            var relativeSavePath = "Assets/" + AutoSaveFolder + "/";
            var currentScene = EditorSceneManager.GetActiveScene();
            var saveName = AutoSaveFileName(currentScene);
            EditorSceneManager.SaveScene(currentScene, relativeSavePath + saveName + ".unity", true);
            lastSave = (float)EditorApplication.timeSinceStartup;

            if(m_debug)
                Debug.Log("Auto-Save Current Scene: " + relativeSavePath + saveName + ".unity");
        }
        //* UPDATER *//


    }
}
#endif