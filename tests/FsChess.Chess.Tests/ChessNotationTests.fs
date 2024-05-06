module FsChess.Chess.Tests.ChessNotationTests

open Expecto

open FsChess.Chess.Chess
open FsChess.Chess.ChessNotation
open FsChess.Chess.Tests

[<Tests>]
let algebraicNotationTests =
    testList "algebraic notation" <| List.collect id [

        [
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
        ]
        |> List.map (fun (piece, symbol) ->
            test $"represents {piece} with symbol '{symbol}'" {
                annotatePiece piece
                |> Expect.equal symbol $"{piece} is not represented with symbol '{symbol}'"
            })

        [
            Pieces.WhiteKing, Squares.E1, "♔e1"
            Pieces.WhiteBishop, Squares.F1, "♗f1"
            Pieces.BlackQueen, Squares.D8, "♕d8"
            Pieces.BlackPawn, Squares.C7, "♙c7"
        ]
        |> List.map (fun (piece, square, annotation) ->
            test $"annotates {piece} at {square} as '{annotation}'" {
                annotatePlayedPiece piece square
                |> Expect.equal annotation $"{piece} at {square} is not annotated as '{annotation}'"
            })

        [
            Pieces.WhitePawn, Squares.B2, Squares.B3, "♙b3"
        ]
        |> List.map (fun (piece, atSquare, toSquare, annotation) ->
            test $"annotates moving {piece} at {atSquare} to {toSquare} as '{annotation}'" {
                Move.makeMove piece atSquare toSquare
                |> annotateMove
                |> Expect.equal annotation $"Moving {piece} at {atSquare} to {toSquare} is not annotated as '{annotation}'"
            })

    ]