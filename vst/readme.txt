
Charlatan - Virtual Analog Synthesizer Plugin

(c) BlauKraut Engineering

Charlatan is a polyphonic, subtractive virtual analog (VA) synthesizer plugin with focus on sound quality and easy usability. It comes with a flexible, yet not overwhelming feature set which encourages artists to start tweaking instead of just relying on presets. Inspite of it's simplicity, Charlatan's architecture is flexible enough to create both classic sounds and rather experimental noises. While making no compromise about sound quality, the sound engine has been highly optimized for efficient CPU usage making Charlatan well suited as a "bread and butter" synth even in projects with a large number of plugin instances.

Best of all, it's FREEWARE! Enjoy!


Key Features
============

- 2 oscillators with shape modulation
- Oscillator hard synchronization and ring modulation
- Unison mode with upto 7 voices and stereo spread
- Stereo noise generator
- 2 ADSR envelope generators
- LFO with host tempo synchronization
- 3 filter types modeled after classic analog hardware: 4-pole (24 db/oct) and 2-pole (12 db/oct) lowpass, bandpass
- 3 voice modes: Monophonic, Monophonic Legato and 8-voice Polyphonic
- Ultra-low CPU usage


Change History
==============

2.0 Final (2015-06-05)
- Fix loading of 1.x presets
- Fix popup menus with multiple monitors
- Make Amp envelope sustain parameter logarithmic
- Fix old presets (LFO Wave), add some new ones
- Reduce pitch drift

2.0 Beta 3 (2015-05-03)
- BUGFIX crashes on AMD systems

2.0 Beta 2 (2015-05-02)
- BUGFIX not loading in some hosts
- BUGFIX note hanging after switching voice modes

2.0 Beta 1 (2015-05-02)
- completely rewritten DSP engine resulting in roughly 50% (!) CPU savings in polyphonic mode
- unison mode with upto 7 voices and stereo spread. Even with 7-voice unison, CPU usage is very bearable
- major GUI overhaul
- additional LFO waveforms
- stereo noise
- some more presets
- mostly compatible with presets for Charlatan 1.x (*)

(*) Due to some tweaks to the sound engine, there may be subtle audible differences when loading some of the old patches.
In most cases, it should be possible to compensate for those with some fine adjustments. Still, I recommend to keep an
old 1.x version of Charlatan around if you need to fully preserve old projects. Sorry for the inconvenience...


System requirements
===================

* Intel(R) x86-compatible CPU with support for Streaming SIMD Extensions 3 (SSE3)
* Microsoft(R) Windows(R) XP SP3 or later
* VST 2.4 compatible 32-bit or 64-bit host application


Installation
============

Extract Charlatan.dll from the zip-file into your VSTPlugins folder (often C:\Program Files\VSTPlugins). Choose the DLL from the x86 folder if you are using a 32-bit host application and the DLL from the x64 folder if your host application is 64-bit.


Legal Disclaimer
================

See license.txt

