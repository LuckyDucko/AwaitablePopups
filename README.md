
<br />
<p align="center">
  <h1 align="center">Awaitable Popups</h3>
  <p align="center">
    Customisable Popups for Xamarin Forms
    <br />
  </p>
</p>
<a href="https://www.codacy.com/manual/LuckyDucko/AwaitablePopups?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=LuckyDucko/AwaitablePopups&amp;utm_campaign=Badge_Grade"><img src="https://api.codacy.com/project/badge/Grade/a40801ba4d7848888fd5cf8e6d673a07"/></a>


<!-- TABLE OF CONTENTS -->
## Table of Contents

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

I have included a very base example, but here is the nuts and bolts. 

This is a function that is called when a user incorrectly logs in.
We create the ViewModel, and then assign the ViewModel properties with what we want. 
The most important is the Single Button Command here, (which can accept the AsyncCommand from AsyncAwaitBestPractices or anything that implements ICommand). 

This property allows you to enact any functionality within the popup once a button is pressed, this could be a call to an API, an extended amount of processing, it does however, need to finish with SafeCloseModal, a function which will return whatever value you place inside it (aslong as it matches pushasync, which is below)

Once you have setup the rest of the properties (image is important), you then use PushAsync.
PushAsync is the generic glue that holds this all together. With it, you will specify the ViewModel, the Page, and the type you are returning using safeclosemodal. 

This is because one ViewModel could be used for several popuppages, and so forth.

A limitation here is that no matter what, you return one type. This is something I will need to workaround in the future.
```csharp
private async Task<bool> IncorrectLoginAsync()
{
    var incorrectLoginError = new SingleResponseViewModel(PopupService);
    incorrectLoginError.SingleButtonCommand = new Xamarin.Forms.Command(() => incorrectLoginError.SafeCloseModal(false));
    incorrectLoginError.SingleButtonColour = Xamarin.Forms.Color.Goldenrod;
    incorrectLoginError.SingleButtonText = "Try Again";
    incorrectLoginError.MainPopupInformation = "Your Phone Number or Pin is incorrect, please try again.";
    incorrectLoginError.MainPopupColour = Xamarin.Forms.Color.Honeydew;
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

Coming soon, however, the MIT license is what im thinking



<!-- CONTACT -->
## Contact

My [Github](https://github.com/LuckyDucko),
or reach me on the [Xamarin Slack](https://xamarinchat.herokuapp.com/),
or on my [E-mail](tyson@logchecker.com.au)

Project Link: [AwaitablePopups](https://github.com/LuckyDucko/AwaitablePopups)



<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements
* [Brimmick](https://github.com/brminnick) has been a model to follow (might steal his hackernews app for an example)

