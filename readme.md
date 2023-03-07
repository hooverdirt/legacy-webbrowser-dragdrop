DISCLAIMER

This is demo code written in 2010 - THIS CODE WAS PUT HERE FOR REFERENCE ONLY. YOU SHOULD NOT HAVE TO USE THIS CODE.
NOTE ALL CODE WAS WRITTEN BY MYSELF.

Introduction

Quick and dirty POC that allows one to drag and drop text into an IE-based web browser.

The Scene:

It's early 2010 - and the product's use of an external control (CSEXWB) is reportedly causing issues in the newly released Windows 7. We have a problem - but support has been holding the fort for now, but we need a fix - the sooner the better.

Another (brilliant) senior dev Scott discovers that the issue is in an included COM control - and we don't have the sources to it/or we can't compile C++ code. Either way, We spitball:

1. Either find an alternative way thru possibly JS? Me: That would still require a control that accepts Drag & Drop events.
2. Me: How about I check my extensive Delphi collection and see if I wrote something like this in the past in Delphi...

I didn't find a wrapper control but I dive into the Windows/IE API.  A few days later, I pass the control back to Scott. To my absolute suprise and shock, he implements it into the product within a day. He says 'it worked with a few changes' (most likely the highlighting part/drag over - I'm 100% certain he changed some of the code...). We regression test everything. All looks good and it's pushed to prod.

How to compile:

1. Load in Visual Studio.
2. Compile/Build
3. Run!