namespace FsChess.Chess

/// A file on a chess board.
type File = A | B | C | D | E | F | G | H

/// A rank on a chess board.
type Rank = private Rank of int
    with override this.ToString() = match this with Rank rank -> $"{rank}"

/// A squares on a chess board.
type Square = private Square of File * Rank
    with override this.ToString() = match this with Square (file, rank) -> $"{file}{rank}"
    
/// All files on a chess board, indexed from A to H.
[<RequireQualifiedAccess>]
module Files =

    let A = File.A
    let B = File.B
    let C = File.C
    let D = File.D
    let E = File.E
    let F = File.F
    let G = File.G
    let H = File.H

/// All ranks on a chess board, indexed from 1 to 8.
[<RequireQualifiedAccess>]
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
[<RequireQualifiedAccess>]
module Rank =

    let next = function
        | Rank 8 -> failwith "Illegal"
        | Rank rank -> Rank (rank + 1)

    let prev = function
        | Rank 1 -> failwith "Illegal"
        | Rank rank -> Rank (rank - 1)

/// All squares on a chess board, indexed by file and rank.
[<RequireQualifiedAccess>]
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
[<RequireQualifiedAccess>]
module Square =

    let file = function Square (file, _) -> file

    let rank = function Square (_, rank) -> rank

    let atNextRank = function Square (file, rank) -> Square (file, Rank.next rank)

    let atPrevRank = function Square (file, rank) -> Square (file, Rank.prev rank)
