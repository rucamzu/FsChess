module FsChess.Rest.Chess

open Giraffe

open FsChess.App.Chess
open FsChess.Common.Functions

type Game = {
    Board : string list
    // Moves : string list
}

module Game =

    let private ofColorPiece color piece =
        match (color, piece) with
        | White, King -> "♔"
        | White, Queen -> "♕"
        | White, Rook -> "♖"
        | White, Bishop -> "♗"
        | White, Knight -> "♘"
        | White, Pawn -> "♙"
        | Black, King -> "♔"
        | Black, Queen -> "♕"
        | Black, Rook -> "♖"
        | Black, Bishop -> "♗"
        | Black, Knight -> "♘"
        | Black, Pawn -> "♙"

    let private ofSquareColorPiece square color piece =
        $"{ofColorPiece color piece}{square}"

    let private ofBoard =
        Board.getAll >> Seq.map (uncurry3 ofSquareColorPiece) >> Seq.toList

    let ofGame (game : FsChess.App.Chess.Game) : Game = {
        Board = game |> Game.board |> ofBoard
        // Moves = game |> Game.moves |> mapMoves
    }

let webapi (api : FsChess.App.Chess.Api) : HttpHandler =
    choose [
        route "/chess/games/new" >=>
            json (api.NewGame |> Game.ofGame)
    ]
