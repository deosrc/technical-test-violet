using System.Diagnostics.CodeAnalysis;
using Deosrc.TechnicalTests.Violet.DataStructure;
using Deosrc.TechnicalTests.Violet.Versioning;

namespace Deosrc.TechnicalTests.Violet.Cli;

class Program
{
	static async Task Main(string[] args)
	{
		// Read and validate arguments.
		Console.WriteLine("Violet (Version Incrementer)");
			Console.WriteLine();

		if (!TryParseArgs(args, out var options))
		{
			Console.Error.WriteLine("Invalid arguments.");

			var releaseTypes = Enum.GetValues<ReleaseType>().Select(x => x.ToString());

			Console.WriteLine();
			Console.WriteLine("Arguments:");
			Console.WriteLine("    FILE_PATH RELEASE_TYPE");
			Console.WriteLine();
			Console.WriteLine("    FILE_PATH - The path to the version file to update.");
			Console.WriteLine($"    RELEASE_TYPE - The type of release to update the version number for. Valid options: {string.Join(", ", releaseTypes)}");
			Environment.Exit(1);
			return;
		}

		// Prepare services
		var versioning = new SemanticVersioning();
		var dataUpdater = new StructuredVersionDataUpdater(new(), versioning);
		var versionFileUpdater = new VersionFileUpdater(dataUpdater);

		// Run violet
		await versionFileUpdater.IncrementVersionAsync(options.FilePath, options.ReleaseType);
	}

	private static bool TryParseArgs(string[] args, [NotNullWhen(true)] out VioletOptions? options)
	{
		options = null;

		if (args.Length < 2)
			return false;

		if (!Enum.TryParse(args[1], true, out ReleaseType releaseType))
			return false;

		options = new()
		{
			FilePath = args[0],
			ReleaseType = releaseType
		};
		return true;
	}
}
