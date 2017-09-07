#addin Cake.SemVer

// Enviroment
var isRunningBitrise = Bitrise.IsRunningOnBitrise;
var isRunningOnWindows = IsRunningOnWindows();

// Arguments.
var target = Argument("target", "Default");
var configuration = "Release";

// Define directories.
var solutionFile = new FilePath("Xamarin.Android.Tooltips.sln");
var artifactsDirectory = new DirectoryPath("artifacts");

// Versioning. Used for all the packages and assemblies for now.
var version = CreateSemVer(1, 0, 6);

Setup((context) =>
{
	Information("AppVeyor: {0}", isRunningBitrise);
	Information ("Running on Windows: {0}", isRunningOnWindows);
	Information("Configuration: {0}", configuration);
});

Task("Clean")
	.Does(() =>
	{	
		CleanDirectory(artifactsDirectory);

		DotNetBuild(solutionFile, settings => settings
				.SetConfiguration(configuration)
				.WithTarget("Clean")
				.SetVerbosity(Verbosity.Minimal));
	});

Task("Restore")
	.Does(() => 
	{
		NuGetRestore(solutionFile);
	});

Task("Build")
	.IsDependentOn("Clean")
	.IsDependentOn("Restore")
	.Does(() =>  
	{	
		DotNetBuild(solutionFile, settings => settings
					.SetConfiguration(configuration)
					.WithTarget("Build")
					.SetVerbosity(Verbosity.Minimal));
	});

Task ("NuGet")
	.IsDependentOn ("Build")
	.WithCriteria(isRunningBitrise)
	.Does (() =>
	{
		Information("Nuget version: {0}", version);
		
		var nugetVersion = Bitrise.Environment.Repository.GitBranch == "master" ? version.ToString() : version.Change(prerelease: "pre" + Bitrise.Environment.Build.BuildNumber).ToString();

		NuGetPack ("./nuspec/Xamarin.Android.Tooltips.nuspec", 
			new NuGetPackSettings 
				{ 
					Version = nugetVersion,
					Verbosity = NuGetVerbosity.Normal,
					OutputDirectory = artifactsDirectory,
					BasePath = "./",
					ArgumentCustomization = args => args.Append("-NoDefaultExcludes")		
				});	
	});

Task("Default")
	.IsDependentOn("NuGet")
	.Does(() => {});

RunTarget(target);