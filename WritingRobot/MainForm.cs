using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI;

namespace WriteRobot
{
	public partial class MainForm : RadRibbonForm
	{
		public MainForm()
		{
			InitializeComponent();

			this.radLabel23.Text = "What\'s new in " + VersionNumber.MarketingVersion;


			this.AllowAero = false;

			this.radRibbonBar1.StartButtonImage = ResFinder.MenuIcon;
			this.radRibbonBar1.RibbonBarElement.TabStripElement.SelectedItem =
				this.radRibbonBar1.RibbonBarElement.TabStripElement.Items[0];
			this.radRibbonBar1.QuickAccessToolBar.InnerItem.ContentLayout.Margin = new Padding(2, 0, 2, 0);

			this.MinimumSize = new Size(210, 140);
			this.DoubleBuffered = true;

			WireEvents();
		}

		private void WireEvents()
		{
			this.radRibbonBarBackstageView1.ItemClicked +=
				new System.EventHandler<BackstageItemEventArgs>(this.radRibbonBarBackstageView1_ItemClicked);
			// this.flowLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel2_Paint);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.ApplyGalleryStyle();
		}

		private void ApplyGalleryStyle()
		{
			foreach (RadGalleryItem item in this.radGalleryElement1.Items)
			{
				int childrenCount = item.Children.Count;

				if (childrenCount > 0)
				{
					RadElement element = item.Children[0];

					if (element is FillPrimitive)
					{
						FillPrimitive fillPrimitive = element as FillPrimitive;
						fillPrimitive.BackColor = Color.White;
					}
				}
			}
		}

		void recentPlacesListControl_CreatingVisualListItem(object sender, CreatingVisualListItemEventArgs args)
		{
			args.VisualItem = new PinnedListVisualItem();
		}

		void recentPlacesListControl_VisualItemFormatting(object sender, VisualItemFormattingEventArgs args)
		{
			// args.VisualItem.Image = global::Telerik.Examples.WinControls.Properties.Resources.open32;
		}

		void recentDocumentsListControl_VisualItemFormatting(object sender, VisualItemFormattingEventArgs args)
		{
			// args.VisualItem.Image = global::Telerik.Examples.WinControls.Properties.Resources.plain_text;
		}

		void recentDocumentsListControl_CreatingVisualListItem(object sender, CreatingVisualListItemEventArgs args)
		{
			args.VisualItem = new PinnedListVisualItem();
		}

		private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(Pens.LightGray,
				0,
				0,
				flowLayoutPanel2.Width - 1,
				flowLayoutPanel2.Height - 1);

			base.OnPaint(e);
		}

		private void radRibbonBarBackstageView1_ItemClicked(object sender, BackstageItemEventArgs e)
		{
			if (e.Item is BackstageButtonItem)
			{
				this.radRibbonBarBackstageView1.HidePopup();
			}
		}

		public void ApplyTheme(string themeName)
		{
			ThemeResolutionService.ApplyThemeToControlTree(this, themeName);
		}

		private void radRibbonBarGroup9_Click(object sender, EventArgs e)
		{
		}
	}

	public class PinnedListVisualItem : RadListVisualItem
	{
		private RadButtonElement pinImage;

		public PinnedListVisualItem()
		{
			AdjustVisibility();
		}

		protected override void CreateChildElements()
		{
			base.CreateChildElements();
			pinImage = new RadButtonElement();
			// pinImage.Image = global::Telerik.Examples.WinControls.Properties.Resources.pin;
			pinImage.Alignment = ContentAlignment.MiddleCenter;
			pinImage.ImageAlignment = ContentAlignment.MiddleCenter;
			pinImage.BorderElement.Visibility = ElementVisibility.Hidden;
			pinImage.ButtonFillElement.Visibility = ElementVisibility.Hidden;
			pinImage.RadPropertyChanged += new RadPropertyChangedEventHandler(pinImage_RadPropertyChanged);
			this.Children.Add(pinImage);
		}

		void pinImage_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
		{
			if (e.Property == ContainsMouseProperty)
			{
				AdjustVisibility();
			}
		}

		private void AdjustVisibility()
		{
			if (pinImage.ContainsMouse)
			{
				pinImage.BorderElement.Visibility = ElementVisibility.Visible;
				pinImage.ButtonFillElement.Visibility = ElementVisibility.Visible;
			}
			else
			{
				pinImage.BorderElement.Visibility = ElementVisibility.Hidden;
				pinImage.ButtonFillElement.Visibility = ElementVisibility.Hidden;
			}
		}

		protected override SizeF MeasureOverride(SizeF availableSize)
		{
			pinImage.Measure(availableSize);
			return base.MeasureOverride(availableSize);
		}

		protected override SizeF ArrangeOverride(SizeF finalSize)
		{
			finalSize.Width -= 36;
			base.ArrangeOverride(finalSize);
			finalSize.Width += 36;
			pinImage.Arrange(new RectangleF(finalSize.Width - 36, 0, 36, 36));

			return finalSize;
		}

		protected override Type ThemeEffectiveType
		{
			get { return typeof(RadListVisualItem); }
		}
	}
}