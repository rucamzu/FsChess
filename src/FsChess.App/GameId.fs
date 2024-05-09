namespace FsChess.App

open FsChess.Common
open FsChess.Chess

type GameId =
    | NewGameId
    | GameId of id:string
with
    override this.ToString() =
        match this with
        | NewGameId -> "new"
        | GameId id -> id


/// Functions to convert between chess games and game identifiers.
/// TODO: generate and parse game identifiers without move separators.
[<RequireQualifiedAccess>]
module GameId =

    /// Returns the identifier of a given game.
    let ofGame game =
        match game with
        | _ when game = Game.newGame -> NewGameId
        | _ -> game
            |> Game.playedMoves
            |> List.map (Notation.annotateMove Notation.pieceLetter)
            |> String.join "-"
            |> GameId

    /// Returns the game corresponding to a given identifier.
    let toGame = function
        | NewGameId -> Game.newGame
        | GameId gameId ->
            let playMoveAnnotation game moveAnnotation =
                let move = game |> Game.playableMoves |> Seq.find (Notation.annotateMove Notation.pieceSymbol >> (=) moveAnnotation)
                game |> Game.play move

            gameId
            |> String.split "-"
            |> Seq.fold playMoveAnnotation Game.newGame
