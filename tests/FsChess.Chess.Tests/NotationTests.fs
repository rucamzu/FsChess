module FsChess.Chess.Tests.ChessNotationTests

open Expecto

open FsChess.Chess
open FsChess.Test.Common

[<Tests>]
let algebraicNotationTests =
    testList "notation" [

        testTheory $"annotates piece" [
            Pieces.WhiteKing, "♔"
            Pieces.WhiteQueen, "♕"
            Pieces.WhiteRook, "♖"
            Pieces.WhiteBishop, "♗"
            Pieces.WhiteKnight, "♘"
            Pieces.WhitePawn, "♙"
            Pieces.BlackKing, "♔"
            Pieces.BlackQueen, "♕"
            Pieces.BlackRook, "♖"
            Pieces.BlackBishop, "♗"
            Pieces.BlackKnight, "♘"
            Pieces.BlackPawn, "♙"
        ] <| fun (piece, expectedSymbol) ->
            let annotatedPiece = Notation.pieceSymbol piece
            annotatedPiece
            |> Expect.equal expectedSymbol $"{piece} is represented with '{annotatedPiece}' rather than '{expectedSymbol}'"

        testTheory $"annotates played piece" [
            Pieces.WhiteKing, Squares.E1, "♔e1"
            Pieces.WhiteBishop, Squares.F1, "♗f1"
            Pieces.BlackQueen, Squares.D8, "♕d8"
            Pieces.BlackPawn, Squares.C7, "♙c7"
        ] <| fun (piece, square, expectedAnnotation) ->
            let annotatedPlayedPiece = Notation.annotatePlayedPiece piece square
            annotatedPlayedPiece
            |> Expect.equal expectedAnnotation $"{piece} at {square} is annotated as '{annotatedPlayedPiece}' rather than '{expectedAnnotation}'"

        testTheory $"annotates move" [
            Pieces.WhitePawn, Squares.B2, Squares.B3, "b3"
            Pieces.BlackBishop, Squares.C8, Squares.D7, "♗d7"
        ] <| fun (piece, atSquare, toSquare, expectedAnnotation) ->
            let annotatedMove =
                Move.makeMove piece atSquare toSquare
                |> Notation.annotateMove Notation.pieceSymbol
            annotatedMove
            |> Expect.equal expectedAnnotation $"Moving {piece} at {atSquare} to {toSquare} is annotated as '{annotatedMove}' rather than '{expectedAnnotation}'"

    ]