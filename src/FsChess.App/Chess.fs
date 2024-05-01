module FsChess.App.Chess

type Piece = Pawn | Knight | Bishop | Rook | Queen | King

type Color = White | Black

type File = A | B | C | D | E | F | G | H

type Rank = private Rank of int
    with override this.ToString() = match this with Rank rank -> $"{rank}"

type Square = private Square of File * Rank
    with override this.ToString() = match this with Square (file, rank) -> $"{file}{rank}"
    
type Board = private Board of Map<Square, Color * Piece>

type Move =
    | Move of exn
    | Castle of exn
    | Capture of exn
    | CaptureEnPassant of exn

type Game = private Game of Board * Move list

/// Files of a chess board, indexed from A to H
module Files =
    let A = File.A
    let B = File.B
    let C = File.C
    let D = File.D
    let E = File.E
    let F = File.F
    let G = File.G
    let H = File.H

/// Ranks of a chess board, indexed from 1 to 8
module Ranks =
    let private make = Rank

    let _1 = make 1
    let _2 = make 2
    let _3 = make 3
    let _4 = make 4
    let _5 = make 5
    let _6 = make 6
    let _7 = make 7
    let _8 = make 8

/// Squares of a chess board, indexed by file and rank
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

module Board =
    let empty = Board Map.empty

    let place color piece square = function
        | Board pieces -> pieces |> Map.add square (color, piece) |> Board

    let placeMany color piece squares board =
        squares
        |> Seq.fold (fun board' square-> place color piece square board') board

    let getAt square = function
        | Board pieces -> pieces |> Map.tryFind square |> Option.get

    let getAll = function
        | Board pieces ->
            pieces
            |> Map.toSeq
            |> Seq.map (fun (square, (color, piece)) -> square, color, piece)
            |> Seq.sortBy (fun (square, _, _) -> square)

module Game =
    let newGame =
        let board =
            Board.empty
            |> Board.placeMany White Rook [ Squares.A1; Squares.H1 ]
            |> Board.placeMany White Knight [ Squares.B1; Squares.G1 ]
            |> Board.placeMany White Bishop [ Squares.C1; Squares.F1 ]
            |> Board.place White Queen Squares.D1
            |> Board.place White King Squares.E1
            |> Board.placeMany White Pawn [ Squares.A2; Squares.B2; Squares.C2; Squares.D2; Squares.E2; Squares.F2; Squares.G2; Squares.H2 ]
            |> Board.placeMany Black Rook [ Squares.A8; Squares.H8 ]
            |> Board.placeMany Black Knight [ Squares.B8; Squares.G8 ]
            |> Board.placeMany Black Bishop [ Squares.C8; Squares.F8 ]
            |> Board.place Black Queen Squares.D8
            |> Board.place Black King Squares.E8
            |> Board.placeMany Black Pawn [ Squares.A7; Squares.B7; Squares.C7; Squares.D7; Squares.E7; Squares.F7; Squares.G7; Squares.H7 ]
        Game (board, [])

    let board = function Game (board, _) -> board

    let moves = function Game (_, moves) -> moves

type Api = {
    NewGame : Game
}

module Api =
    let api : Api = {
        NewGame = Game.newGame
    }
