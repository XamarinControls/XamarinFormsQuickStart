using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Core.Models
{
    public class MasterListItem : ViewCell
    {
        public MasterListItem()
        {
            var image = new Image();
            var nameLabel = new Label();
            var typeLabel = new Label();
            var verticalLayout = new StackLayout();
            var horizontalLayout = new StackLayout() { BackgroundColor = Color.Olive };

            //set bindings
            //Title = "Home",
            //    IconSource = "ic_home_black_36dp.png",
            //    TargetType = typeof(HomePage)
            nameLabel.SetBinding(Label.TextProperty, new Binding("Title"));
            typeLabel.SetBinding(Label.TextProperty, new Binding("TargetType"));
            image.SetBinding(Image.SourceProperty, new Binding("IconSource"));

            //Set properties for desired design
            horizontalLayout.Orientation = StackOrientation.Horizontal;
            horizontalLayout.HorizontalOptions = LayoutOptions.Fill;
            image.HorizontalOptions = LayoutOptions.End;
            nameLabel.FontSize = 24;

            //add views to the view hierarchy
            verticalLayout.Children.Add(nameLabel);
            verticalLayout.Children.Add(typeLabel);
            horizontalLayout.Children.Add(verticalLayout);
            horizontalLayout.Children.Add(image);

            // add to parent view
            View = horizontalLayout;
        }
    }
}
