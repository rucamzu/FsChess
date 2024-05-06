/// Functions to describe chess pieces and moves in algebraic notation.
/// See https://en.wikipedia.org/wiki/Algebraic_notation_(chess).
module FsChess.Chess.ChessNotation

open FsChess.Chess

let annotatePiece piece =
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

let annotatePlayedPiece piece atSquare =
    $"{annotatePiece piece}{atSquare.ToString().ToLowerInvariant()}"

let annotateMove = function
    | Move (piece, atSquare, toSquare) -> $"{annotatePiece piece}{toSquare.ToString().ToLowerInvariant()}"
