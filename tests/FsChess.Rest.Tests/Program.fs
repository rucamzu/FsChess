open System.Reflection

open Expecto
open Hopac
open Logary
open Logary.Configuration
open Logary.Adapters.Facade
open Logary.Targets

[<EntryPoint>]
let main argv =
    "localhost"
    |> Config.create (Assembly.GetExecutingAssembly().GetName().Name)
    |> Config.targets [ LiterateConsole.create LiterateConsole.empty "console" ]
    |> Config.processing (Events.events |> Events.sink [ "console" ])
    |> Config.build
    |> run
    |> LogaryFacadeAdapter.initialise<Logging.Logger>

    runTestsInAssemblyWithCLIArgs [] argv