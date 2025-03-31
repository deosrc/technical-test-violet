# Technical Test: Violet (Version Incrementer)

This solution was developed for a technical test. The requirement was to develop a CLI application which would increment a version number in [Semantic Versioning Format](https://semver.org/), based on a provided release type.

## Getting Started

There are currently no rep-compiled binaries of the application. Use requires compiling and running the applications yourself.

Due to installation and licensing issues, the solution was developed using Visual Studio Code (VSCode) rather than Visual Studio. While it should function in Visual Studio with minimal effort, it has not been verified. As such, these instructions use Visual Studio Code.

### Requirements

- Visual Studio Code, with the Dev Containers extension installed.
- Docker

### Instructions

1. Clone the repository locally.
2. Open the repository folder in VSCode.
3. When prompted, click the button to "Reopen in Container". Alternatively, press `F1`, and select `Dev Containers: Reopen in Container".

VSCode may take a few minutes to build the container on first run. Once fully loaded, you can execute the CLI by pressing `F5` as normal. CLI arguments can be set in `.vscode/launch.json`. Available arguments will be output to the console when running the application if no arguments or invalid arguments are provided.

Unit tests can be executed from the "Testing" tab.

> :warning: There seems to be a bug where the tests may not display when the container is first built. This can usually resolved by restarting VSCode.

## Design Decisions

- The method used for writing the JSON file will retain all data structures, but may make changes to the file such as adjusting whitespace or removing comments (comments are not valid JSON). This was intentional so that the application can also act to clean the file and keep it consistent. In a real world scenario, questions would be raised to gain a better understanding of the purpose of the file, and make a more informed decision/recommendation.

## Known Issues/Limitations

A number of known issues/limitations exist in this first version:

- The implementation for reading and writing to the filesystem has no tests. Given it uses an external resource (filesystem), it is intended to be covered by end-to-end testing. This could be implemented using xUnit.
- There is currently no dependency injection framework used, with services initialised and injected manually instead.
- CLI arguments are currently positional, and use a simple parsing mechanism. It would be beneficial to convert this to use the configuration components sof .NET so that various providers could be used, in addition to making the CLI arguments named.