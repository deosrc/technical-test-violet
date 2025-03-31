using Deosrc.TechnicalTests.Violet.Versioning;

namespace Deosrc.TechnicalTests.Violet.DataStructure;

/// <summary>
/// Updates version data in a structured data format.
/// </summary>
public class StructuredVersionDataUpdater(StructuredVersionDataUpdaterOptions options, IVersioning versioning) : IVersionDataUpdater<IDictionary<string, object?>>
{
	public IDictionary<string, object?> IncrementVersion(IDictionary<string, object?> dataStructure, ReleaseType releaseType)
	{
		// Get the version property
		if (!dataStructure.TryGetValue(options.VersionPropertyKey, out var version))
			throw new InvalidDataException($"Failed to read '{options.VersionPropertyKey}' property.");

		// Update the version number
		dataStructure["Version"] = versioning.IncrementVersion(version.ToString() ?? string.Empty, releaseType);

		return dataStructure;
	}
}
