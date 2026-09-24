using System.IO;
using UnityEditor;
using UnityEditor.Compilation;

namespace EventSystem
{
    /// <summary>
    /// Adds <c>Assets/Create/Event System/Event Script</c> to the Project window's Create menu: a new
    /// <c>readonly struct</c> event with one field and a matching constructor, ready to raise with
    /// <c>EventManager.Invoke(new MyEvent(...))</c>.
    /// </summary>
    internal static class EventScriptMenu
    {
        private const string TemplateFileName = "EventTemplate.cs.txt";

        [MenuItem("Assets/Create/Event System/Event Script", false, 50)]
        private static void CreateEventScript()
        {
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(TemplatePath(), "NewEvent.cs");
        }

        // The template sits next to this assembly's asmdef, wherever the package was installed.
        private static string TemplatePath()
        {
            var asmdef = CompilationPipeline.GetAssemblyDefinitionFilePathFromAssemblyName("EventSystem.Editor");
            return Path.GetDirectoryName(asmdef)!.Replace('\\', '/') + "/" + TemplateFileName;
        }
    }
}
