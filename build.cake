var target = Argument("target", "Default");

var sourceDir = @".\src";
var projectDir = System.IO.Path.Combine(sourceDir, "BacklogSystemApi", "BacklogSystem");
var project = "BacklogSystem.csproj";

var runSettings = new DotNetCoreRunSettings
{
    WorkingDirectory = projectDir
};

Task("Restore:Api")
    .Does(() =>
{
    DotNetCoreRestore(projectDir);
});

Task("Build:Api")
    .IsDependentOn("Restore:Api")
    .Does(() =>
{
    DotNetCoreBuild(projectDir);
});

Task("Build")
    .IsDependentOn("Build:Api")
    .Does(() =>
{
});

Task("Start:Api")
    .IsDependentOn("Build:Api")
    .Does(() =>
{
	DotNetCoreRun(project,"--args", runSettings);
});

Task("Start")
    .IsDependentOn("Start:Api")
    .Does(() =>
{
});

Task("Default")
    .IsDependentOn("Build")
    .Does(() =>
{
});

RunTarget(target);
