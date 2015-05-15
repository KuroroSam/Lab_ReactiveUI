using System;

using UIKit;
using ReactiveUI.Cocoa;
using ReactiveUI;
using Core.ViewModels;
using System.Reactive.Linq;
using CoreAnimation;
using Foundation;
using CoreGraphics;
using System.Collections.Generic;


namespace testXS
{
	public partial class ViewController : ReactiveViewController, IViewFor<LoginViewModel>
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			ViewModel = new LoginViewModel ();

			// Perform any additional setup after loading the view, typically from a nib.
			this.Bind(ViewModel, vm => vm.UserName, v => v.UserNameText.Text);
			this.Bind(ViewModel, vm => vm.Password, v => v.PasswordText.Text);
			this.BindCommand(ViewModel, vm => vm.Login, v => v.Login);

			//Listening to Commands from the View via WhenAnyObservable
			//the only thing we don't want to do in ViewModel is the navigation
			//Android and Window navigation should be fine but iOS doesn't make sense
			this.WhenAnyObservable (x => x.ViewModel.Login)
				.Where (x => x == true) //Filter the signal
				.Subscribe (x => {
				this.PerformSegue ("ToPlanView", this);
			});

			ViewModel.Login.ThrownExceptions.Subscribe (ex => {
				var message = ex.Message;
				infoLabel.Text = message;

				//Key frame animation
				var a = (CAKeyFrameAnimation)CAKeyFrameAnimation.FromKeyPath ("transform");  
				a.Values =  new NSObject[] {
					NSValue.FromCATransform3D(CATransform3D.MakeTranslation(-5.0f, 0.0f, 0.0f)),
					NSValue.FromCATransform3D(CATransform3D.MakeTranslation(5.0f, 0.0f, 0.0f))
				};
				a.AutoReverses = true;
				a.RepeatCount = 2;
				a.Duration = 0.1;

				this.View.Layer.AddAnimation(a,null);


//				UIView.AnimateNotify(0.2,0,0.6f,0.1f,UIViewAnimationOptions.CurveEaseOut  | UIViewAnimationOptions.Repeat,()=>{
//					var p = View.Center.X;
//					UIView.SetAnimationRepeatCount(4);
//					View.Center = new CGPoint(View.Center.X - 10,View.Center.Y);
//					View.Center = new CGPoint(View.Center.Y + 20,View.Center.Y);
//				},finished =>{
//					
//				});

//				
				//UI kitDynamics
//				UIDynamicAnimator animator = new UIDynamicAnimator(this.View.Superview);
//
//				var items = new UIView[] {View};
//
//				UIGravityBehavior gravityBehavior = new UIGravityBehavior(items);
//				animator.AddBehavior(gravityBehavior);
//
//				UICollisionBehavior collisionBehavior = new UICollisionBehavior(items);
//				collisionBehavior.TranslatesReferenceBoundsIntoBoundary = true;
//				animator.AddBehavior(collisionBehavior);
//
//				UIDynamicItemBehavior elasticityBehavior = new UIDynamicItemBehavior(items);
//				elasticityBehavior.Elasticity = 0.7f;
//				animator.AddBehavior(elasticityBehavior);


			});
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		LoginViewModel _ViewModel;
		public LoginViewModel ViewModel {
			get { return _ViewModel; }
			set { this.RaiseAndSetIfChanged(ref _ViewModel, value); }
		}

		object IViewFor.ViewModel {
			get { return ViewModel; }
			set { ViewModel = (LoginViewModel)value; }
		}
	}
}

