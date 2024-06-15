module FsChess.Chess.Tests.MoveTests

open Expecto

open FsChess.Chess
open FsChess.Test.Common

[<Tests>]
let gameTests =

    testList "game" [
        
        testTheory "pawn at initial position can move one square forward" [
            Pieces.WhitePawn, Squares.A2, Squares.A3
        ] <| fun (piece, atSquare, toSquare) -> 
            let move = Game.newGame |> Game.playableMoves |> List.find (Move.isMove piece atSquare toSquare)
            Game.newGame
            |> Game.play move
            |> Game.board
            |> Board.getAt toSquare
            |> Option.get
            |> Expect.equal piece $"{piece} is not at {toSquare} after {move}"

        testTheory "pawn at initial position can move two squares forward" [
            Pieces.WhitePawn, Squares.E2
        ] <| fun (pawn, atSquare) ->
            Game.newGame
            |> Game.playableMoves
            |> Expect.contains (Move.makeMove pawn atSquare (atSquare |> Square.atNextRank |> Square.atNextRank)) $"{pawn} at {atSquare} cannot move two squares forward"

    ]