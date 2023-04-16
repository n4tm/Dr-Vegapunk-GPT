namespace DrVegapunk.GPT.App.Utils;

// Simple implementation
public static class ProjectContext {
    public static readonly string DirectoryPath =
        Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!
                    .Parent!.Parent!.Parent!
                    .FullName;

    public static readonly string ConfigDirPath = $"{DirectoryPath}\\Config";

    public static readonly string ChatActorsDirPath = $"{DirectoryPath}\\ChatActors";

    public static readonly string KnowledgesDirPath = $"{ChatActorsDirPath}\\Knowledges";
}
