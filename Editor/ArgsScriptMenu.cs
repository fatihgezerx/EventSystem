#if UNITY_EDITOR
using UnityEditor;

namespace EventSystem
{
    /// <summary>
    /// Adds <c>Assets/Create/Event System/Args Script</c> to the Project window's Create menu,
    /// generating a new standalone event-data class from <see cref="TemplatePath"/> - the same way
    /// Unity's own "Create &gt; C# Script" works, just pre-filled with the Args boilerplate (a public
    /// field plus a matching constructor) instead of an empty MonoBehaviour. Use it for project-specific
    /// event data that doesn't fit the built-in <c>IntArgs</c>/<c>BoolArgs</c>/... types, e.g. an
    /// <c>EnemyArgs</c> or <c>PanelArgs</c>.
    /// </summary>
    internal static class ArgsScriptMenu
    {
        private const string TemplatePath = "Assets/Scripts/EventSystem/Editor/EventArgsTemplate.cs.txt";

        [MenuItem("Assets/Create/Event System/Args Script", false, 50)]
        private static void CreateArgsScript()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(TemplatePath, "NewArgs.cs");
        }
    }
}
#endif
