public interface IKeypadTarget
{
    int RequiredLength { get; }
    void TryUnlock(int[] input);
}
