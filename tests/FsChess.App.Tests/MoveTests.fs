module FsChess.App.Tests.MoveTests

open Expecto

open FsChess.App.Chess
open FsChess.App.Tests

[<Tests>]
let moveTests =

    testList "move" <| List.collect id [
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
                |> Expect.equal piece $"{piece} is not at {toSquare} after {move}"
            })

    ]