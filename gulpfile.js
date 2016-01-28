'use strict';

const
  p = require('./package.json'),
  gulp = require('gulp'),
  assemblyInfo = require('gulp-dotnet-assembly-info'),
  xmlpoke = require('gulp-xmlpoke'),
  msbuild = require('gulp-msbuild'),
  nunit = require('gulp-nunit-runner'),
  nuget = require('nuget-runner')({
    nugetPath: '.nuget/nuget.exe'
  });

gulp.task('default', ['nuget-package']);

gulp.task('nuget-package', ['nuspec'], function () {
  return nuget
    .pack({
      spec: 'Restful.Query.Filter.nuspec',
      outputDirectory: 'bin',
      version: p.version
    });
});

gulp.task('nuspec', ['test'], function () {
  return gulp
    .src('Restful.Query.Filter.nuspec')
    .pipe(xmlpoke({
      replacements: [{
        xpath: "//package:version",
        namespaces: {
          "package": "http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd"
        },
        value: p.version
      }]
    }))
    .pipe(gulp.dest('.'));
});

gulp.task('test', ['build'], function () {
  return gulp
    .src(['test/**/bin/Release/*.Test.dll'], {
      read: false
    })
    .pipe(nunit({
      executable: 'packages/NUnit.Runners.2.6.4/tools/nunit-console.exe',
      options: {
        config: 'Release',
        nologo: true,
        noresult: true,
        nodots: true
      }
    }));
});

gulp.task('build', ['restore'], function () {
  return gulp
    .src('Restful.Query.Filter.sln')
    .pipe(msbuild({
      toolsVersion: 4.0,
      targets: ['Clean', 'Build'],
      errorOnFail: true,
      configuration: 'Release'
    }));
});

gulp.task('restore', ['assemblyInfo'], function () {
  return nuget
    .restore({
      packages: 'Restful.Query.Filter.sln',
      verbosity: 'normal'
    });
});

gulp.task('assemblyInfo', [], function () {
  return gulp
    .src('**/AssemblyInfo.cs')
    .pipe(assemblyInfo({
      version: p.version,
      title: p.name,
      description: p.description,
      company: p.author,
      product: p.name,
      copyright: 'Copyright (C) ' + p.author + ' ' + new Date().getFullYear()
    }))
    .pipe(gulp.dest('.'));
});