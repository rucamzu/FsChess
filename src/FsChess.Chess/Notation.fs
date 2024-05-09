namespace FsChess.Chess

open FsChess.Chess

/// Functions to describe chess pieces and moves in algebraic notation.
/// See https://en.wikipedia.org/wiki/Algebraic_notation_(chess).
[<RequireQualifiedAccess>]
module Notation =

    type PieceNotation = Piece -> string

    /// Returns the symbol that represents a piece.
    let pieceSymbol piece =
        match (Piece.colour piece, Piece.chessman piece) with
        | White, King -> "♔"
        | White, Queen -> "♕"
        | White, Rook -> "♖"
        | White, Bishop -> "♗"
        | White, Knight -> "♘"
        | White, Pawn -> "♙"
        | Black, King -> "♔"
        | Black, Queen -> "♕"
        | Black, Rook -> "♖"
        | Black, Bishop -> "♗"
        | Black, Knight -> "♘"
        | Black, Pawn -> "♙"

    /// Returns the letter that represents a piece.
    let pieceLetter piece =
        match Piece.chessman piece with
        | King -> "K"
        | Queen -> "Q"
        | Rook -> "R"
        | Bishop -> "B"
        | Knight -> "N"
        | Pawn -> ""

    let annotatePlayedPiece piece atSquare =
        $"{pieceSymbol piece}{atSquare.ToString().ToLowerInvariant()}"

    let annotateMove (annotatePiece : PieceNotation) = function
        | Move (piece, _, toSquare) ->
            match Piece.chessman piece with
            | Pawn -> $"{toSquare.ToString().ToLowerInvariant()}"
            | _ -> $"{annotatePiece piece}{toSquare.ToString().ToLowerInvariant()}"
