// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace InAppPurchaseTest
{
	[Register ("FeaturesController")]
	partial class FeaturesController
	{
		[Outlet]
		UIKit.UITextField AmountToConsume { get; set; }

		[Outlet]
		UIKit.UIButton ConsumeButton { get; set; }

		[Outlet]
		UIKit.UIView SpecialView { get; set; }

		[Outlet]
		UIKit.UILabel TotalCoins { get; set; }

		[Outlet]
		UIKit.UITapGestureRecognizer ViewTouched { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TotalCoins != null) {
				TotalCoins.Dispose ();
				TotalCoins = null;
			}

			if (AmountToConsume != null) {
				AmountToConsume.Dispose ();
				AmountToConsume = null;
			}

			if (ConsumeButton != null) {
				ConsumeButton.Dispose ();
				ConsumeButton = null;
			}

			if (SpecialView != null) {
				SpecialView.Dispose ();
				SpecialView = null;
			}

			if (ViewTouched != null) {
				ViewTouched.Dispose ();
				ViewTouched = null;
			}
		}
	}
}
