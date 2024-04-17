namespace FsChess.Build

open System.ComponentModel
open Nuke.Common.Tooling

[<TypeConverter(typeof<Configuration>)>]
type Configuration() =
    inherit Enumeration()

    static member Debug = Configuration(Value = nameof(Configuration.Debug))
    static member Release = Configuration(Value = nameof(Configuration.Release))

    static member public op_Implicit(configuration : Configuration) =
        configuration.Value
