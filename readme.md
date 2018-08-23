Once upon a time there was a product called Windows Phone, and it was good, but Windows Phone didn't get the love and attention that other platforms got with apps, leaving it to it's passionate users to build for it instead.

Long ago I wrote a very popular app named BUDCC (Brendan's Unofficial Dropcam Client) which allowed users not only to view their Dropcams (now Nest Cams) from afar, but even pin a Live Tile to their home screen so they could always have an at a glance (and semi up to date view) of what their home was showing.

Over the years, Windows Phone has died (RIP), and many of us have moved on in some form, but the core client code which I originally wrote for WP8 (then backported to WP7, re-ported to WP8 and Windows 8) still has value.

While working on an Android port of the Live Tile functionality of BUDCC recently, I decided it would be worthwhile to make available this common backend I've used in several Dropcam/Nest Cam related apps.

This repo contains two C# projects:
- BUDCC.DropcamClient – a .NET Standard 1.2 library which contains DropcamClient which allows logging in, capturing images from cameras, listing clips, requesting new clips, deleting existing clips.
- ConsoleTest – .NET 4.6 console app to demonstrate basic functionality.
