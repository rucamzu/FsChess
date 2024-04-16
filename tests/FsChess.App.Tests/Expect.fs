module FsChess.App.Tests.Expect

open Expecto

let equal expected message actual = Expecto.Expect.equal actual expected message