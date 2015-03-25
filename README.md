# apptemplate-8.1
Universal App Template for Gateway Apps

A few notes:

1.  Clone this into Documents\Visual Studio vXX\Templates\Project Templates\Gateway Apps 8.1\
2.  When you create a new project using this template, you will get errors about missing packages.  Just ignore them, then right click on your solution and enable nuget package restore and do a build.
3.  This project includes Unity, PRISM, Raygun, and JSON.Net.
4.  There are some helper functions added to App.xaml.cs - LocalSettings(get and set), RemoteSettings(get and set), and GetResource<T>(resourceName)



Thanks and enjoy!
