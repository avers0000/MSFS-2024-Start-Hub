# MSFS 2024 Start Hub — Changelog
All notable changes to this project will be documented in this file.

The format is based on Keep a Changelog, and this project adheres to Semantic Versioning.

## [0.7.3]
### Added
- Process check for startup items: optional skip and/or warning if a process is already running before launch.
- New fields in startup item configuration: Process Name, Skip If Running, Warn If Running.
- Pre-launch warning dialog listing all already-running processes marked for warning.
- Process Name is automatically pre-filled from the executable file name when browsing for an `.exe` file.

## [0.7.2]
### Added
- Settings form for viewing and managing simulator configuration.
- Ability to re-run simulator auto-detection without restarting the application.
- Manual path configuration (Custom mode) for non-standard simulator installations.

### Fixed
- Delay Before / Delay After labels now correctly show seconds instead of milliseconds.

### Changed
- Upgraded to .NET 10.

## [0.7.1]
### Fixed
- Improved detection of Microsoft Flight Simulator 2024 (Store version) to prevent incorrect identification when multiple simulator versions are installed.

## [0.7.0]
### Added
- Initial public release of MSFS 2024 Start Hub.
- Automatic launch of Microsoft Flight Simulator 2024.
- Launching of external applications and scripts (EXE, COM, BAT, CMD, PS1).
- Startup item management: add, edit, remove, enable/disable.
- Launch order control with before/after MSFS execution.
- Optional “Keep open” mode.
- Clean and lightweight user interface.