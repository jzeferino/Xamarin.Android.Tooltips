#addin Cake.SemVer

// Enviroment
var isRunningOnAppVeyor = AppVeyor.IsRunningOnAppVeyor;
var isRunningOnWindows = IsRunningOnWindows();

// Arguments.
var target = Argument("target", "Default");
var configuration = "Release";

// Define directories.
var solutionFile = new FilePath("Xamarin.Android.Tooltips.sln");
var artifactsDirectory = new DirectoryPath("artifacts");

// Versioning. Used for all the packages and assemblies for now.
var version = CreateSemVer(1, 0, 7);

Setup((context) =>
{
	Information("AppVeyor: {0}", isRunningOnAppVeyor);
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
	.WithCriteria(isRunningOnAppVeyor)
	.Does (() =>
	{
		Information("Nuget version: {0}", version);
		
		AppVeyor.UpdateBuildVersion(string.Format("{0}-{1}-build{2}", version.ToString(), AppVeyor.Environment.Repository.Branch, AppVeyor.Environment.Build.Number));
  		var nugetVersion = AppVeyor.Environment.Repository.Branch == "master" ? version.ToString() : version.Change(prerelease: "pre" + AppVeyor.Environment.Build.Number).ToString();

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