module FsChess.Rest.Chess

open System

open Giraffe

open FsChess.Chess
open FsChess.Chess.Chess
open FsChess.Chess.ChessNotation
open FsChess.App
open FsChess.Common
open FsChess.Common.Functions
open FsChess.Common.Tuples

type Game = {
    Board : string list
    PlayedMoves : string list
    PlayableMoves : Map<string, Uri>
}

/// Functions to compute URL identifiers from games and moves.
module GameId =

    /// Returns the id of a given game.
    let ofGame game =
        match Game.playedMoves game with
        | [] -> "new"
        | playedMoves -> playedMoves |> List.map annotateMove |> String.join "-"

    let toGame (api : FsChess.App.Api) gameId =
        let playMoveAnnotation game moveAnnotation =
            let move = game |> Game.playableMoves |> Seq.find (fun move -> annotateMove move = moveAnnotation)
            game |> Game.play move

        gameId
        |> String.split "-"
        |> Seq.fold playMoveAnnotation api.NewGame

/// Functions to compute URLs of games and playable moves.
module URL =

    /// Returns the root URL of a given game.
    let ofGame baseUrl game =
        Uri(baseUrl, $"/chess/games/{GameId.ofGame game}")

/// Functions to build data transfer objects that will be returned as part of HTTP responses.
module DTO =

    let buildBoard =
        Game.board >> Board.getAll >> Seq.map (flip >> uncurry2 annotatePlayedPiece) >> Seq.toList

    let buildPlayedMoves =
        Game.playedMoves >> List.map annotateMove

    let buildPlayableMoves game =
        let baseUrl = Uri("http://localhost:5064") // TODO: hardcoded base URL
        game
        |> Game.playableMoves
        |> Seq.map (fun move -> (annotateMove move, game |> Game.play move |> URL.ofGame baseUrl))
        |> Map.ofSeq

    let buildGame (game : FsChess.Chess.Chess.Game) : Game = {
        Board = game |> buildBoard
        PlayedMoves = game |> buildPlayedMoves
        PlayableMoves = game |> buildPlayableMoves
    }

module RestApi =

    let newGame api : HttpHandler =
        api.NewGame
        |> DTO.buildGame
        |> json

    let getGame api (gameId : string) : HttpHandler =
        gameId
        |> GameId.toGame api
        |> DTO.buildGame
        |> json

let webapi (api : FsChess.App.Api) : HttpHandler =
    choose [
        route "/chess/games/new" >=> RestApi.newGame api
        routef "/chess/games/%s" <| RestApi.getGame api

        RequestErrors.NOT_FOUND "Not found"
    ]
