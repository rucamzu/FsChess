module FsChess.Chess.Tests.BoardTests

open Expecto

open FsChess.Chess
open FsChess.Test.Common

[<Tests>]
let boardTests =

    testList "board" <| List.collect id [

        [ Squares.A1; Squares.D6; Squares.H8 ]
        |> List.map (fun square ->
            test $"square {square} is empty before placing any pieces in it" {
                let piece = Board.empty |> Board.getAt square
                piece |> Expect.isNone $"Found {piece} at {square} on an empty board"
            })

        [
            Squares.A2, [ Pieces.WhiteKnight ]
            Squares.B4, [ Pieces.BlackQueen; Pieces.BlackKing ]
            Squares.G7, [ Pieces.WhitePawn; Pieces.WhitePawn; Pieces.BlackPawn; Pieces.WhitePawn; Pieces.BlackPawn ]
        ]
        |> List.map (fun (square, pieces) ->
            let lastPiece = List.last pieces
            test $"square {square} has {lastPiece} placed in it" {
                let piece = pieces |> List.fold (fun board piece' -> Board.place piece' square board) Board.empty |> Board.getAt square
                piece |> Expect.equal (Some lastPiece) $"Cannot retrieve {lastPiece} placed last at {square}"
            })

        [
            Squares.D2, [ Pieces.WhiteBishop ]
            Squares.F8, [ Pieces.WhiteQueen; Pieces.BlackKing ]
            Squares.G3, [ Pieces.WhitePawn; Pieces.WhitePawn; Pieces.BlackPawn; Pieces.WhitePawn; Pieces.BlackPawn ]
        ]
        |> List.map (fun (square, pieces) ->
            test $"square {square} is empty after removing any pieces in it" {
                let piece =
                    pieces
                    |> List.fold (fun board piece' -> Board.place piece' square board) Board.empty
                    |> Board.remove square
                    |> Board.getAt square
                piece |> Expect.isNone $"Found {piece} placed at {square} after removing any pieces in it"
            })

    ]