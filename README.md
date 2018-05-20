# WPF-UWP-Test1
A testing app for reading/writing uwp "People" contact.

## Current State
Working on Windows 10. Windows 8 has limited contact API I could use, so it won't work for Windows 8 currently.

I use `ContactManager` class from `Windows.ApplicationModel.Contacts` namespace, to get a list of `Contact` object from WIndows contact. 
I could also create/modify contact into Windows contact.
