namespace FS24StartHub.IntegrationTests.Helpers
{
    public static class TestPathHelper
    {
        /// <summary>
        /// Finds the nearest parent folder with the given name.
        /// </summary>
        public static string FindParentFolder(string startPath, string folderName)
        {
            var dir = new DirectoryInfo(startPath);
            while (dir != null)
            {
                if (dir.Name.Equals(folderName, StringComparison.OrdinalIgnoreCase))
                    return dir.FullName;

                dir = dir.Parent;
            }

            throw new DirectoryNotFoundException(
                $"Could not find folder '{folderName}' starting from {startPath}");
        }

        /// <summary>
        /// Returns the external test data root (e.g. FS24StartHub_TestData),
        /// located next to the solution folder.
        /// </summary>
        public static string GetExternalTestDataRoot(string solutionFolderName = "FS24StartHub")
        {
            var baseDir = AppContext.BaseDirectory;
            var solutionRoot = FindParentFolder(baseDir, solutionFolderName);
            var projectsRoot = Directory.GetParent(solutionRoot)!.FullName;

            return Path.Combine(projectsRoot, solutionFolderName + "_TestData");
        }
    }
}