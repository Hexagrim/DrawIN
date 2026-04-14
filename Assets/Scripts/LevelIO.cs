using SFB;
using System.ComponentModel;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelIO : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject ground;
    public GameObject bounce;
    public GameObject noDraw;
    public GameObject ball;
    public GameObject box;

    // =========================
    // EXPORT LEVEL
    // =========================

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "LoadedLevel")
        {
            RebuildLevel(GameData.loadedLevel);
        }
    }
    public void ExportLevel()
    {
        LevelData level = BuildLevelData();

        string json = JsonUtility.ToJson(level, true);

        var path = StandaloneFileBrowser.SaveFilePanel(
            "Export Level",
            "",
            "level",
            "json"
        );

        if (string.IsNullOrEmpty(path))
            return;

        File.WriteAllText(path, json);

        Debug.Log("Level exported: " + path);
    }

    // 
    // IMPORT LEVEL
    // 
    public void ImportLevel()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel(
            "Import Level",
            "",
            "json",
            false
        );

        if (paths.Length == 0)
            return;

        string json = File.ReadAllText(paths[0]);

        LevelData data = JsonUtility.FromJson<LevelData>(json);
        //RebuildLevel(data);
        GameData.loadedLevel = data;

        Debug.Log("Level loaded!");
    }

    public LevelData BuildLevelData()
    {
        EditObject[] objects = FindObjectsByType<EditObject>(FindObjectsSortMode.None);

        LevelData level = new LevelData();

        foreach (var obj in objects)
        {
            ObjectData data = new ObjectData
            {
                id = obj.id,
                position = obj.transform.position,
                rotation = obj.transform.eulerAngles,
                scale = obj.transform.localScale
            };

            level.objects.Add(data);
        }

        return level;
    }

    void RebuildLevel(LevelData data)
    {
        foreach (var obj in data.objects)
        {
            GameObject prefab = GetPrefab(obj.id);

            if (prefab == null)
            {
                Debug.LogWarning("Unknown ID: " + obj.id);
                continue;
            }

            GameObject instance = Instantiate(prefab);

            instance.transform.position = obj.position;
            instance.transform.eulerAngles = obj.rotation;
            instance.transform.localScale = obj.scale;
        }
        //GameData.loadedLevel = null;
    }
    GameObject GetPrefab(string id)
    {
        switch (id)
        {
            case "ground": return ground;
            case "bounce": return bounce;
            case "noDraw": return noDraw;
            case "ball": return ball;
            case "box": return box;
        }

        return null;
    }

    void ClearScene()
    {
        foreach (var obj in FindObjectsByType<EditObject>(FindObjectsSortMode.None))
        {
            Destroy(obj.gameObject);
        }
    }
}
