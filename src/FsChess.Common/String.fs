[<RequireQualifiedAccess>]
module FsChess.Common.String

open System

/// Functional wrapper around the System.String.Join method.
let join (separator : string) (strings : string seq) =
    System.String.Join(separator, strings)

/// Functional wrapper around the System.String.Split method.
let split (separator : string) (s : string) =
    s.Split(separator)
