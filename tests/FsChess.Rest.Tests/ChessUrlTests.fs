module FsChess.Rest.Tests.ChessUrlTests

open System

open Giraffe
open Expecto
open FsChess.Chess
open FsChess.Common
open FsChess.Rest
open FsChess.Test.Common

let private baseUrl = Uri("https://www.chessurl.tests")

let private playAnnotatedMove annotatedMove game =
    let move = 
        game
        |> Game.playableMoves
        |> List.find (fun move -> (Notation.annotateMove Notation.pieceLetter move) = annotatedMove)
    Game.play move game

let private playAnnotatedMoves annotatedMoves game =
    Seq.fold (flip playAnnotatedMove) game annotatedMoves

[<Tests>]
let chessUrlTests = testList "URL" [

    test "of new game is correct" {
        Game.newGame
        |> Chess.URL.ofGame baseUrl
        |> Uri.localPath
        |> Expect.equal "/chess/games/new" ""
    }

    testTheory "of game with played moves is correct" [
        [ "e4" ]
        [ "e4"; "e5"; "f4" ]
    ] <| fun annotatedMoves ->
        let expectedUrlPath = "/chess/games/" + (String.join "/" annotatedMoves)
        Game.newGame
        |> playAnnotatedMoves annotatedMoves
        |> Chess.URL.ofGame baseUrl
        |> Uri.localPath
        |> Expect.equal expectedUrlPath ""

]
