namespace FsChess.App

open FsChess.Chess.Chess

type Api = {
    NewGame : Game
}

module Api =
    let api : Api = {
        NewGame = Game.newGame
    }
