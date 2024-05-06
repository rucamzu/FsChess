namespace FsChess.Chess

/// A chess board.
type Board = private Board of Map<Square, Piece>

/// Functions to query and manipulate chess boards.
[<RequireQualifiedAccess>]
module Board =

    /// An empty board.
    let empty = Board Map.empty

    /// Places a piece on a square of a given board. 
    let place piece atSquare = function
        | Board pieces -> pieces |> (Map.add atSquare piece) |> Board

    /// Places a piece on many squares of a given board. 
    let placeMany piece squares board =
        squares
        |> Seq.fold (fun board' square-> place piece square board') board

    /// Removes any piece on a square of a given board. 
    let remove square = function
        | Board pieces -> pieces |> Map.remove square |> Board

    /// Returns the piece placed on a square of a given board.
    let getAt square = function
        | Board pieces -> pieces |> Map.tryFind square |> Option.get

    /// Returns a list of all occupied squares together with the pieces placed in them.
    let getAll = function
        | Board pieces ->
            pieces
            |> Map.toSeq
            |> Seq.sortBy (fun (square, _) -> square)
