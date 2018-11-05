
(*** raw ***)
(*---
title: What is a functor?
updated: 2018-11-05
comments: true
---*)

(**
People will often ask me about monads - it's a common topic on the stream but
we don't really give functors any love. That's a pity, as functors are a great
gateway to understanding monads as well as many other things.

Let's have a quick look at what a functor has to do:
*)

// Just one example of a functor
type Functor<'a> = List<'a> 


type MapFunctor<'source, 'result> =
    ('source -> 'result) -> Functor<'source> -> Functor<'result>

(**
Let's break this down as it is a little unwieldy:

The first line is just giving us an example of something that could be considered a functor,
Lists aren't the only functors around though as we'll see later.

For now let's work on the meat of the definition: that `MapFunctor` that we defined.
Remember that `->` means that we're working with a function and that `a -> b` could be
read as "a goes to b" or just "a to b" so `MapFunctor` is a function that takes in a 
function of a single argument and a "wrapped" item, then returns a new "wrapped" item
where the function taken has been applied to each item before re-wrapping it.

A concrete example mught help I think:
*)

let ourList = [1; 2; 3; 4]

let mapList : MapFunctor<_, _> = List.map

let newList = mapList (fun item -> item + 1) ourList

(**
`newList` now holds the value:
*)

(*** include-value: newList ***)

(**
As you can see - each element has been incremented.

### Hold on - you never defined the MapFunctor, you just used List.map!

Indeed, well spotted. While I could go into implementing `List.map` here
I think it would be best left as an exercise to the reader. (Hint: pattern
matching and recursion are your friends.)

Let's introduce the `Option` Functor:
*)

type Option<'a> =
    | Some of 'a
    | None

(**
The general idea here is instead of `null` we have something where you either
have `Some` value or you have `None`. So what should our map function do?
I think we can pretty safely say that any `None` value should map to `None` and
that only if we pass in `Some` value should we execute the function thus:
*)

let mapOption func opt =
    match opt with
    | Some item -> Some (func item)
    | None -> None

(**
We can see that the function above does what we wanted. Something that we haven't yet
touched on yet though is that we can map to a different type. Let's have a look at that
now:
*)

let toString (num: int) = num.ToString()

let someNum = Some 1
let someStr = mapOption toString someNum

(**
`someStr` is now not an `Option<int>` but is the following `Option<string>`: 
*)
(*** include-value: someStr ***)
(**
As you can see - the `map` operation isn't just reserved for collections but can
work for other constructs too. Any construct that the `map` operation can sensibly be
defined on can be considered a functor.

By sensibly we mean the following:

- If we map a function that returns the original value over a functor then we must
get the original functor back (`fun x -> x` is the function that I'm referring to here.)

- Composing two functions and then mapping them should be the same as composing the functions
mapped on their own: `fa` and `fb` are functions here: `map (fa >> fb) functor` must give the
same result as `map fa (map fb functor)`

These may seem complicated but they really are just pretty standard behaviour and by following
these rules with a type you have just made a functor!
*)

