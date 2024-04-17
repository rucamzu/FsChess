namespace FsChess.Build

open System
open System.Linq.Expressions

open Nuke.Common
open Nuke.Common.Tools.DotNet

type Build() = 
    inherit NukeBuild()

    [<Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")>]
    member this.Configuration : Configuration =
        if Build.IsLocalBuild then Configuration.Debug else Configuration.Release

    member this.Clean : Target = Target(fun target ->
        target
            .Description("Clean existing build outputs")
            .Before(this.Restore)
            .Executes(fun _ -> DotNetTasks.DotNetClean() |> ignore))

    member this.Restore : Target = Target(fun target ->
        target
            .Description("Restore NuGet dependencies")
            .Executes(fun _ -> DotNetTasks.DotNetRestore() |> ignore))

    member this.Build : Target = Target(fun target ->
        target
            .Description("Build all projects for testing")
            .DependsOn(this.Restore)
            .Executes(fun _ -> DotNetTasks.DotNetBuild(
                fun settings ->
                    settings
                        .SetConfiguration(this.Configuration)
                        .SetNoRestore(true)) |> ignore))

    member this.Test : Target = Target(fun target ->
        target
            .Description("Run all tests")
            .DependsOn(this.Build) // Causes error: Sequence contains no matching element (!?)
            .Executes(fun _ ->
                Paths.testsProjects
                |> Seq.iter (fun testProject ->
                    DotNetTasks.DotNetTest(
                    fun settings ->
                        settings
                            .SetProjectFile(testProject)
                            .SetConfiguration(this.Configuration)
                            .SetNoBuild(true)) |> ignore)))

    static member private DefaultTarget(t : Expression<Func<Build, Target>>) = t

    static member public Main() =
        Build.Execute<Build>(
            Build.DefaultTarget(fun build -> build.Clean),
            Build.DefaultTarget(fun build -> build.Test))

module Main =
    [<EntryPoint>]
    let main argv =
        try
            Build.Main()
        with
        | ex ->
            printfn "%A" ex
            -1
