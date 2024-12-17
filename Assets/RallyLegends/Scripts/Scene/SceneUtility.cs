using System;

namespace RallyLegends.Scene
{
    public static class SceneUtility
    {
        public static string GetSceneNameByBuildIndex(int buildIndex) => 
            GetSceneNameFromScenePath(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(buildIndex));

        private static string GetSceneNameFromScenePath(string scenePath)
        {
            var sceneNameStart = scenePath.LastIndexOf("/", StringComparison.Ordinal) + 1;
            var sceneNameEnd = scenePath.LastIndexOf(".", StringComparison.Ordinal);
            var sceneNameLength = sceneNameEnd - sceneNameStart;

            return scenePath.Substring(sceneNameStart, sceneNameLength);
        }
    }
}