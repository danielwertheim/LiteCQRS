#--------------------------------------
# Dependencies
#--------------------------------------
require 'albacore'
#--------------------------------------
# Debug
#--------------------------------------
#ENV.each {|key, value| puts "#{key} = #{value}" }
#--------------------------------------
# Environment vars
#--------------------------------------
@env_solutionname = 'LiteCqrs'
@env_projectnameLiteCqrs = 'LiteCqrs'
@env_solutionfolderpath = "../Source"
@env_buildversion = "0.1.0" + (ENV['env_buildnumber'].to_s.empty? ? "" : ".#{ENV['env_buildnumber'].to_s}")
@env_buildconfigname = ENV['env_buildconfigname'].to_s.empty? ? "Release" : ENV['env_buildconfigname'].to_s
@env_buildname = "#{@env_solutionname}-v#{@env_buildversion}-#{@env_buildconfigname}"
@env_buildfolderpath = 'build'
#--------------------------------------
#optional if no remote nuget actions should be performed
@env_nugetPublishApiKey = ENV['env_nugetPublishApiKey']
@env_nugetPublishUrl = ENV['env_nugetPublishUrl']
@env_nugetSourceUrl = ENV['env_nugetSourceUrl']
#--------------------------------------
# Reusable vars
#--------------------------------------
liteCqrsOutputPath = "#{@env_buildfolderpath}/#{@env_projectnameLiteCqrs}"
#--------------------------------------
# Albacore flow controlling tasks
#--------------------------------------
task :ci => [:buildIt, :copyLiteCqrs, :testIt, :zipIt, :packIt, :publishIt]

task :local => [:buildIt, :copyLiteCqrs, :testIt, :zipIt, :packIt]
#--------------------------------------
task :testIt => [:unittests]

task :zipIt => [:zipLiteCqrs]

task :packIt => [:packLiteCqrsNuGet]

task :publishIt => [:publishLiteCqrsNuGet]
#--------------------------------------
# Albacore tasks
#--------------------------------------
assemblyinfo :versionIt do |asm|
	sharedAssemblyInfoPath = "#{@env_solutionfolderpath}/SharedAssemblyInfo.cs"

	asm.input_file = sharedAssemblyInfoPath
	asm.output_file = sharedAssemblyInfoPath
	asm.version = @env_buildversion
	asm.file_version = @env_buildversion  
end

task :ensureCleanBuildFolder do
	FileUtils.rm_rf(@env_buildfolderpath)
	FileUtils.mkdir_p(@env_buildfolderpath)
end

msbuild :buildIt => [:ensureCleanBuildFolder, :versionIt] do |msb|
	msb.properties :configuration => @env_buildconfigname
	msb.targets :Clean, :Build
	msb.solution = "#{@env_solutionfolderpath}/#{@env_solutionname}.sln"
end

task :copyLiteCqrs do
	FileUtils.mkdir_p(liteCqrsOutputPath)
	FileUtils.cp_r(FileList["#{@env_solutionfolderpath}/Projects/#{@env_projectnameLiteCqrs}/bin/#{@env_buildconfigname}/**"], liteCqrsOutputPath)
end

nunit :unittests do |nunit|
	nunit.command = "#{@env_solutionfolderpath}/packages/NUnit.2.5.10.11092/tools/nunit-console.exe"
	nunit.options "/framework=v4.0.30319","/xml=#{@env_buildfolderpath}/NUnit-Report-#{@env_solutionname}-UnitTests.xml"
	nunit.assemblies FileList["#{@env_solutionfolderpath}/Tests/#{@env_solutionname}.**UnitTests/bin/#{@env_buildconfigname}/#{@env_solutionname}.**UnitTests.dll"]
end

zip :zipLiteCqrs do |zip|
	zip.directories_to_zip liteCqrsOutputPath
	zip.output_file = "#{@env_buildname}.zip"
	zip.output_path = @env_buildfolderpath
end

exec :packLiteCqrsNuGet do |cmd|
	cmd.command = "NuGet.exe"
	cmd.parameters = "pack #{@env_projectnameLiteCqrs}.nuspec -version #{@env_buildversion} -basepath #{liteCqrsOutputPath} -outputdirectory #{@env_buildfolderpath}"
end

exec :publishLiteCqrsNuGet do |cmd|
	cmd.command = "NuGet.exe"
	cmd.parameters = "push #{@env_buildfolderpath}/#{@env_projectnameLiteCqrs}.#{@env_buildversion}.nupkg #{@env_nugetPublishApiKey} -src #{@env_nugetPublishUrl}"
end