module FsChess.App.Tests.Expect

open Expecto

let equal expected message actual = Expecto.Expect.equal actual expected message

let contains element message sequence = Expecto.Expect.contains sequence element message

let exists predicate message sequence = Expecto.Expect.exists sequence predicate message

let isEmpty message actual = Expecto.Expect.isEmpty actual message