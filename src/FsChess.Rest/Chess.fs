module FsChess.Rest.Chess

open Giraffe

open FsChess.App.Chess
open FsChess.App.ChessNotation
open FsChess.Common.Functions
open FsChess.Common.Tuples

type Game = {
    Board : string list
    PlayedMoves : string list
    PlayableMoves : string list
}

let private ofBoard =
    Board.getAll >> Seq.map (flip >> uncurry2 annotatePlayedPiece) >> Seq.toList

let ofGame (game : FsChess.App.Chess.Game) : Game = {
    Board = game |> Game.board |> ofBoard
    PlayedMoves = game |> Game.playedMoves |> List.map annotateMove
    PlayableMoves = game |> Game.playableMoves |> List.map annotateMove
}

let webapi (api : FsChess.App.Chess.Api) : HttpHandler =
    choose [
        route "/chess/games/new" >=>
            json (api.NewGame |> ofGame)
    ]
