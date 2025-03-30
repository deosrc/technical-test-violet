namespace Deosrc.TechnicalTests.Violet;

public record VioletOptions
{
	public required string FilePath { get; set; }

	public required ReleaseType ReleaseType { get; set; }
}