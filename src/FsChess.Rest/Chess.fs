module FsChess.Rest.Chess

open Giraffe

let webapi : HttpHandler =
    choose [
        route "/chess/games/new" >=> text "New game"
    ]
