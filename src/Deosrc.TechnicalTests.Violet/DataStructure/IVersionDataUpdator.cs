namespace Deosrc.TechnicalTests.Violet.DataStructure;

public interface IVersionDataUpdater<TData>
{
	TData IncrementVersion(TData dataStructure, ReleaseType releaseType);
}
