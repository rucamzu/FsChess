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

module DTO =

    let private buildBoard =
        Game.board >> Board.getAll >> Seq.map (flip >> uncurry2 annotatePlayedPiece) >> Seq.toList

    let buildPlayedMoves =
        Game.playedMoves >> List.map annotateMove

    let buildPlayableMoves =
        Game.playableMoves >> List.map annotateMove

    let buildGame (game : FsChess.App.Chess.Game) : Game = {
        Board = game |> buildBoard
        PlayedMoves = game |> buildPlayedMoves
        PlayableMoves = game |> buildPlayableMoves
    }

let webapi (api : FsChess.App.Chess.Api) : HttpHandler =
    choose [
        route "/chess/games/new" >=> json (api.NewGame |> DTO.buildGame)
    ]
