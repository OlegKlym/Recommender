using Xamarin.Forms;

namespace Recommender.Controls
{
    public class GridView : Grid
    {
        public static readonly BindableProperty ColumnsCountProperty = BindableProperty.Create
            (nameof(ColumnsCount), typeof(int), typeof(GridView), default(int), propertyChanged: ColumnsCountPropertyChanged);
        
        public int ColumnsCount
        {
            get => (int)GetValue(ColumnsCountProperty);
            set => SetValue(ColumnsCountProperty, value);
        }
        
        protected override void OnAdded(View view)
        {
            var addedChildren = Children.Count - 1;
            var row = addedChildren / ColumnsCount;
            var column = addedChildren % ColumnsCount;
            
            Children.Add(view, column, row);
        }

        protected override void OnRemoved(View view)
        {
            var removedItemColumn = GetColumn(view);
            var removedItemRow = GetRow(view);
            
            foreach (var child in Children)
            {
                var childColumn = GetColumn(child);
                var childRow = GetRow(child);

                // previous row - no actions
                if (childRow < removedItemRow)
                {
                    continue;
                }
                
                // same row - swipe left
                if (childRow == removedItemRow)
                {
                    if (childColumn <= removedItemColumn)
                    {
                        continue;
                    }
                    
                    Children.Add(child, childColumn - 1, childRow);
                }
                
                // next row
                if (childRow > removedItemRow)
                {
                    if (childColumn == 0)
                    {
                        // first column - swipe up
                        Children.Add(child, ColumnsCount - 1, childRow - 1);
                    }
                    else
                    {
                        // other columns - swipe left
                        Children.Add(child, childColumn - 1, childRow);
                    }
                }
            }
        }

        private static void ColumnsCountPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var gridView = bindable as GridView;
            var columnDefinitions = new ColumnDefinitionCollection();
            
            for (var i = 0; i < (int)newvalue; i++)
            {
                columnDefinitions.Add(new ColumnDefinition{Width = GridLength.Star});
            }

            gridView.ColumnDefinitions = columnDefinitions;
        }
    }
}