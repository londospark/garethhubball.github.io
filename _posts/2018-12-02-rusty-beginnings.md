---
title: Rusty Beginnings and Why a Systems Language?
updated: 2018-12-02
comments: true
---

As I'm sure you all know I've been doing a spot of streaming over on [twitch.tv](https://twitch.tv/garethhubball) and so far everything about it has been F#-based. Even my blog was designed so that I could use F# code and show you the types, the output, the syntax highlighting. I think it's pretty safe to say then... I adore F#.

## Is There Something Amiss?

Indeed there may well be, for me it's the experience of creating performant applications in a resource-constrained space. To be clear, I don't class phones and tablets as resource-constrained anymore. My phone and tablet both have quad-core processors in them and even at the low-end you're now picking up nothing less than a dual-core as far as I'm aware. (No doubt someone in the comments will correct me on this.) No, I think the resource constraints are now on the laptop/desktop computer market. Think about it carefully... yes, there is an abundance of RAM and processing power, but you're doing so many more things on these larger form factor machines than we ever want on a phone or a tablet.

How many times do you have more than one thing open at a time on your smartphone? If it's really high-end then you might have two things open. On your standard tablet I think it might go up to four. Everything else is suspended barring for some very simple tasks that are marshalled by the operating system (certainly true in iOS, unsure on Android). Now look at how many things are open at once on your laptop/desktop. Each browser tab is a process, each application is a process and given that people have for many years said that the desktop isn't resource-constrained you get some bonkers applications now.

Let's have a look at a popular framework of 2018 to create cross-platform applications: [Electron](https://github.com/electron/electron).

## Electrons Are Small... Right?

Yes... and no. In the real world they are minute, sub-atomic particles/waves of energy so small that they exhibit quantum behaviour. In our world though, we're looking at something that's horribly inefficient for writing desktop programs and while it should be easy to use, is just not something that I would wish to impose on somebody. Before I carry on my rant I must say that Electron has uses and that I'm even writing this post in an editor that is based on Electron. However, it does seem to take up an awful lot of RAM and CPU time for a text editor with some fancy features. We all used to joke about Emacs doing the same, some of us still do, however it is nothing like as resource-hungry as a customised version of Chrome with a Node.js server built-in and custom code running on the top of that lot.

This is a framework for developing cross-platform applications that, no matter how small your requirements, spins up Chrome, spins up a Node.js server and interprets Javascript. That's a lot of complexity for a simple application. Why do we need that?

## But That's Nothing To Do With F#!

Well spotted - it isn't. But how about we use F#? We still ahve to pay the cost of a JIT compiler and garbage collector on somebody's computer. .NET Native doesn't support F# yet so we don't have the ability to do AoT compilation, granted: we don't have to run a javascript server and a full browser-engine but how do we make it cross-platform? We could go with Xamarin.Forms as it now works with [macOS](https://blog.xamarin.com/preview-bringing-macos-to-xamarin-forms/) and [WPF](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/platform/wpf) but both are still in preview and I don't really rate how it works on Windows (not tried it on the Mac so I can't comment on that) - but even if it were fine on both: we're missing out on GNU/Linux which isn't exactly insignificant anymore.

## Enter GTK#

There is a toolset that should work across all three of the main platforms and that is [GTK#](https://www.mono-project.com/docs/gui/gtksharp/) but again we're not without issue here, we're still not out of the woods with regards to performance. We still have the .NET native issue to contend with and while that might come soon, there's no official release date for it and no previews yet either. There's also [Avalonia](http://avaloniaui.net/) but that has a load of abstractions too - it runs on top of Gtk, Win32 or MonoMac so it's yet another layer of indirection that serves to hinder performance.

## Fine, But Why Rust? C/C++ Would Work.

Ahhh - now we're getting to the meat of it. I agree that C/C++ would work and they are the traditional systems programming languages but there's some lack of safety there. I don't know much systems programming and can see me having all sorts of segfaults and use after free errors. I need some safety, I need me some support for functional programming techniques. So what does [Rust](https://www.rust-lang.org/) have that intrigued me? It has pattern-matching, that's a nice little feature - but no... I need more than that to be convinced to play with a language. Rust does have one secret-weapon that you love to hate - the borrow checker. Rust will check your pointers or borrows and make sure that you can't have data races, it can be hard to work with but it really can save you. Better still, this is static analysis so there's no GC involved, it's a zero-cost abstraction. So now you can have performance and productivity. Hooray to Rust!