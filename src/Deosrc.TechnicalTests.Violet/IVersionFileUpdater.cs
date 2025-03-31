namespace Deosrc.TechnicalTests.Violet;

public interface IVersionFileUpdater
{
	Task IncrementVersionAsync(string filePath, ReleaseType releaseType, CancellationToken cancellationToken = default);
}
