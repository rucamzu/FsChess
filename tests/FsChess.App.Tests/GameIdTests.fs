module FsChess.Chess.Tests.BoardTests

open Expecto

open FsChess.Chess
open FsChess.App

[<Tests>]
let gameIdTests =

    testList "game id" [

        test "new game can be identified" {
            let game =
                Game.newGame
                |> GameId.ofGame
                |> GameId.toGame

            Expect.equal game Game.newGame "the game obtained from the id of a new game is not a new game"
        }

    ]