using Deosrc.TechnicalTests.Violet.Versioning;

namespace Deosrc.TechnicalTests.Violet.Tests.Versioning;

public class SemanticVersioningTests
{
	private SemanticVersioning _sut = new();

    [Theory]
	[InlineData("0.0.0", ReleaseType.Patch, "0.0.1")]
	[InlineData("0.0.1", ReleaseType.Patch, "0.0.2")]
	[InlineData("0.0.9", ReleaseType.Patch, "0.0.10")]
	[InlineData("0.0.99", ReleaseType.Patch, "0.0.100")]
	[InlineData("0.1.5", ReleaseType.Patch, "0.1.6")]
	[InlineData("1.0.5", ReleaseType.Patch, "1.0.6")]
	[InlineData("1.0.0-rc.1", ReleaseType.Patch, "1.0.1-rc.1")]
	[InlineData("1.0.0+build.metadata", ReleaseType.Patch, "1.0.1+build.metadata")]
	[InlineData("1.0.0-rc.1+build.metadata", ReleaseType.Patch, "1.0.1-rc.1+build.metadata")]
	[InlineData("0.0.0", ReleaseType.Minor, "0.1.0")]
	[InlineData("0.1.0", ReleaseType.Minor, "0.2.0")]
	[InlineData("0.9.0", ReleaseType.Minor, "0.10.0")]
	[InlineData("0.99.0", ReleaseType.Minor, "0.100.0")]
	[InlineData("0.5.0", ReleaseType.Minor, "0.6.0")]
	[InlineData("1.5.0", ReleaseType.Minor, "1.6.0")]
	[InlineData("1.0.0-rc.1", ReleaseType.Minor, "1.1.0-rc.1")]
	[InlineData("1.0.0+build.metadata", ReleaseType.Minor, "1.1.0+build.metadata")]
	[InlineData("1.0.0-rc.1+build.metadata", ReleaseType.Minor, "1.1.0-rc.1+build.metadata")]
    public void IncrementVersion_WhenValidArguments_ReturnsExpectedVersion(string versionNumber, ReleaseType releaseType, string expectedVersion)
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
		Assert.Equal($"Version number '{versionNumber}' in not a valid semantic versioning format.", ex.Message);
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
