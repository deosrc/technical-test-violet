using Semver;

namespace Deosrc.TechnicalTests.Violet.Versioning;

public class SemanticVersioning : IVersioning
{
	public string IncrementVersion(string currentVersion, ReleaseType releaseType)
	{
		if (!SemVersion.TryParse(currentVersion, out var parsedVersion))
			throw new InvalidDataException($"Version number '{currentVersion}' is not a supported semantic version format.");

		throw new NotImplementedException();
	}
}