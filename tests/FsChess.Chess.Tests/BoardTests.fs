module FsChess.Chess.Tests.BoardTests

open Expecto

open FsChess.Chess
open FsChess.Test.Common

[<Tests>]
let boardTests =

    testList "board" [

        testTheory "square is empty before having any pieces placed in it" [
            Squares.A1; Squares.D6; Squares.H8
        ] <| fun square ->
            let piece = Board.empty |> Board.getAt square
            Expect.isNone $"Found {piece} at {square} on an empty board"

        testTheory $"can retrieve last piece placed in square" [
            Squares.A2, [ Pieces.WhiteKnight ]
            Squares.B4, [ Pieces.BlackQueen; Pieces.BlackKing ]
            Squares.G7, [ Pieces.WhitePawn; Pieces.WhitePawn; Pieces.BlackPawn; Pieces.WhitePawn; Pieces.BlackPawn ]
        ] <| fun (square, pieces) ->
            let lastPiece = List.last pieces
            pieces
            |> List.fold (fun board piece' -> Board.place piece' square board) Board.empty
            |> Board.getAt square
            |> Expect.equal (Some lastPiece) $"Cannot retrieve {lastPiece} placed last at {square}"

        testTheory "square is empty after removing last piece placed in it" [
            Squares.D2, [ Pieces.WhiteBishop ]
            Squares.F8, [ Pieces.WhiteQueen; Pieces.BlackKing ]
            Squares.G3, [ Pieces.WhitePawn; Pieces.WhitePawn; Pieces.BlackPawn; Pieces.WhitePawn; Pieces.BlackPawn ]
        ] <| fun (square, pieces) ->
            let piece =
                pieces
                |> List.fold (fun board piece' -> Board.place piece' square board) Board.empty
                |> Board.remove square
                |> Board.getAt square
            piece |> Expect.isNone $"Found {piece} placed at {square} after removing any pieces in it"

    ]