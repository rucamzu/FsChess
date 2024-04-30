module FsChess.App.Tests.Expect

open Expecto

let equal expected message actual = Expecto.Expect.equal actual expected message

let isEmpty message actual = Expecto.Expect.isEmpty actual message