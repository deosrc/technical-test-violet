using Deosrc.TechnicalTests.Violet.Versioning;

namespace Deosrc.TechnicalTests.Violet.Tests.Versioning;

public class SemanticVersioningTests
{
	private SemanticVersioning _sut = new();

	[Theory]
	[InlineData(ReleaseType.Patch, "0.0.0", "0.0.1")]
	[InlineData(ReleaseType.Patch, "0.0.1", "0.0.2")]
	[InlineData(ReleaseType.Patch, "0.0.9", "0.0.10")]
	[InlineData(ReleaseType.Patch, "0.0.99", "0.0.100")]
	[InlineData(ReleaseType.Patch, "0.1.5", "0.1.6")]
	[InlineData(ReleaseType.Patch, "1.0.5", "1.0.6")]
	[InlineData(ReleaseType.Patch, "1.0.0-rc.1", "1.0.1-rc.1")]
	[InlineData(ReleaseType.Patch, "1.0.0+build.metadata", "1.0.1+build.metadata")]
	[InlineData(ReleaseType.Patch, "1.0.0-rc.1+build.metadata", "1.0.1-rc.1+build.metadata")]
	[InlineData(ReleaseType.Minor, "0.0.0", "0.1.0")]
	[InlineData(ReleaseType.Minor, "0.1.0", "0.2.0")]
	[InlineData(ReleaseType.Minor, "0.9.0", "0.10.0")]
	[InlineData(ReleaseType.Minor, "0.99.0", "0.100.0")]
	[InlineData(ReleaseType.Minor, "0.5.0", "0.6.0")]
	[InlineData(ReleaseType.Minor, "1.5.0", "1.6.0")]
	[InlineData(ReleaseType.Minor, "1.0.0-rc.1", "1.1.0-rc.1")]
	[InlineData(ReleaseType.Minor, "1.0.0+build.metadata", "1.1.0+build.metadata")]
	[InlineData(ReleaseType.Minor, "1.0.0-rc.1+build.metadata", "1.1.0-rc.1+build.metadata")]
	public void IncrementVersion_WhenValidArguments_ReturnsExpectedVersion(ReleaseType releaseType, string versionNumber, string expectedVersion)
	{
		// Act
		var result = _sut.IncrementVersion(versionNumber, releaseType);

		// Assert
		Assert.Equal(expectedVersion, result);
	}

	[Theory]
	[InlineData("")]
	[InlineData("abc")]
	[InlineData("0.0.0.0.0")]
	public void IncrementVersion_WhenVersionNumberInvalid_ThrowsException(string versionNumber)
	{
		void act()
		{
			_sut.IncrementVersion(versionNumber, ReleaseType.Patch);
		}

		var ex = Assert.Throws<InvalidDataException>(act);
		Assert.Equal($"Version number '{versionNumber}' is not a supported semantic version format.", ex.Message);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(3)]
	public void IncrementVersion_WhenReleaseTypeInvalid_ThrowsException(int releaseType)
	{
		void act()
		{
			_sut.IncrementVersion("0.0.1", (ReleaseType)releaseType);
		}

		var ex = Assert.Throws<NotSupportedException>(act);
		Assert.Equal($"Release type '{releaseType}' is not supported.", ex.Message);
	}
}
