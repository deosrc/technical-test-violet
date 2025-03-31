using Deosrc.TechnicalTests.Violet.DataStructure;
using Deosrc.TechnicalTests.Violet.Versioning;
using Moq;

namespace Deosrc.TechnicalTests.Violet.Tests.DataStructure;

public class StructuredVersionDataUpdaterTests
{
	private Mock<IVersioning> _versioningMock = new();
	private StructuredVersionDataUpdaterOptions _options = new();
	private StructuredVersionDataUpdater _sut;

	public StructuredVersionDataUpdaterTests()
	{
		_sut = new(_options, _versioningMock.Object);
	}

	[Theory]
	[InlineData(null, "Version")]
	[InlineData("V", "V")]
	public void IncrementVersion_WhenPropertyFound_UpdatesVersion(string? versionPropertyKey, string dataVersionPropertyKey)
	{
		// Arrange
		if (versionPropertyKey is not null)
			_options.VersionPropertyKey = versionPropertyKey;

		var testData = BuildTestData(dataVersionPropertyKey, "1.2.3");

		_versioningMock
			.Setup(x => x.IncrementVersion(It.IsAny<string>(), It.IsAny<ReleaseType>()))
			.Returns("abc123");

		// Act
		var result = _sut.IncrementVersion(testData, ReleaseType.Minor);

		// Assert
		Assert.Equivalent(
			BuildTestData(dataVersionPropertyKey, "abc123"),
			result
		);
		_versioningMock.Verify(x => x.IncrementVersion(It.IsAny<string>(), It.IsAny<ReleaseType>()), Times.Once());
	}

	[Theory]
	[InlineData("1.2.3", ReleaseType.Patch)]
	[InlineData("4.5.6", ReleaseType.Minor)]
	public void IncrementVersion_CorrectlyCallsVersioningService(string currentVersion, ReleaseType releaseType)
	{
		// Arrange
		var testData = BuildTestData("Version", currentVersion);

		_versioningMock
			.Setup(x => x.IncrementVersion(It.IsAny<string>(), It.IsAny<ReleaseType>()))
			.Returns("abc123");

		// Act
		var result = _sut.IncrementVersion(testData, releaseType);

		// Assert
		_versioningMock.Verify(x => x.IncrementVersion(It.IsAny<string>(), It.IsAny<ReleaseType>()), Times.Once());
		_versioningMock.Verify(x => x.IncrementVersion(currentVersion, releaseType), Times.Once());
	}

	[Theory]
	[InlineData(null, "V", "1.2.3")]
	[InlineData("V", "Version", "1.2.3")]
	public void IncrementVersion_WhenPropertyNotFound_ThrowsException(string? versionPropertyKey, string dataVersionPropertyKey, object? dataVersionPropertyValue)
	{
		// Arrange
		if (versionPropertyKey is not null)
			_options.VersionPropertyKey = versionPropertyKey;

		var testData = BuildTestData(dataVersionPropertyKey, dataVersionPropertyValue);

		// Act
		void act()
		{
			_sut.IncrementVersion(testData, ReleaseType.Minor);
		}

		// Assert
		var ex = Assert.Throws<InvalidDataException>(act);
	}

	public static Dictionary<string, object?> BuildTestData(string dataVersionPropertyKey, object? dataVersionPropertyValue)
	{
		return new()
		{
			{ dataVersionPropertyKey, dataVersionPropertyValue },
			{ "OtherStructure", new object[] { 1, 2, 3 } }
		};
	}
}
