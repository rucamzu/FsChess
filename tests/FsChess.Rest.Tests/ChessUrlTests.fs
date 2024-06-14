module FsChess.Rest.Tests.ChessUrlTests

open System

open Giraffe
open Expecto
open FsChess.Chess
open FsChess.Common
open FsChess.Rest
open FsChess.Test.Common

let private baseUrl = Uri("https://www.chessurl.tests")

let private playAnnotated annotatedMove game =
    let move = 
        game
        |> Game.playableMoves
        |> List.find (fun move -> (Notation.annotateMove Notation.pieceLetter move) = annotatedMove)
    Game.play move game

[<Tests>]
let chessUrlTests = testList "URL" [
    test "of new game is correct" {
        Game.newGame
        |> Chess.URL.ofGame baseUrl
        |> Uri.localPath
        |> Expect.equal "/chess/games/new" ""
    }

    test "of game with played moves is correct" {
        Game.newGame
        |> playAnnotated "e4"
        |> playAnnotated "e5"
        |> playAnnotated "f4"
        |> Chess.URL.ofGame baseUrl
        |> Uri.localPath
        |> Expect.equal "/chess/games/e4/e5/f4" ""
    }
]
