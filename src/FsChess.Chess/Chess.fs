module FsChess.Chess.Chess

open FsChess.Common.Functions
open FsChess.Common.Tuples

type Move =
    | Move of Piece * Square * Square

type Game = private Game of Board * Move list * Move list

/// Functions to query and manipulate moves on a chess game .
module Move =

    /// Returns the moved chess piece.
    let piece = function
        | Move (piece, _, _) -> piece

    let makeMove piece atSquare toSquare = Move (piece, atSquare, toSquare)

    let isMove piece atSquare toSquare = function
        | Move (piece', atSquare', toSquare') -> piece = piece' && atSquare = atSquare' && toSquare = toSquare'

module Game =

    let private noPlayedMoves = []
    let private noPlayableMoves = []

    /// Returns the current board for a given game.
    let board = function Game (board, _, _) -> board

    /// Returns the list of all the moves played on a given game, in the order they were played.
    let playedMoves = function Game (_, played, _) -> played

    /// Returns the set of all moves that can be played next on a given game.
    let playableMoves = function Game (_, _, playable) -> playable

    /// Returns the colour of the player who plays next ona given name.
    let playingNext = function
        | Game (_, [], _) -> White
        | Game (_, played, _) ->
            match (played |> List.last |> Move.piece |> Piece.colour) with
                | White -> Black
                | Black -> White

    /// Functions to compute playable moves in a game of chess.
    module private PlayableMoves =

        /// Returns all the moves that can be played next with a given pawn on a given game.
        let forPawn game colour atSquare =
            match colour with
            | White -> [ Move.makeMove Pieces.WhitePawn atSquare (Square.atNextRank atSquare) ]
            | Black -> [ Move.makeMove Pieces.BlackPawn atSquare (Square.atPrevRank atSquare) ]

        /// Returns all the moves that can be played next with a given piece on a given game.
        let forPiece game piece atSquare =
            match piece |> Piece.chessman with
            | Pawn -> forPawn game (Piece.colour piece) atSquare
            | _ -> []

        /// Returns all the moves that can be played next on a given game.
        let forGame game =
            game
            |> board
            |> Board.getAll
            |> Seq.filter (fun (_, piece) -> Piece.ofColour (playingNext game) piece)
            |> Seq.map flip
            |> Seq.collect (uncurry2 (forPiece game))
            |> Seq.toList

    /// A new game with all pieces in their starting positions and no moves played.
    let newGame =
        let place colour chessman = Board.place (Piece.make colour chessman)
        let placeMany colour chessman = Board.placeMany (Piece.make colour chessman)

        let board =
            Board.empty
            |> placeMany White Rook [ Squares.A1; Squares.H1 ]
            |> placeMany White Knight [ Squares.B1; Squares.G1 ]
            |> placeMany White Bishop [ Squares.C1; Squares.F1 ]
            |> place White Queen Squares.D1
            |> place White King Squares.E1
            |> placeMany White Pawn [ Squares.A2; Squares.B2; Squares.C2; Squares.D2; Squares.E2; Squares.F2; Squares.G2; Squares.H2 ]
            |> placeMany Black Rook [ Squares.A8; Squares.H8 ]
            |> placeMany Black Knight [ Squares.B8; Squares.G8 ]
            |> placeMany Black Bishop [ Squares.C8; Squares.F8 ]
            |> place Black Queen Squares.D8
            |> place Black King Squares.E8
            |> placeMany Black Pawn [ Squares.A7; Squares.B7; Squares.C7; Squares.D7; Squares.E7; Squares.F7; Squares.G7; Squares.H7 ]

        Game (board, noPlayedMoves, Game (board, noPlayedMoves, noPlayableMoves) |> PlayableMoves.forGame)

    let play move = function
        Game (board, playedMoves, playableMoves) ->
            match move with
            | Move (piece, atSquare, toSquare) -> 
                let board' = board |> Board.remove atSquare |> Board.place piece toSquare
                let playedMoves' = List.append playedMoves [ move ]
                let playableMoves' = Game (board', playedMoves', noPlayableMoves) |> PlayableMoves.forGame
                Game (board', playedMoves', playableMoves')
