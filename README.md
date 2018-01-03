# Xamarin.Forms ReactiveUI Quickstart
### .NetStandard 2.0 version instead of PCL found [here](https://github.com/helzgate/XamarinFormsQuickStart2.0)

## Using:
* Akavache
* Autofac
* GoogleAnalytics
* ReactiveUI

## Pages:
* About Page 
* Terms Page (to disable set IsTermsPageEnabled to false  in Constants.cs)
* Policy Page (to disable set IsTermsPageEnabled to false  in Constants.cs)
* Login Page (easily turn off in Constants.cs)
* Settings Page
* Home Page

## Features:
* SVG icons
* User defined font size
* Base level theming via Constants.cs
* Login button has a random chance of working, allowing you to setup failure logic even though no authentication system is being called yet.
* User settings are stored using Akavache (Sqlite)
* About, Policy, and Terms page allow you to use HTML

## Works Using:
* Android
* iOS
* UWP

## My System
* Windows 10 Professional with Fall Creator's update
* Visual Studio Enterprise v15.4.3

## Notes
* I'm not using ReactiveUI routing because my understanding is that it doesn't work with MasterDetailPage according to [this](https://stackoverflow.com/questions/28624011/xamarin-form-reactive-ui-masterdetailpage) Stack Overflow.  I tried anyway but couldn't get it working.