# Epicycle.Commons-cs 0.1.8.0
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
* Binary & stream utilities *(Epicycle.Commons.Binary)*
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

* **Version 0.1.8.0** [2015-07-13]
  * System.Diagnostics
    * Adding Restart() extension method to Stopwatch in .NET 3.5
  * Epicycle.Commons
    * Adding IUpdatable
  * Epicycle.Commons.Threading
    * Moving all thread related stuff to Epicycle.Commons.Threading
    * Creating more convenient constructors for BasePeriodicThread and PeriodicThread
    * Fixing a nasty bug in BasePeriodicThread that made it go into busy loop

* **Version 0.1.7.0** [2015-07-13]
  * Epicycle.Commons
    * Added StringUtils.Repeat
    * Added StringUtils.EnsureNewLineIfNotEmpty
  * Creating Epicycle.Commons.Binary:
    * BinaryUtils: Various serialization, deserialization and other binary utils.
    * StreamUtils: Utilities and extension methods for System.IO.Stream
    * BinaryReaderUtils: Utilities and extension methods for System.IO.BinaryReader
    * BinaryWriterUtils: Utilities and extension methods for System.IO.BinaryWriter
  * Epicycle.Commons.Reporting
    * SerializableReport:
	  * SimpleReport was renamed into SerializableReport and made public
	  * Fixing a bug with indentation
	  * Can now have a prefix that will be serialized as well 
	* Creating ReportingUtils:
	  * It is now possible to report a System.Diagnostics.Stopwatch
	  * INumericReport.Time was moved into ReportingUtils and renamed into TimeAndReport
	  * WriteReport that writes a SerializableReport to a file
    * Creating dummy implementation for IReport and IStatisticsReporter: DummyReport & DummyStatisticsReporter
	* Better unit test coverage
  * Epicycle.Commons.Time:
    * Creating DateTimeUtcAndLocal that can store both the UTC and Local time of a point in time.
    * Adding more utilities to TimeUtils
    * Creating IDateTimeProvider together with two implementations: SystemDateTimeProvider and ManualDateTimeProvider
	* TimeFormating was moved to Epicycle.Commons.Time
    * Creating DateTimeFormatting with flexible ISO 8601 formatting capabilities
	* Some unit tests
  * Epicycle.Commons.Collections:
    * Adding IFixedCollection, IFixedReadOnlyList and IFixedList for fixed size collections
    * CollectionUtils: AsReadOnlyList<T>(this IEnumerable<T>) was renamed to ToReadOnlyList

* **Version 0.1.6.0** [2015-01-13]
  * Adding a very simple CSV parsing framework
  * Adding StringUtils.ParseString<T>

* **Version 0.1.5.0** [2015-01-12]
  * .NET 3.5 Compatibility:
    * Adding dependency on TaskParallelLibrary
    * Almost full implementation of System.Tuple
    * Almost full implementation of System.Numerics.Complex
  * Epicycle.Commons.Collections:
	* CollectionUtils.AsReadOnlyList can now work with IEnumerable and IList.
    * Better input validation
	* Better test coverage

* **Version 0.1.4.0** [2015-01-08]
  * The emulated System.Collections.ObjectModel.ReadOnlyCollection was replaced by a much thinner ReadOnlyListWrapper
  * FileSystemPath: explicit conversion from string
  * Bringing warnings to zero

* **Version 0.1.3.0** [2015-01-07]
  * Fixing the build for .NET 3.5 and .NET 4.0
  * Creating a dedicated project (Epicycle.Commons.TestUtils_cs) for UT utilities
  * Adding emulated System.Collections.ObjectModel.ReadOnlyCollection for .NET 3.5 and .NET 4.0
  * Adding AsReadOnlyList to CollectionUtils

* **Version 0.1.2.0** [2015-01-07]
  * Upgrading libraries

* **Version 0.1.1.0** [2015-01-06]
  * Adding NuGet dependencies

* **Version 0.1.0.0** [2015-01-06]
  * Initial upload
