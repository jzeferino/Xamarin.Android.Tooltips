[![Build Status](https://www.bitrise.io/app/2991ad930c72ba28/status.svg?token=UYpiCuOXDGjUm8_sMTNabw)](https://www.bitrise.io/app/2991ad930c72ba28)

Xamarin.Android.Tooltips
===================

This is a Xamarin Android Binding for the [tooltips](https://github.com/tomergoldst/tooltips).

This project provides you a convenient way Simple to use library for android, enabling to add a tooltip near any view with ease.

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

Use the `ToolTip.Builder` to create and position you Tooltip.

```c#
builder = new ToolTip.Builder(
    this,
    _textView, /* anchor view */
    _rootLayout, /* root view, must be a RelativeLayout or FrameLayout. */
    text, /* tooltip text */
    ToolTip.PositionAbove); /* Position related to achor view */

builder.SetAlign(_align); /* arrow align */

_toolTipsManager.Show(builder.Build());
```

Run and play with the [sample](https://github.com/jzeferino/Xamarin.Android.Tooltips/tree/master/Xamarin.Android.Tooltips.Sample).

Read more detailed documention [original library project](https://github.com/tomergoldst/tooltips). 

### License
[MIT Licence](LICENSE) 
