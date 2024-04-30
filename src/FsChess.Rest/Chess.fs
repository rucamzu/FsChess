module FsChess.Rest.Chess

open Giraffe

type Game = {
    Board : string list
    Moves : string list
}

let private mapBoard board = []

let private mapMoves moves = []

let private mapGame (game : FsChess.App.Chess.Game) : Game = {
    Board = game |> FsChess.App.Chess.Game.board |> mapBoard
    Moves = game |> FsChess.App.Chess.Game.moves |> mapMoves
}

let webapi (api : FsChess.App.Chess.Api) : HttpHandler =
    choose [
        route "/chess/games/new" >=>
            json (mapGame api.NewGame)
    ]
