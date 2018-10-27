
(*** raw ***)
(*---
title: Testing some of the F# workflow.
updated: 2018-10-26
---*)

(**
Let's say that we have the following definition:
*)

let add a b = a + b

(**
That looks like a function that takes 2 arguments,
however we must remember that F# curries functions
by default and therefore the above definition is equivalent to the following:
*)

let add' a =
    let add'' b =
        a + b
    add''