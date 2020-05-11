
<br />
<p align="center">
  <h1 align="center">Awaitable Popups</h3>
  <p align="center">
    Customisable Popups for Xamarin Forms
    <br />
  </p>
</p>



<!-- TABLE OF CONTENTS -->

## Table of Contents

**Codacy**  [![Codacy Badge](https://api.codacy.com/project/badge/Grade/609f811983454f5babd100dabde256c1)](https://app.codacy.com/manual/LuckyDucko/AwaitablePopups?utm_source=github.com&utm_medium=referral&utm_content=LuckyDucko/AwaitablePopups&utm_campaign=Badge_Grade_Settings)
**NuGet** [![nuget](https://img.shields.io/nuget/v/AwaitablePopups.svg)](https://www.nuget.org/packages/AwaitablePopups/)
**CodeFactor** [![CodeFactor](https://www.codefactor.io/repository/github/luckyducko/awaitablepopups/badge)](https://www.codefactor.io/repository/github/luckyducko/awaitablepopups)
**Fuget** [![AwaitablePopups on fuget.org](https://www.fuget.org/packages/AwaitablePopups/badge.svg)](https://www.fuget.org/packages/AwaitablePopups)
(Use Fuget to see API differences between each package.)

https://www.fuget.org/packages/AwaitablePopups/badges
* [About the Project](#about-the-project)
  * [Built With](#built-with)
* [Getting Started](#getting-started)
  * [Installation](#installation)
* [Usage](#usage)
* [Contributing](#contributing)
* [License](#license)
* [Contact](#contact)
* [Acknowledgements](#acknowledgements)



<!-- ABOUT THE PROJECT -->
## About The Project


Awaitable Popups is a neat blend of the Rg.Plugins.Popup and AsyncAwaitBestPractices plugins to bring you a quick way to add popups into your Xamarin Forms App using familiar concepts 



### Built With
* [Rg.Plugins.Popup](https://github.com/rotorgames/Rg.Plugins.Popup)
* [AsyncAwaitBestPractices](https://github.com/brminnick/AsyncAwaitBestPractices)


<!-- GETTING STARTED -->
## Getting Started

First, you must follow the [initialisation](https://github.com/rotorgames/Rg.Plugins.Popup/wiki/Getting-started)
 guide set out by Rg.Plugins.Popup, once you have that, have a look at usage down below

### Installation

You can install the nuget by looking up 'AwaitablePopups' in your nuget package manager, or by getting it [here](https://www.nuget.org/packages/AwaitablePopups/)



<!-- USAGE EXAMPLES -->
## Usage
here is an example of what this plugin makes easy (Looks slow due to giphy)

![Gif Example](https://media.giphy.com/media/hosV0dFihvyl0tpFGU/giphy.gif)


I have included the older, 'safer' example, and the new simplfied example.

### New Example

To Use the plugin for its inbuilt popup pages (Dual/Single Response, TextInput and loader.) All you need are these one liners

`SingleResponse Popup Page`
```csharp
return await SingleResponseViewModel.GeneratePopup(new PopupButton(Color.Goldenrod, Color.Black, "Okay"), Color.Gray, "Your Phone Number or Pin is incorrect, please try again.", "NoSource.png");
```

`DualResponse Popup Page`
```csharp
return await DualResponseViewModel.GeneratePopup(new PopupButton(Color.Green, Color.Black, "I Accept"), new PopupButton(Color.Red, Color.Black, "I decline"), Color.Gray, "Do you accept the terms and conditions?", "NoSource.png");
```

`Loader Popup Page`
```csharp
  await PopupService.WrapTaskInLoader(Task.Delay(10000), Color.Blue, Color.White, LoadingReasons(), Color.Black);
```

`Text Input PopupPage`
```csharp
await TextInputViewModel.GeneratePopup(new PopupButton(Color.Green, Color.Black, "I Accept"), new PopupButton(Color.Red, Color.Black, "I decline"), Color.Green, "TEXT HERE", "Placeholder");
```

LoginPage is currently going through some testing, come back soon. 
`LoginPage PopupPage`
```csharp
await LoginPageViewModel.GeneratePopup(CHECK DOCUMENTATION);
```

or, to return from the loader a value
```csharp
await PopupService.WrapReturnableTaskInLoader<bool>(IndepthCheckAgainstDatabase(), Color.Blue, Color.White, LoadingReasons(), Color.Black);
```

you can also add in synchronous functions, however they are wrapped in a task
```csharp

private bool LongRunningFunction(int millisecondDelay)
{
    Thread.Sleep(millisecondDelay);
    return true;
}
await PopupService.WrapReturnableFuncInLoader(LongRunningFunction, 6000, Color.Blue, Color.White, LoadingReasons(), Color.Black);

```

That's it! for a more indepth of how it works, read on (and check the wiki!)

### Older 'Safer' Example

This example is safer as it allows you to do more direct manipulation with the commands, instead of relying on sneaky wrapping and whatnot.
This is a function that is called when a user logs in using the incorrect credentials.

First, there is the loader. This is a new addition to the Plugin, and it allows you to wrap any function in a 
Progress loader, and it will automatically disappear when that task is complete. It has an extra of allowing you to scroll through messages that will display to the end user, if you so choose.

Second, now that the Login attempt is over, We create the ViewModel, and then assign the ViewModel properties with what we want. 
The most important is the Single Button Command here, (which can accept the AsyncCommand from AsyncAwaitBestPractices or anything that implements ICommand). 

This property allows you to enact any functionality within the popup once a button is pressed, this could be a call to an API, an extended amount of processing, it does however, need to finish with SafeCloseModal, a function which will return whatever value you place inside it (aslong as it matches pushasync, which is below)

Once you have setup the rest of the properties (image is important), you then use PushAsync.
PushAsync is the generic glue that holds this all together. With it, you will specify the ViewModel, the Page, and the type you are returning using safeclosemodal. 

This is because one ViewModel could be used for several popuppages, and so forth.

A limitation here is that no matter what, you return one type. This is something I will need to workaround in the future.
```csharp
private async Task<bool> IncorrectLoginAsync()
{
    System.Collections.Generic.List<string> Reasons = new System.Collections.Generic.List<string>
    {
        "Twiddling Thumbs",
        "Rolling Eyes",
        "Checking Watch",
        "General Complaining",
        "Calling in late to work",
        "Waiting"
    };
    await PopupService.WrapTaskInLoader(Task.Delay(10000), Xamarin.Forms.Color.Blue, Xamarin.Forms.Color.White, Reasons, Xamarin.Forms.Color.Black);

    var incorrectLoginError = new SingleResponseViewModel(PopupService);
    incorrectLoginError.SingleButtonCommand = new Xamarin.Forms.Command(() => incorrectLoginError.SafeCloseModal(false));
    incorrectLoginError.SingleButtonColour = Xamarin.Forms.Color.Goldenrod;
    incorrectLoginError.SingleButtonText = "Okay";
    incorrectLoginError.MainPopupInformation = "Your Phone Number or Pin is incorrect, please try again.";
    incorrectLoginError.MainPopupColour = Xamarin.Forms.Color.Gray;
    incorrectLoginError.SingleDisplayImage = "NoSource.png";
    return await PopupService.PushAsync<SingleResponseViewModel, SingleResponsePopupPage, bool>(incorrectLoginError);
}
```


This function then is called like this:
```csharp
if (string.IsNullOrEmpty(Mobile) || string.IsNullOrEmpty(Password))
{
    loginResult = await IncorrectLoginAsync();
}
```

And then, if a user puts in an incorrect login, the popup will show, wait for user interaction, and fire off 
`incorrectLoginError.SafeCloseModal(false))` which in turn, makes `loginResult = false`.




**If you want to make your own Popup Page**

This is the real power of this Plugin (Thanks in no small amount to Rotorgames amazing plugin). If you look at the source for DualResponsePopupPage, or the SingleResponse version you'll notice that they are just simple Xaml Pages. Nothing fancy. (except for the rg popup spice). 

All you need to do is follow that example. So:
1. Create Xaml Page with codebehind
2. Create your ViewModel that is associated with the popup, lets call ours `InformationPopupPage`
3. Ensure your ViewModel inherits from `PopupViewModel<TReturnable>` where `TReturnable` is what you want the popuppage to return to its caller
4. Ensure that your xaml page codebehind inherits from `PopupPage` (requirement to use rg plugins popup) and `IGenericViewModel<TViewModel>` where `TViewModel` is your Viewmodel, in our case it will be `IGenericViewModel<InformationPopupPage>`
5. You're ready to start using it the same as `DualResponsePopupPage`


<!-- CONTRIBUTING -->
## Contributing

Coming soon, but if you have any ideas or anything, please feel free to make an issue or PR.



<!-- LICENSE -->
## License

This project uses the MIT License



<!-- CONTACT -->
## Contact

My [Github](https://github.com/LuckyDucko),
or reach me on the [Xamarin Slack](https://xamarinchat.herokuapp.com/),
or on my [E-mail](tyson@logchecker.com.au)

Project Link: [AwaitablePopups](https://github.com/LuckyDucko/AwaitablePopups)



<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements
* [Brimmick](https://github.com/brminnick) has been a model to follow (might steal his hackernews app for an example)

