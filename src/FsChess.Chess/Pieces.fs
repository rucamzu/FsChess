namespace FsChess.Chess

open FsChess.Common.Functions

/// Piece shapes in a game of chess.
type Chessman = Pawn | Knight | Bishop | Rook | Queen | King

/// Piece colours in a game of chess.
type Colour = White | Black

/// Coloured pieces in a game of chess.
type Piece = private Piece of Colour * Chessman
    with override this.ToString() = match this with Piece (colour, chessman) -> $"{colour} {chessman}"

/// All the different pieces used in a game of chess.
[<RequireQualifiedAccess>]
module Pieces =

    let private make = curry Piece

    let WhiteKing = make White King
    let WhiteQueen = make White Queen
    let WhiteRook = make White Rook
    let WhiteBishop = make White Bishop
    let WhiteKnight = make White Knight
    let WhitePawn = make White Pawn
    let BlackKing = make Black King
    let BlackQueen = make Black Queen
    let BlackRook = make Black Rook
    let BlackBishop = make Black Bishop
    let BlackKnight = make Black Knight
    let BlackPawn = make Black Pawn

/// Functions to query chess pieces.
[<RequireQualifiedAccess>]
module Piece =

    let internal make = curry Piece

    /// Returns the colour of a given chess piece.
    let colour = function Piece (colour, _) -> colour

    /// Returns the shape of a given chess piece.
    let chessman = function Piece (_, chessman) -> chessman

    /// Returns whether a given piece is of a given colour.
    let ofColour colour = function Piece (colour', _) -> colour = colour'
