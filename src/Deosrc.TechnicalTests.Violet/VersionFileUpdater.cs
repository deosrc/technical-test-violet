
using System.Text.Json;
using Deosrc.TechnicalTests.Violet.DataStructure;

namespace Deosrc.TechnicalTests.Violet;

public class VersionFileUpdater(IVersionDataUpdater<IDictionary<string, object?>> dataUpdater) : IVersionFileUpdater
{
	public async Task IncrementVersionAsync(string filePath, ReleaseType releaseType, CancellationToken cancellationToken = default)
	{
		// Ensure file exists before starting any processing.
		if (!File.Exists(filePath))
			throw new FileNotFoundException($"File {filePath} could not be found.");

		// Read the file to a structured object.
		Dictionary<string, object?>? contents;
		try
		{
			using var file = File.Open(filePath, FileMode.Open);
			contents = await JsonSerializer.DeserializeAsync<Dictionary<string, object?>?>(file, cancellationToken: cancellationToken).ConfigureAwait(false)
				?? throw new InvalidDataException("Failed to read JSON.");
		}
		catch (JsonException ex)
		{
			throw new InvalidDataException("Invalid or unexpected JSON format.", ex);
		}

		// Update the data
		dataUpdater.IncrementVersion(contents, releaseType);

		// Write the data back to the file
		using (var file = File.Open(filePath, FileMode.Create))
		{
			await JsonSerializer.SerializeAsync(file, contents, new JsonSerializerOptions() {
				WriteIndented = true
			}, cancellationToken).ConfigureAwait(false);
		}
	}
}
