using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Core.Templates
{
    public class TwoValueHorizontalGrid
    {
        Grid twoValueHorizontalGrid;
        public Grid Create() { 
            if(twoValueHorizontalGrid == null)
            {
                twoValueHorizontalGrid = new Grid()
                {
                    Padding = new Thickness(15, 0, 10, 0),
                    ColumnSpacing = 0,
                    RowSpacing = 0,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                twoValueHorizontalGrid.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(1, GridUnitType.Auto)
                });

                twoValueHorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                twoValueHorizontalGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }
            return twoValueHorizontalGrid;
        }
    }
}
