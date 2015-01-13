# Epicycle.Commons-cs 0.1.6.0
Epicycle .NET Commons Library.

***Note***: *This library is in it's 0.X version, that means that it's still in active development and backward compatibility is not guaranteed!*

## Links
* NuGet package: https://www.nuget.org/packages/Epicycle.Commons-cs
* Git repository: https://github.com/open-epicycle/Epicycle.Commons-cs
* All Epicycle Git repositories: https://github.com/open-epicycle

## Main features
* File system abstraction *(Epicycle.Commons.FileSystem)*
* A simple framework for objects that are based on the file system *(Epicycle.Commons.FileSystemBasedObjects)*
* Hierarchical reporting infrastructure *(Epicycle.Commons.Reporting)*
* Unsafe buffers *(Epicycle.Commons.Unsafe)*
* Various collection utilities *(Epicycle.Commons.Collections)*
* Time related utilities *(Epicycle.Commons.Time)*
* CSV parsing framework *(Epicycle.Commons.Csv)*
* Various utilities and extensions (e.g. assertions, math, strings, formatting, threading, YAML)
* Specific **.NET 4.5** classes emulation for older frameworks (**.NET 3.5** and **.NET 4.0**)
* Supported frameworks: **.NET 3.5**, **.NET 4.0**, **.NET 4.5**

## License and Copyright
Apache License 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
Copyright 2015 Epicycle (http://epicycle.org)

## Release Notes
### Version 0.1 

* **Version 0.1.6** [2015-01-13]
  * Adding a very simple CSV parsing framework
  * Adding StringUtils.ParseString<T>

* **Version 0.1.5** [2015-01-12]
  * .NET 3.5 Compatibility:
    * Adding dependency on TaskParallelLibrary
    * Almost full implementation of System.Tuple
    * Almost full implementation of System.Numerics.Complex
  * Epicycle.Commons.Collections:
	* CollectionUtils.AsReadOnlyList can now work with IEnumerable and IList.
    * Better input validation
	* Better test coverage

* **Version 0.1.4** [2015-01-08]
  * The emulated System.Collections.ObjectModel.ReadOnlyCollection was replaced by a much thinner ReadOnlyListWrapper
  * FileSystemPath: explicit conversion from string
  * Bringing warnings to zero

* **Version 0.1.3** [2015-01-07]
  * Fixing the build for .NET 3.5 and .NET 4.0
  * Creating a dedicated project (Epicycle.Commons.TestUtils_cs) for UT utilities
  * Adding emulated System.Collections.ObjectModel.ReadOnlyCollection for .NET 3.5 and .NET 4.0
  * Adding AsReadOnlyList to CollectionUtils

* **Version 0.1.2** [2015-01-07]
  * Upgrading libraries

* **Version 0.1.1** [2015-01-06]
  * Adding NuGet dependencies

* **Version 0.1.0** [2015-01-06]
  * Initial upload
