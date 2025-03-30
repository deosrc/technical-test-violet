using Semver;

namespace Deosrc.TechnicalTests.Violet.Versioning;

public class SemanticVersioning : IVersioning
{
	public string IncrementVersion(string currentVersion, ReleaseType releaseType)
	{
		if (!SemVersion.TryParse(currentVersion, out var parsedVersion))
			throw new InvalidDataException($"Version number '{currentVersion}' is not a supported semantic version format.");

		return releaseType switch
		{
			ReleaseType.Patch => parsedVersion.WithPatch(parsedVersion.Patch + 1).ToString(),
			ReleaseType.Minor => parsedVersion.WithMinor(parsedVersion.Minor + 1).ToString(),
			_ => throw new NotSupportedException($"Release type '{releaseType}' is not supported.")
		};
	}
}