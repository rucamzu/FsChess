module FsChess.Chess.Tests.MoveTests

open Expecto

open FsChess.Chess
open FsChess.Test.Common

[<Tests>]
let gameTests =

    testList "game" <| List.collect id [
        [
            Pieces.WhitePawn, Squares.A2, Squares.A3
        ]
        |> List.map (fun (piece, atSquare, toSquare) -> 
            test $"{piece} to {toSquare} from {atSquare}" {
                let game = Game.newGame
                let move = game |> Game.playableMoves |> List.find (Move.isMove piece atSquare toSquare)
                let game' = game |> Game.play move
                game'
                |> Game.board
                |> Board.getAt toSquare
                |> Option.get
                |> Expect.equal piece $"{piece} is not at {toSquare} after {move}"
            })

        [
            Pieces.WhitePawn, Squares.E2
        ]
        |> List.map (fun (pawn, atSquare) ->
            test $"{pawn} at {atSquare} can move two squares forward" {
                Game.newGame
                |> Game.playableMoves
                |> Expect.contains (Move.makeMove pawn atSquare (atSquare |> Square.atNextRank |> Square.atNextRank)) $"{pawn} at {atSquare} cannot move two squares forward"
            })

    ]