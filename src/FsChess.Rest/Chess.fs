module FsChess.Rest.Chess

open System

open Giraffe

open FsChess.Chess
open FsChess.App
open FsChess.Common
open FsChess.Common.Functions
open FsChess.Common.Tuples

type GameDTO = {
    Board : string list
    PlayedMoves : string list
    PlayableMoves : Map<string, Uri>
}

/// Functions to build URLs associated to chess games.
module URL =

    /// Returns the URL associated to a given game.
    let ofGame baseUrl = function
        | Game.NewGame _ -> Uri(baseUrl, $"/chess/games/new")
        | game ->
            let gamePath =
                game
                |> Game.playedMoves
                |> Seq.map (Notation.annotateMove Notation.pieceLetter)
                |> String.join "/"
            Uri(baseUrl, $"/chess/games/{gamePath}")

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

/// Types and functions that make up the web API.
module WebApi =

    /// Returns a new chess game.
    let newGame api : HttpHandler =
        api.NewGame
        |> DTO.buildGame
        |> json

    let rec private playMoves api game moves =
        match moves with
        | nextMove :: remainingMoves -> playMoves api (api.PlayMove nextMove game) remainingMoves
        | [] -> game

    /// Returns the chess game corresponding to the sequence of moves encoded in the relative path
    let getGame api moves : HttpHandler =
        moves
        |> Seq.toList
        |> playMoves api api.NewGame
        |> DTO.buildGame
        |> json

let webapi (api : FsChess.App.Api) : HttpHandler =
    choose [
        subRouteCi "/chess/games/" <| choose [
            routeCi "new" >=> WebApi.newGame api
            routexpn "[KBRQK]?[a-h][1-8]" <| WebApi.getGame api
        ]

        RequestErrors.NOT_FOUND "Not found"
    ]
