
(*** raw ***)
(*---
title: Partial Application in F#
updated: 2018-10-27
---*)

(**
Having done a small amount of streaming with F# and seen how people react to partial application of functions,
especially those who are already programmers in another language like C# or Java -
I thought it might be prudent to have a look at partial application in F# and some of the uses of it.

Let's say that we have the following function definition:
*)

let add a b = a + b

(**
Just a simple function that takes two integers and adds them thus:
*)

let simpleAdd = add 4 5

(**
To give the variable `simpleAdd` a value of:
*)

(*** include-value: simpleAdd ***)

(**
That looks like a function that takes 2 arguments,
however we must remember that F# curries functions
by default and therefore the above definition is equivalent to the following:
*)

let add' a =
    let add'' b =
        a + b
    add''

(**
Hang on - what's that word "Currying" all about? Well it's named after [Haskell Curry](https://en.wikipedia.org/wiki/Haskell_Curry)
as is the programming language of [Haskell](https://www.haskell.org/).

That didn't help much did it?

Fair enough - Currying is where a language treats all functions as functions that take a single argument and return a value.
There is nothing to say that the value returned can't be another function:
*)

let add2 = add 2
let add2' = add' 2

(**
Above are invocations of the `add` and `add'` functions. Hover over the `add2` and `add2'` - go on... I'm not going anywhere.
What did you notice? They are of the same type aren't they? Both `int -> int` if I'm not much mistaken. Excellent - now if you're
surprised by this then I guess you were expecting an error to the effect of `I'm terribly sorry but you haven't given me enough
information to run` - or at least that might be the case for `add2`

What happens instead looks like some form of suspended animation if you like, where `add2` waits for the other argument
before it kicks off and runs. In reality we're working with one-parameter (or one-argument) functions. Interestingly, to me anyway,
this does seem to explain the rather odd notation for a function signature in F#, being in this case `int -> int -> int`. There is no
distinction between successive parameters and the return type as F# is fine with you treating it as a function that takes an `int` and
returns a function of type `int -> int` or one that takes two `int`s in succession to return a single `int`
(I tend to read `->` as "to" or "goes to", so `int -> int -> int` is a function of int to int to int.)

### Use in the real world.

Let's say we have a function:
*)

let doesSomething x =
    printfn "Doing something"
    x + 1

(**
All we're saying here is that we want to do something with `x` but we want to log it to the console first. That's all fine and works nicely
but someday your co-worker or boss really wants that logging to go to a file in some cases and to the console in others. Now you could write
functions to do both. Let's not though, we're not interested in that kind of duplication. How about this instead?
*)

let doesSomethingWithALogger logger x =
    logger "Doing something"
    x + 1

let doesSomethingConsole = doesSomethingWithALogger (printfn "%s")

let doesSomethingWithNoLogging = doesSomethingWithALogger ignore

(**
We've just created a function that takes in a logger and then two functions that only pass in loggers. `doesSomethingConsole`,
`doesSomethingWithNoLogging` and `doesSomething` are all of type `int -> int`. They're all drop-in replacements for each other.
The eagle-eyed among you might have spotted that we've used partial application another time: `printfn "%s"` - that's because printfn
doesn't take a string, it takes some `Printf.TextWriterFormat` with a generic type, that's another post though.

Hopefully you now understand the idea behind partial application, how currying enables it and how injecting behaviour can clean up your

*)