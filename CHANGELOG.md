# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.1.2] - 2025-07-08
### Added
- Support for .NET 5, .NET 6, .NET 7, and .NET 8 — now multi‑targeted with no code changes required.
- New test configuration options to optionally **disable test parallelism** during local development.
- Introduced `CHANGELOG.md` using [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).
- Added GitHub Actions workflow to **automatically create Git tags and releases** based on changelog updates.

### Fixed
- Unit tests updated to ensure compatibility with .NET 5.

### Changed
- Internal test configuration refactored to handle parallelism flags in a cleaner, more elegant way.

---

## [0.1.1] - 2025-07-02
### Fixed
- Corrected example in README to reflect accurate usage.
- Fixed incorrect versioning in the `.csproj` file.

### Changed
- Improved documentation and formatting in multiple README commits.
- Updated `CITATION.cff` file.

---

## [0.1.0] - 2025-06-28
### Added
- Initial release of `LCSoft.Results` with:
  - Core `Result`, `Success`, and `Error` abstractions.
  - Fluent methods: `Map`, `Bind`, `Tap`, `Match`.
  - Interface-driven API design.
  - 100% unit test coverage.
- Included GitHub Actions to auto-publish to NuGet.
- Added debug project for testing and experimentation.
- Configured test and CI workflow.