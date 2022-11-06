## Drastic.Social

Drastic.Social is an App and App Framework for Mastodon. It is based off of (https://github.com/drasticactions/WinMasto)[WinMasto], a UWP Mastodon client I wrote some years ago.

The idea is to make a generic framework for creating applications for Mastodon, based on a set of generic view models that are not bound by an existing framework. You should be able to build apps for various platforms (WinUI, Avalonia, Uno, MAUI, or directly with OS specific frameworks) by focusing on the UI portion, while the underlying core libraries handle the rest.

This is _not_ a library for Mastodon itself. There are several for .NET out there, including (https://github.com/drasticactions/drastic.mastodon)[Drastic.Mastodon]. If you want to interact with the API itself, use those.