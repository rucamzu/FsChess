[<RequireQualifiedAccess>]
module FsChess.Common.Uri

open System

/// Returns the path relative to the base part on an absolute URI.
let localPath (uri : Uri) = uri.LocalPath
