# LiteCQRS
LiteCQRS is a small convention-based CQRS framework that differs from others by NOT using interfaces or base-classes or attributes for defining command- and event handlers. Instead, it relies on method signatures and the namespace of the class, containing the method.

## License
License: [The MIT License (MIT)](http://www.opensource.org/licenses/mit-license.php)

## SemVer for versioning
LiteCQRS uses [SemVer](http://semver.org) for versioning.

## Contribute
Line feeds setting should be `core.autocrlf=false`.

Unit-tests are written using `NUnit` and integration tests ans specifications are written using [Machine Specifications](https://github.com/machine/machine.specifications).

Pull request should be against the **Develop branch**