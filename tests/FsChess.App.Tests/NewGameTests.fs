module FsChess.App.Tests.NewGameTests

open Expecto

open FsChess.App.Chess
open FsChess.App.Tests

[<Tests>]
let newGameTests =
    testList "new game" <| List.collect id [
        [
            Pieces.WhiteRook, [ Squares.A1; Squares.H1 ]
            Pieces.WhiteKnight, [ Squares.B1; Squares.G1 ]
            Pieces.WhiteBishop, [ Squares.C1; Squares.F1 ]
            Pieces.WhiteQueen, [ Squares.D1 ]
            Pieces.WhiteKing, [ Squares.E1 ]
            Pieces.WhitePawn, [ Squares.A2; Squares.B2; Squares.C2; Squares.D2; Squares.E2; Squares.F2; Squares.G2; Squares.H2 ]
            Pieces.BlackRook, [ Squares.A8; Squares.H8 ]
            Pieces.BlackKnight, [ Squares.B8; Squares.G8 ]
            Pieces.BlackBishop, [ Squares.C8; Squares.F8 ]
            Pieces.BlackQueen, [ Squares.D8 ]
            Pieces.BlackKing, [ Squares.E8 ]
            Pieces.BlackPawn, [ Squares.A7; Squares.B7; Squares.C7; Squares.D7; Squares.E7; Squares.F7; Squares.G7; Squares.H7 ]
        ]
        |> List.collect (fun (piece, squares) -> squares |> List.map (fun square -> (piece, square)))
        |> List.map (fun (piece, square) ->
            test $"starts with {piece} at {square}" {
                Game.newGame
                |> Game.board
                |> Board.getAt square
                |> Expect.equal piece $"New game does not have {piece} initially placed at {square}"
            })

        [
            test "has no played moves" {
                Game.newGame
                |> Game.playedMoves
                |> Expect.isEmpty "New game already has played moves"
            }

            test "starts with White moving" {
                Game.newGame
                |> Game.playableMoves
                |> Expect.all (Move.piece >> Piece.colour >> ((=) White)) "New game allows playable moves by Black"
            } 
        ]

        [
            Pieces.WhitePawn, Squares.A2, [ Squares.A3 ]
            Pieces.WhitePawn, Squares.B2, [ Squares.B3 ]
            Pieces.WhitePawn, Squares.C2, [ Squares.C3 ]
            Pieces.WhitePawn, Squares.D2, [ Squares.D3 ]
            Pieces.WhitePawn, Squares.E2, [ Squares.E3 ]
            Pieces.WhitePawn, Squares.F2, [ Squares.F3 ]
            Pieces.WhitePawn, Squares.G2, [ Squares.G3 ]
            Pieces.WhitePawn, Squares.H2, [ Squares.H3 ]
        ]
        |> List.collect (fun (piece, atSquare, toSquares) -> toSquares |> List.map (fun toSquare -> (piece, atSquare, toSquare)))
        |> List.map (fun (piece, atSquare, toSquare) ->
            test $"allows moving {piece} from {atSquare} to {toSquare}" {
                Game.newGame
                |> Game.playableMoves
                |> Expect.contains (Move.makeMove piece atSquare toSquare)  $"New game does not allow {piece} at initial {atSquare} to move to {toSquare}"
            })

    ]