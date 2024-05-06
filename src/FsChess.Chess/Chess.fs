module FsChess.Chess.Chess

open FsChess.Common.Functions
open FsChess.Common.Tuples

type File = A | B | C | D | E | F | G | H

type Rank = private Rank of int
    with override this.ToString() = match this with Rank rank -> $"{rank}"

type Square = private Square of File * Rank
    with override this.ToString() = match this with Square (file, rank) -> $"{file}{rank}"
    
type Board = private Board of Map<Square, Piece>

type Move =
    | Move of Piece * Square * Square

type Game = private Game of Board * Move list * Move list

/// Files of a chess board, indexed from A to H.
module Files =
    let A = File.A
    let B = File.B
    let C = File.C
    let D = File.D
    let E = File.E
    let F = File.F
    let G = File.G
    let H = File.H

/// Ranks of a chess board, indexed from 1 to 8.
module Ranks =
    let _1 = Rank 1
    let _2 = Rank 2
    let _3 = Rank 3
    let _4 = Rank 4
    let _5 = Rank 5
    let _6 = Rank 6
    let _7 = Rank 7
    let _8 = Rank 8

/// Functions to query and visit ranks on a chess board.
module Rank =

    let next = function
        | Rank 8 -> failwith "Illegal"
        | Rank rank -> Rank (rank + 1)

    let prev = function
        | Rank 1 -> failwith "Illegal"
        | Rank rank -> Rank (rank - 1)

/// Squares of a chess board, indexed by file and rank.
module Squares =
    let private make file rank = Square (file, rank)

    let A1 = make Files.A Ranks._1
    let A2 = make Files.A Ranks._2
    let A3 = make Files.A Ranks._3
    let A4 = make Files.A Ranks._4
    let A5 = make Files.A Ranks._5
    let A6 = make Files.A Ranks._6
    let A7 = make Files.A Ranks._7
    let A8 = make Files.A Ranks._8

    let B1 = make Files.B Ranks._1
    let B2 = make Files.B Ranks._2
    let B3 = make Files.B Ranks._3
    let B4 = make Files.B Ranks._4
    let B5 = make Files.B Ranks._5
    let B6 = make Files.B Ranks._6
    let B7 = make Files.B Ranks._7
    let B8 = make Files.B Ranks._8

    let C1 = make Files.C Ranks._1
    let C2 = make Files.C Ranks._2
    let C3 = make Files.C Ranks._3
    let C4 = make Files.C Ranks._4
    let C5 = make Files.C Ranks._5
    let C6 = make Files.C Ranks._6
    let C7 = make Files.C Ranks._7
    let C8 = make Files.C Ranks._8

    let D1 = make Files.D Ranks._1
    let D2 = make Files.D Ranks._2
    let D3 = make Files.D Ranks._3
    let D4 = make Files.D Ranks._4
    let D5 = make Files.D Ranks._5
    let D6 = make Files.D Ranks._6
    let D7 = make Files.D Ranks._7
    let D8 = make Files.D Ranks._8

    let E1 = make Files.E Ranks._1
    let E2 = make Files.E Ranks._2
    let E3 = make Files.E Ranks._3
    let E4 = make Files.E Ranks._4
    let E5 = make Files.E Ranks._5
    let E6 = make Files.E Ranks._6
    let E7 = make Files.E Ranks._7
    let E8 = make Files.E Ranks._8

    let F1 = make Files.F Ranks._1
    let F2 = make Files.F Ranks._2
    let F3 = make Files.F Ranks._3
    let F4 = make Files.F Ranks._4
    let F5 = make Files.F Ranks._5
    let F6 = make Files.F Ranks._6
    let F7 = make Files.F Ranks._7
    let F8 = make Files.F Ranks._8

    let G1 = make Files.G Ranks._1
    let G2 = make Files.G Ranks._2
    let G3 = make Files.G Ranks._3
    let G4 = make Files.G Ranks._4
    let G5 = make Files.G Ranks._5
    let G6 = make Files.G Ranks._6
    let G7 = make Files.G Ranks._7
    let G8 = make Files.G Ranks._8

    let H1 = make Files.H Ranks._1
    let H2 = make Files.H Ranks._2
    let H3 = make Files.H Ranks._3
    let H4 = make Files.H Ranks._4
    let H5 = make Files.H Ranks._5
    let H6 = make Files.H Ranks._6
    let H7 = make Files.H Ranks._7
    let H8 = make Files.H Ranks._8

/// Functions to query and visit squares on a chess board.
module Square =

    let file = function Square (file, _) -> file

    let rank = function Square (_, rank) -> rank

    let atNextRank = function Square (file, rank) -> Square (file, Rank.next rank)

    let atPrevRank = function Square (file, rank) -> Square (file, Rank.prev rank)

/// Functions to query and manipulate a chess board.
module Board =
    let empty = Board Map.empty

    /// Places a piece on a square of a given board. 
    let place piece atSquare = function
        | Board pieces -> pieces |> (Map.add atSquare piece) |> Board

    /// Places a piece on many squares of a given board. 
    let placeMany piece squares board =
        squares
        |> Seq.fold (fun board' square-> place piece square board') board

    /// Removes any piece on a square of a given board. 
    let remove square = function
        | Board pieces -> pieces |> Map.remove square |> Board

    /// Returns the piece placed on a square of a given board.
    let getAt square = function
        | Board pieces -> pieces |> Map.tryFind square |> Option.get

    /// Returns a list of all occupied squares together with the pieces placed in them.
    let getAll = function
        | Board pieces ->
            pieces
            |> Map.toSeq
            |> Seq.sortBy (fun (square, _) -> square)

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
