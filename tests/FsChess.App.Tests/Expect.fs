module FsChess.App.Tests.Expect

open Expecto

let all predicate message sequence = Expecto.Expect.all sequence predicate message

let contains element message sequence = Expecto.Expect.contains sequence element message

let equal expected message actual = Expecto.Expect.equal actual expected message

let exists predicate message sequence = Expecto.Expect.exists sequence predicate message

let isEmpty message actual = Expecto.Expect.isEmpty actual message

let isFalse message actual = Expecto.Expect.isFalse actual message

let isTrue message actual = Expecto.Expect.isTrue actual message
