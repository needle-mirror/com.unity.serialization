# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [0.3.0-preview] - 2019-08-28
### Changed
* Updated `com.unity.properties` to version `0.7.1-preview`
* Support JSON serialization of enums using [numeric integral types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types) as underlying type

## [0.2.1-preview] - 2019-08-08
### Changed
* Support for Unity 2019.1

## [0.2.0-preview] - 2019-08-06

### Changed
* `JsonVisitor` virtual method `GetTypeInfo` now provides the property, container and value parameters to help with type resolution.

## [0.1.0-preview] - 2019-07-22

* This is the first release of *Unity.Serialization*.
