namespace Deosrc.TechnicalTests.Violet.Cli;

public record VioletOptions
{
	/// <summary>
	/// The path to the version file to update.
	/// </summary>
	public required string FilePath { get; set; }

	/// <summary>
	/// The release type for updating the version number.
	/// </summary>
	public required ReleaseType ReleaseType { get; set; }
}