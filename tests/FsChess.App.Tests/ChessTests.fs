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
        |> List.collect (fun (colour, piece, squares) -> squares |> List.map (fun square -> (colour, piece, square)))
        |> List.map (fun (colour, piece, square) ->
            test $"starts with {colour} {piece} at {square}" {
                Game.newGame
                |> Game.board
                |> Board.getAt square
                |> Expect.equal (colour, piece) $"{colour} {piece} is not initially placed at {square}"
            })

        [
            test "has no played moves" {
                Game.newGame
                |> Game.played
                |> Expect.isEmpty "no moves have been played yet"
            }

            test "starts with White moving" {
                Game.newGame
                |> Game.playable
                |> Expect.all (Move.colour >> ((=) White)) "New game allows playable Black moves"
            } 
        ]

        [
            White, Pawn, Squares.A2, [ Squares.A3 ]
            White, Pawn, Squares.B2, [ Squares.B3 ]
            White, Pawn, Squares.C2, [ Squares.C3 ]
            White, Pawn, Squares.D2, [ Squares.D3 ]
            White, Pawn, Squares.E2, [ Squares.E3 ]
            White, Pawn, Squares.F2, [ Squares.F3 ]
            White, Pawn, Squares.G2, [ Squares.G3 ]
            White, Pawn, Squares.H2, [ Squares.H3 ]
        ]
        |> List.collect (fun (colour, piece, atSquare, toSquares) -> toSquares |> List.map (fun toSquare -> (colour, piece, atSquare, toSquare)))
        |> List.map (fun (colour, piece, atSquare, toSquare) ->
            test $"allows moving {colour} {piece} from {atSquare} to {toSquare}" {
                Game.newGame
                |> Game.playable
                |> Expect.contains (Move.makeMove colour piece atSquare toSquare)  $"{colour} {piece} at initial {atSquare} cannot move to {toSquare}"
            })

    ]