require 'albacore'

build_mode = ENV['CONFIGURATION'] || 'Release'

task :default => [:compile, :test]

desc 'Compile all projects'
build :compile do |b|
  b.sln = 'restful-query-filter.sln'
  b.target = ['Clean', 'Rebuild']
  b.prop 'Configuration', build_mode
  b.prop 'VisualStudioVersion', '12.0'
  b.be_quiet
  b.nologo
end

desc 'Run all unit test assemblies'
test_runner :test => [:compile] do |t|
  t.files = FileList["test/**/bin/#{build_mode}/*.Test.dll"]
  t.exe = 'packages/NUnit.Runners.2.6.4/tools/nunit-console.exe'
  t.add_parameter '/config:Release'
  t.add_parameter '/nologo'
  t.add_parameter '/noresult'
  t.add_parameter '/nodots'
end

desc 'Set the assembly version number'
asmver :assembly_info do |a|
  a.file_path = "src/Restful.Query.Filter/Properties/AssemblyInfo.cs"
  a.attributes assembly_title: 'Restful.Query.Filter',
               assembly_description: '',
               assembly_configuration: '',
               assembly_company: '',
               assembly_product: 'Restful.Query.Filter',
               assembly_copyright: "Copyright #{Time.now.year}",
               assembly_trademark: '',
               assembly_culture: '',
               assembly_version: ENV['NUGET_VERSION'],
               assembly_file_version: ENV['NUGET_VERSION']
end

desc 'Restore nuget packages for all projects'
nugets_restore :restore do |p|
  p.out = 'packages'
  p.exe = '.nuget/NuGet.exe'
end

desc 'Build nuget packages'
nugets_pack :package => [:ensure_version, :compile, :test, :assembly_info] do |p|
  package_output = 'bin'

  Dir.mkdir(package_output) unless Dir.exists?(package_output)

  p.configuration = build_mode
  p.files = FileList['src/Restful.Query.Filter/Restful.Query.Filter.csproj']
  p.out = package_output
  p.exe = '.nuget/NuGet.exe'
  p.with_metadata do |m|
    m.title = 'Restful.Query.Filter'
    m.authors = 'Junior Oliveira'
    m.project_url = 'https://github.com/jroliveira/restful-query-filter'
    m.tags = 'rest restful api filter'
    m.version = ENV['NUGET_VERSION']
  end
end

task :ensure_version do
  raise 'Missing env NUGET_VERSION value' unless ENV['NUGET_VERSION']
end
