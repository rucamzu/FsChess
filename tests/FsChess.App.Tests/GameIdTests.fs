module FsChess.Chess.Tests.BoardTests

open Expecto

open FsChess.Chess
open FsChess.App
open FsChess.Test.Common

[<Tests>]
let gameIdTests =

    testList "game id" [

        test "new game can be identified" {
            Game.newGame
            |> GameId.ofGame
            |> GameId.toGame
            |> Expect.equal Game.newGame "the game obtained from the id of a new game is not a new game"
        }

    ]