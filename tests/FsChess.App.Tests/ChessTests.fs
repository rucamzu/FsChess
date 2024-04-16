module FsChess.App.Tests.ChessTests

open FsChess.App.Chess
open Expecto
open FsChess.App.Tests

[<Tests>]
let newGameTests =
    testList "new game" <| List.collect id [
        [
            White, Rook, [ Squares.A1; Squares.H1 ]
            White, Knight, [ Squares.B1; Squares.G1 ]
            White, Bishop, [ Squares.C1; Squares.F1 ]
            White, Queen, [ Squares.D1 ]
            White, King, [ Squares.E1 ]
            White, Pawn, [ Squares.A2; Squares.B2; Squares.C2; Squares.D2; Squares.E2; Squares.F2; Squares.G2; Squares.H2 ]
            Black, Rook, [ Squares.A8; Squares.H8 ]
            Black, Knight, [ Squares.B8; Squares.G8 ]
            Black, Bishop, [ Squares.C8; Squares.F8 ]
            Black, Queen, [ Squares.D8 ]
            Black, King, [ Squares.E8 ]
            Black, Pawn, [ Squares.A7; Squares.B7; Squares.C7; Squares.D7; Squares.E7; Squares.F7; Squares.G7; Squares.H7 ]
        ]
        |> List.collect (fun (color, piece, squares) -> squares |> List.map (fun square -> (color, piece, square)))
        |> List.map (fun (color, piece, square) ->
            test $"starts with {color} {piece} at {square}" {
                Game.newGame
                |> Game.board
                |> Board.getAt square
                |> Expect.equal (color, piece) ""
            })
    ]