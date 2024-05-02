module FsChess.Common.Functions

let curry2 f = fun a b -> f (a, b)
let curry3 f = fun a b c -> f (a, b, c)
let curry4 f = fun a b c d -> f (a, b, c, d)
let curry5 f = fun a b c d e -> f (a, b, c, d, e)
let curry = curry2


let uncurry2 f = fun (a, b) -> f a b
let uncurry3 f = fun (a, b, c) -> f a b c
let uncurry4 f = fun (a, b, c, d) -> f a b c d
let uncurry5 f = fun (a, b, c, d, e) -> f a b c d e
let uncurry = uncurry2