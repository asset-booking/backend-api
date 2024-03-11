namespace Architecture.Tests;

using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;

public class ArchitectureTests
{
    private struct SolutionNamespaces
    {
        public const string SharedKernel = "Asset.Booking.SharedKernel";
        public const string Domain = "Asset.Booking.Domain";
        public const string Application = "Asset.Booking.Application";
        public const string Infrastructure = "Asset.Booking.Infrastructure";
        public const string Presentation = "Asset.Booking.Presentation";
        public const string Api = "Asset.Booking.API";
    }

    [Fact]
    public void SharedKernel_Should_Not_HaveDependenciesOnHighLevelProjects()
    {
        // arrange
        Assembly sharedKernelAssembly = Asset.Booking.SharedKernel.AssemblyReference.Assembly;

        var otherNamespaces = new[]
        {
            SolutionNamespaces.Domain,
            SolutionNamespaces.Application,
            SolutionNamespaces.Infrastructure,
            SolutionNamespaces.Presentation,
            SolutionNamespaces.Api
        };

        // act
        TestResult? testResult = Types
            .InAssembly(sharedKernelAssembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherNamespaces)
            .GetResult();

        // assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Domain_Should_Not_HaveDependenciesOnHighLevelProjects()
    {
        // arrange
        Assembly domainAssembly = Asset.Booking.Domain.AssemblyReference.Assembly;

        var otherNamespaces = new[]
        {
            SolutionNamespaces.Application,
            SolutionNamespaces.Infrastructure,
            SolutionNamespaces.Presentation,
            SolutionNamespaces.Api
        };

        // act
        TestResult? testResult = Types
            .InAssembly(domainAssembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherNamespaces)
            .GetResult();

        // assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_HaveDependenciesOnHighLevelProjects()
    {
        // arrange
        Assembly applicationAssembly = Asset.Booking.Application.AssemblyReference.Assembly;

        var otherNamespaces = new[]
        {
            SolutionNamespaces.Infrastructure,
            SolutionNamespaces.Presentation,
            SolutionNamespaces.Api
        };

        // act
        TestResult? testResult = Types
            .InAssembly(applicationAssembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherNamespaces)
            .GetResult();

        // assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Handlers_Should_HaveDependencyOnDomain()
    {
        // arrange
        Assembly applicationAssembly = Asset.Booking.Application.AssemblyReference.Assembly;

        // act
        TestResult? testResult = Types
            .InAssembly(applicationAssembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .HaveDependencyOn(SolutionNamespaces.Domain)
            .GetResult();

        // assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Not_HaveDependenciesOnHighLevelProjects()
    {
        // arrange
        Assembly infrastructureAssembly = Asset.Booking.Infrastructure.AssemblyReference.Assembly;

        var otherNamespaces = new[]
        {
            SolutionNamespaces.Presentation,
            SolutionNamespaces.Api
        };

        // act
        TestResult? testResult = Types
            .InAssembly(infrastructureAssembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherNamespaces)
            .GetResult();

        // assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Presentation_Should_Not_HaveDependenciesOnHighLevelProjects()
    {
        // arrange
        Assembly presentationAssembly = Asset.Booking.Presentation.AssemblyReference.Assembly;

        var otherNamespaces = new[]
        {
            SolutionNamespaces.Infrastructure,
            SolutionNamespaces.Api
        };

        // act
        TestResult? testResult = Types
            .InAssembly(presentationAssembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherNamespaces)
            .GetResult();

        // assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Presentation_Controllers_Should_HaveDependencyOnMediatR()
    {
        // arrange
        Assembly presentationAssembly = Asset.Booking.Presentation.AssemblyReference.Assembly;

        // act
        TestResult? testResult = Types
            .InAssembly(presentationAssembly)
            .That()
            .HaveNameEndingWith("Controller")
            .Should()
            .HaveDependencyOn("MediatR")
            .GetResult();

        // assert
        testResult.IsSuccessful.Should().BeTrue();
    }
}