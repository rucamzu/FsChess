module FsChess.Rest.Chess

open System

open Giraffe

open FsChess.App.Chess
open FsChess.App.ChessNotation
open FsChess.Common.Functions
open FsChess.Common.Tuples

type Game = {
    Board : string list
    PlayedMoves : string list
    PlayableMoves : Map<string, Uri>
}

module String =

    let join (separator : string) (strings : string seq) =
        System.String.Join(separator, strings)

    let split (separator : string) (s : string) =
        s.Split(separator)

/// Functions to compute URL identifiers from games and moves.
module GameId =

    /// Returns the id of a given game.
    let ofGame game =
        match Game.playedMoves game with
        | [] -> "new"
        | playedMoves -> playedMoves |> List.map annotateMove |> String.join "-"

    let toGame api gameId =
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

    let buildGame (game : FsChess.App.Chess.Game) : Game = {
        Board = game |> buildBoard
        PlayedMoves = game |> buildPlayedMoves
        PlayableMoves = game |> buildPlayableMoves
    }

module Handle =

    let newGame api : HttpHandler =
        api.NewGame
        |> DTO.buildGame
        |> json

    let getGame api (gameId : string) : HttpHandler =
        gameId
        |> GameId.toGame api
        |> DTO.buildGame
        |> json

let webapi (api : FsChess.App.Chess.Api) : HttpHandler =
    choose [
        route "/chess/games/new" >=> Handle.newGame api
        routef "/chess/games/%s" <| Handle.getGame api

        RequestErrors.NOT_FOUND "Not found"
    ]
