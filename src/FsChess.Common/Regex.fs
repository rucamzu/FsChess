namespace FsChess.Common

open System.Text.RegularExpressions

/// Functions on regular expressions.
[<RequireQualifiedAccess>]
module Regex =

    let matches pattern input = Regex.Matches(input, pattern)

/// Functions on regular expression matches.
[<RequireQualifiedAccess>]
module Match =

    let value (match' : Match) = match'.Value