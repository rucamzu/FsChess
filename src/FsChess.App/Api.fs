namespace FsChess.App

open FsChess.Chess

type Api = {
    NewGame : Game
    PlayMove : string -> Game -> Game
}

module Api =
    let api : Api = {
        NewGame = Game.newGame
        PlayMove = fun annotatedMove game ->
            game
            |> Game.play (game |> Game.playableMoves |> Seq.find (fun move -> (Notation.annotateMove Notation.pieceLetter move) = annotatedMove))
    }
