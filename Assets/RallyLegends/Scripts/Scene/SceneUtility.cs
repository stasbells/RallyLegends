using System;

namespace RallyLegends.Scene
{
    public static class SceneUtility
    {
        public static string GetSceneNameByBuildIndex(int buildIndex) => 
            GetSceneNameFromScenePath(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(buildIndex));

        private static string GetSceneNameFromScenePath(string scenePath)
        {
            int sceneNameStart = scenePath.LastIndexOf("/", StringComparison.Ordinal) + 1;
            int sceneNameEnd = scenePath.LastIndexOf(".", StringComparison.Ordinal);
            int sceneNameLength = sceneNameEnd - sceneNameStart;

            return scenePath.Substring(sceneNameStart, sceneNameLength);
        }
    }
}