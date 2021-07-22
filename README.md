# Un4seen Unity

Expoloring using [BASS](http://www.un4seen.com/doc/#bass/bass.html) to play audio in Unity3D.

Unity's own audio system treats Tracker modules as any streaming audio asset. 
This is a complete waste as it throws out all the good data about what instruments and notes are playing, or any possiblity to modify patterns, rearrange the order or mute certain instruments etc. interactively.

This Repo contains a branch for each of the 3 wrappers for BASS in .NET that I'm aware of:
- [x] [BASS.NET](http://bass.radio42.com/help/#) 
- [ ] [ManagedBASS](https://github.com/ManagedBass/ManagedBass) 
- [ ] [BASSSharp](https://github.com/parksquare/BassSharp) (which is a fork of the above.)

The master branch is currently equivalent to the BASS.NET branch as that's the only I've got to work properly with the small test I've tried.

The included Unity Project should load a quick .it module I knocked out for testing, play it, and print in the Unity console when it reaches certain positions in the order & the end.

The position tracking only seems to work properly in BASS.NET wrapper though, which is why I've defaulted to using this one.
In the other two both trigger all the position synced events at the begining of the song instead, though the end synced event works normally.

BASS.NET has additional [licence costs](http://bass.radio42.com/help/html/b8b8a713-7af4-465e-a612-1acd769d4639.htm#LicenseCost) over the others, which are free (after the cost of BASS itself) but I guess that's the cost of getting more feature complete wrapper.

Last Tried in Unity 2020.3.14f1 (d0d1bb862f9d) @ 2021-07-22.
