module FsChess.Rest.Chess

open System

open Giraffe

open FsChess.Chess
open FsChess.App
open FsChess.Common.Functions
open FsChess.Common.Tuples

type GameDTO = {
    Board : string list
    PlayedMoves : string list
    PlayableMoves : Map<string, Uri>
}

/// Functions to compute URLs of games and playable moves.
module URL =

    /// Returns the root URL of a given game.
    let ofGame baseUrl game =
        Uri(baseUrl, $"/chess/games/{GameId.ofGame game}")

/// Functions to build data transfer objects that will be returned as part of HTTP responses.
module DTO =

    let buildBoard =
        Game.board >> Board.getAll >> Seq.map (flip >> uncurry2 Notation.annotatePlayedPiece) >> Seq.toList

    let buildPlayedMoves =
        Game.playedMoves >> List.map (Notation.annotateMove Notation.pieceSymbol)

    let buildPlayableMoves game =
        let baseUrl = Uri("http://localhost:5064") // TODO: hardcoded base URL
        game
        |> Game.playableMoves
        |> Seq.map (fun move -> (Notation.annotateMove Notation.pieceSymbol move, game |> Game.play move |> URL.ofGame baseUrl))
        |> Map.ofSeq

    let buildGame game : GameDTO = {
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
        |> api.MakeGameId
        |> api.GetGame
        |> DTO.buildGame
        |> json

let webapi (api : FsChess.App.Api) : HttpHandler =
    choose [
        route "/chess/games/new" >=> RestApi.newGame api
        routef "/chess/games/%s" <| RestApi.getGame api

        RequestErrors.NOT_FOUND "Not found"
    ]
