namespace FsChess.App

open FsChess.Chess

type Api = {
    NewGame : Game
    MakeGameId : string -> GameId
    GetGameId : Game -> GameId
    GetGame : GameId -> Game
}

module Api =
    let api : Api = {
        NewGame = Game.newGame
        MakeGameId = GameId
        GetGameId = GameId.ofGame
        GetGame = GameId.toGame
    }
