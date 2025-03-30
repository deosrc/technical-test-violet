namespace Deosrc.TechnicalTests.Violet.Versioning;

public interface IVersioning
{
	string IncrementVersion(string currentVersion, ReleaseType releaseType);
}