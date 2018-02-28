# SimpleAppDomainHotreloading
Demo program to show hotreloading of .NET assemblies.

In the code you'll see an IMod interface that gets wrapped in a MarshalByRefObject class when types in another AppDomain inherit from IMod.
