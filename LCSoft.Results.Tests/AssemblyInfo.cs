using Xunit;

#if NET5_0 || NET7_0
[assembly: CollectionBehavior(DisableTestParallelization = true)]
#else
[assembly: CollectionBehavior(DisableTestParallelization = false)]
#endif