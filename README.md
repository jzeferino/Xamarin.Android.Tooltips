[![Build Status](https://www.bitrise.io/app/2991ad930c72ba28/status.svg?token=UYpiCuOXDGjUm8_sMTNabw)](https://www.bitrise.io/app/2991ad930c72ba28)
[![NuGet](https://img.shields.io/nuget/v/Xamarin.Android.Tooltips.svg?label=NuGet)](https://www.nuget.org/packages/Xamarin.Android.Tooltips/)

Xamarin.Android.Tooltips
===================

This is a Xamarin Android Binding for the [tooltips](https://github.com/tomergoldst/tooltips).

This project provides you a convenient way and simple to use library for android, enabling to add a tooltip near any view with ease.

<p align="center">
  <img src="https://github.com/jzeferino/Xamarin.Android.Tooltips/blob/master/art/tooltip.gif?raw=true"/>
</p>

## Usage

### Step 1

Install NuGet [package](https://www.nuget.org/packages/Xamarin.Android.Tooltips/).

### Step 2

Create a `ToolTipsManager`.

```c#
_toolTipsManager = new ToolTipsManager(this);
```

### Step 3

Use the `ToolTip.Builder` to create and position your Tooltip.

```c#
builder = new ToolTip.Builder(
    this,
    _textView,              /* anchor view */
    _rootLayout,            /* root view of entire layout, must be a RelativeLayout or FrameLayout. */
    text,                   /* tooltip text */
    ToolTip.PositionAbove); /* Position related to achor view */

builder.SetAlign(_align);   /* arrow align */

_toolTipsManager.Show(builder.Build());
```

### IMPORTANT NOTE:
If you want to how the tooltip immediately when the `Activity` is created you should Build and show the tooltip in the `OnWindowsFocusChanged` method, example:

```c#
public override void OnWindowFocusChanged(bool hasFocus)
{
    base.OnWindowFocusChanged(hasFocus);
    builder = new ToolTip.Builder(
        this,
        _textView,              /* anchor view */
        _rootLayout,            /* root view of entire layout, must be a RelativeLayout or FrameLayout. */
        text,                   /* tooltip text */
        ToolTip.PositionAbove); /* Position related to achor view */

    builder.SetAlign(_align);   /* arrow align */

    _toolTipsManager.Show(builder.Build());
}
```
In alternative you could use `View.Post` or `View.PostDelayed` in `OnCreate`.

### Sample
Run and play with the [sample](https://github.com/jzeferino/Xamarin.Android.Tooltips/tree/master/Xamarin.Android.Tooltips.Sample).

### More documentation
Read more detailed documention [original library project](https://github.com/tomergoldst/tooltips/blob/master/README.md). 

### License
[MIT Licence](LICENSE) 
