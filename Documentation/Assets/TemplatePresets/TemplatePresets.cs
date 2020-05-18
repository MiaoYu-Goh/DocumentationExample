using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Presets;
using UnityEngine;

public class TemplatePresets
{
    public static void ApplyTemplatePresetsLater() {
        EditorApplication.delayCall += ApplyTemplateSettings;
    }

    [MenuItem("Packages/Apply Template Default Settings")]
    public static void ApplyTemplateSettings()
    {
        var presets = AssetDatabase.FindAssets("t:Preset", new[] {"Assets/TemplatePresets"})
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<Preset>)
            .ToList();

        if (presets.Count <= 0)
        {
            return;
        }

        // this is because ProjectSettingsBase is an internal class
        var type = typeof(UnityEditorInternal.ReorderableList).Assembly.GetType("UnityEditorInternal.ProjectSettingsBase");
        var managers = Resources.FindObjectsOfTypeAll(type);
        foreach (var manager in managers)
        {
            foreach (var preset in presets)
            {
                preset.ApplyTo(manager);
            }
        }
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Packages/Apply Template Default Settings", true)]
    static bool EnableApplyTemplateSettingsMenu()
    {
        var presets = AssetDatabase.FindAssets("t:Preset", new[] {"Assets/TemplatePresets"})
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<Preset>)
            .ToList();
        return presets.Count > 0;
    }

}
