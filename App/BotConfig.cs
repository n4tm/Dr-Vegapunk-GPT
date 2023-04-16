using DrVegapunk.GPT.App.Utils;

namespace DrVegapunk.GPT.App;

public class BotConfig {
    public static BotConfig _ = ConfigManager.TryReadConfigFile<BotConfig>("Bot");

    public char CommandPrefix { get; set; } = '.';

    public char AlternativeCommandPrefix { get; set; } = '!';

    public int IOMaxLength { get; set; } = 2048;

    public bool EnableGPTStreamOutput { get; set; }

    public int MaxUserAttemptsToChatGPT { get; set; } = 32;

    public int MaxUserAttemptsToDallE { get; set; } = 3;
}
