#load "tasks/build.cake"
#load "tasks/clean.cake"
#load "tasks/pack.cake"
#load "tasks/restore.cake"
#load "tasks/test.cake"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var solutionPath = $"./../Http.Query.Filter.sln";
var artifactsDirectory = Directory($"./../artifacts");

Task("Default")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Test");

Task("Deploy")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("Pack");

RunTarget(target);