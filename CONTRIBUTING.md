# Contributing to SmartOrderBy

Thank you for your interest in contributing to SmartOrderBy! This document provides guidelines and information for contributors.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [How Can I Contribute?](#how-can-i-contribute)
- [Development Setup](#development-setup)
- [Coding Standards](#coding-standards)
- [Testing](#testing)
- [Pull Request Process](#pull-request-process)
- [Release Process](#release-process)

## Code of Conduct

This project and everyone participating in it is governed by our Code of Conduct. By participating, you are expected to uphold this code.

## How Can I Contribute?

### Reporting Bugs

- Use the [GitHub issue tracker](https://github.com/byerlikaya/SmartOrderBy/issues)
- Include a clear and descriptive title
- Describe the exact steps to reproduce the problem
- Provide specific examples to demonstrate the steps
- Describe the behavior you observed after following the steps
- Explain which behavior you expected to see instead and why
- Include details about your configuration and environment

### Suggesting Enhancements

- Use the [GitHub issue tracker](https://github.com/byerlikaya/SmartOrderBy/issues)
- Provide a clear and descriptive title
- Describe the current behavior and explain which behavior you expected to see instead
- Explain why this enhancement would be useful to most SmartOrderBy users

### Pull Requests

- Fork the repository
- Create a feature branch (`git checkout -b feature/amazing-feature`)
- Make your changes
- Add tests for new functionality
- Ensure all tests pass
- Commit your changes (`git commit -m 'Add amazing feature'`)
- Push to the branch (`git push origin feature/amazing-feature`)
- Open a Pull Request

## Development Setup

### Prerequisites

- .NET 9.0 SDK or later
- Visual Studio 2022, VS Code, or Rider
- Git

### Getting Started

1. **Fork and Clone**
   ```bash
   git clone https://github.com/YOUR_USERNAME/SmartOrderBy.git
   cd SmartOrderBy
   ```

2. **Restore Dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the Project**
   ```bash
   dotnet build
   ```

4. **Run Tests**
   ```bash
   dotnet test
   ```

## Coding Standards

### C# Coding Standards

- Follow [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use meaningful variable and method names
- Add XML documentation for public APIs
- Keep methods small and focused
- Use SOLID principles
- Follow DRY (Don't Repeat Yourself) principle

### Code Style

- Use 4 spaces for indentation
- Use PascalCase for public members
- Use camelCase for private members and parameters
- Use meaningful names that describe the purpose
- Add comments for complex logic

### Example

```csharp
/// <summary>
/// Sorts the queryable collection based on the specified sorting criteria.
/// </summary>
/// <typeparam name="T">The type of elements in the collection.</typeparam>
/// <param name="queryable">The queryable collection to sort.</param>
/// <param name="sorting">The sorting criteria to apply.</param>
/// <returns>A sorted queryable collection.</returns>
public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, Sorting sorting)
{
    if (sorting == null || string.IsNullOrEmpty(sorting.Name))
        return queryable;

    // Implementation details...
}
```

## Testing

### Test Requirements

- All new functionality must have corresponding tests
- Tests should cover both positive and negative scenarios
- Use descriptive test names that explain what is being tested
- Follow the Arrange-Act-Assert pattern
- Ensure tests are independent and can run in any order

### Running Tests

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test test/SmartOrderBy.Test/

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Test Naming Convention

```csharp
[Test]
public void OrderBy_WithValidSorting_ReturnsSortedCollection()
{
    // Test implementation
}

[Test]
public void OrderBy_WithNullSorting_ReturnsOriginalCollection()
{
    // Test implementation
}
```

## Pull Request Process

### Before Submitting

1. **Ensure Code Quality**
   - All tests pass
   - Code follows coding standards
   - No compiler warnings
   - Code coverage is maintained or improved

2. **Update Documentation**
   - Update README.md if needed
   - Add XML documentation for new public APIs
   - Update sample code if applicable

3. **Commit Message Format**
   ```
   type(scope): description
   
   [optional body]
   [optional footer]
   ```
   
   Examples:
   ```
   feat(core): add support for custom sorting comparers
   fix(mapper): resolve null reference exception in nested properties
   docs(readme): update installation instructions
   ```

### Pull Request Template

When creating a pull request, please use the provided template and include:

- Clear description of changes
- Related issue number (if applicable)
- Testing instructions
- Screenshots (if UI changes)
- Breaking changes (if any)

## Release Process

### Versioning

We use [Semantic Versioning](https://semver.org/) for version numbers:

- **MAJOR**: Incompatible API changes
- **MINOR**: New functionality in a backward-compatible manner
- **PATCH**: Backward-compatible bug fixes

### Release Steps

1. **Update Version**
   - Update version in project file
   - Update CHANGELOG.md
   - Commit with message: `[release] v1.2.3`

2. **Automated Process**
   - CI/CD pipeline automatically:
     - Builds the project
     - Runs tests
     - Creates NuGet package
     - Publishes to GitHub Packages
     - Creates GitHub release
     - Publishes to NuGet.org

3. **Manual Verification**
   - Verify NuGet package contents
   - Test the package in a sample project
   - Update documentation if needed

## Code Review Process

### Review Guidelines

- Be constructive and respectful
- Focus on code quality and functionality
- Provide specific feedback and suggestions
- Consider security implications
- Ensure performance considerations

### Review Checklist

- [ ] Code follows coding standards
- [ ] Tests are comprehensive
- [ ] Documentation is updated
- [ ] No security vulnerabilities
- [ ] Performance is acceptable
- [ ] Error handling is appropriate

## Getting Help

If you need help with contributing:

- Check existing issues and discussions
- Ask questions in GitHub Discussions
- Contact maintainers through GitHub
- Review the project documentation

## Recognition

Contributors will be recognized in:

- GitHub contributors list
- Release notes
- Project documentation
- README.md contributors section

Thank you for contributing to SmartOrderBy! 🚀
