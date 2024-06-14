module FsChess.Rest.Giraffe

open FsChess.Common

open Giraffe

/// Filters an incoming HTTP request based on the request path (case sensitive) split by path separators.
let routexpn subpath routeHandler : HttpHandler =
    routexp ".*" <| fun singleElementSeq ->
        singleElementSeq
        |> Seq.head
        |> Regex.matches subpath
        |> Seq.map Match.value
        |> routeHandler
