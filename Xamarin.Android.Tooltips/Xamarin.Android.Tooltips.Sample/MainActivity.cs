using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Com.Tomergoldst.Tooltips;
using Android.Views;
using System;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Text;
using Android.Support.V4.Content;

namespace Xamarin.Android.Tooltips.Sample
{
	[Activity(Label = "Xamarin.Android.Tooltips.Sample", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/AppTheme")]
	public class MainActivity : AppCompatActivity, ToolTipsManager.ITipListener, View.IOnClickListener
	{
		private string Tag = nameof(MainActivity);
		private string _tip_text = "Tip";

		private ToolTipsManager _toolTipsManager;
		private RelativeLayout _rootLayout, _parentLayout;
		private TextInputEditText _editText;
		private TextView _textView;

		private Button _aboveBtn, _belowBtn, _leftToBtn, _rightToBtn;

		private RadioButton _alignRight, _alignLeft, _alignCenter;

		int _align = ToolTip.AlignCenter;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.main);

			_rootLayout = FindViewById<RelativeLayout>(Resource.Id.root_layout);
			_parentLayout = FindViewById<RelativeLayout>(Resource.Id.parent_layout);
			_textView = FindViewById<TextView>(Resource.Id.text_view);

			_toolTipsManager = new ToolTipsManager(this);

			_aboveBtn = FindViewById<Button>(Resource.Id.button_above);
			_belowBtn = FindViewById<Button>(Resource.Id.button_below);
			_leftToBtn = FindViewById<Button>(Resource.Id.button_left_to);
			_rightToBtn = FindViewById<Button>(Resource.Id.button_right_to);

			_aboveBtn.SetOnClickListener(this);
			_belowBtn.SetOnClickListener(this);
			_leftToBtn.SetOnClickListener(this);
			_rightToBtn.SetOnClickListener(this);

			_alignCenter = FindViewById<RadioButton>(Resource.Id.button_align_center);
			_alignLeft = FindViewById<RadioButton>(Resource.Id.button_align_left);
			_alignRight = FindViewById<RadioButton>(Resource.Id.button_align_right);

			_alignCenter.SetOnClickListener(this);
			_alignLeft.SetOnClickListener(this);
			_alignRight.SetOnClickListener(this);

			_alignCenter.Checked = true;

			_editText = FindViewById<TextInputEditText>(Resource.Id.text_input_edit_text);
		}

		public override void OnWindowFocusChanged(bool hasFocus)
		{
			base.OnWindowFocusChanged(hasFocus);

			ToolTip.Builder builder = new ToolTip.Builder(this, _textView, _rootLayout, _tip_text, ToolTip.PositionAbove);
			builder.SetAlign(_align);
			_toolTipsManager.Show(builder.Build());
		}

		public void OnTipDismissed(View view, int anchorViewId, bool byUser)
		{
			Log.Debug(Tag, "tip near anchor view " + anchorViewId + " dismissed");

			if (anchorViewId == Resource.Id.text_view)
			{
				// Do something when a tip near view with id "Resource.Id.text_view" has been dismissed
			}
		}

		public void OnClick(View view)
		{
			String text = TextUtils.IsEmpty(_editText.Text) ? _tip_text : _editText.Text;
			ToolTip.Builder builder;

			switch (view.Id)
			{
				case Resource.Id.button_above:
					_toolTipsManager.FindAndDismiss(_textView);
					builder = new ToolTip.Builder(this, _textView, _rootLayout, text, ToolTip.PositionAbove);
					builder.SetAlign(_align);
					_toolTipsManager.Show(builder.Build());
					break;
				case Resource.Id.button_below:
					_toolTipsManager.FindAndDismiss(_textView);
					builder = new ToolTip.Builder(this, _textView, _rootLayout, text, ToolTip.PositionBelow);
					builder.SetAlign(_align);
					builder.SetBackgroundColor(ContextCompat.GetColor(this, Resource.Color.colorOrange));
					_toolTipsManager.Show(builder.Build());
					break;
				case Resource.Id.button_left_to:
					_toolTipsManager.FindAndDismiss(_textView);
					builder = new ToolTip.Builder(this, _textView, _rootLayout, text, ToolTip.PositionLeftTo);
					builder.SetBackgroundColor(ContextCompat.GetColor(this, Resource.Color.colorLightGreen));
					builder.SetTextColor(ContextCompat.GetColor(this, Resource.Color.colorBlack));
					builder.SetGravity(ToolTip.GravityCenter);
					builder.SetTextSize(12);
					_toolTipsManager.Show(builder.Build());
					break;
				case Resource.Id.button_right_to:
					_toolTipsManager.FindAndDismiss(_textView);
					builder = new ToolTip.Builder(this, _textView, _rootLayout, text, ToolTip.PositionRightTo);
					builder.SetBackgroundColor(ContextCompat.GetColor(this, Resource.Color.colorDarkRed));
					builder.SetTextColor(ContextCompat.GetColor(this, Resource.Color.colorWhite));
					_toolTipsManager.Show(builder.Build());
					break;
				case Resource.Id.button_align_center:
					_align = ToolTip.AlignCenter;
					break;
				case Resource.Id.button_align_left:
					_align = ToolTip.AlignLeft;
					break;
				case Resource.Id.button_align_right:
					_align = ToolTip.AlignRight;
					break;
			}
		}
	}
}