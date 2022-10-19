using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AzurLaneDatabase
{
    internal class UIManager
    {
        BitmapImage normalBG = new BitmapImage(new Uri(@"/AzurLaneDatabase;component/Resources/bgs/normal.png", UriKind.Relative));
        BitmapImage rareBG = new BitmapImage(new Uri(@"/AzurLaneDatabase;component/Resources/bgs/rare.png", UriKind.Relative));
        BitmapImage srBG = new BitmapImage(new Uri(@"/AzurLaneDatabase;component/Resources/bgs/elite.png", UriKind.Relative));
        BitmapImage ssrBG = new BitmapImage(new Uri(@"/AzurLaneDatabase;component/Resources/bgs/ssr.png", UriKind.Relative));
        BitmapImage urBG = new BitmapImage(new Uri(@"/AzurLaneDatabase;component/Resources/bgs/ultrarare.png", UriKind.Relative));

        BitmapImage star = new BitmapImage(new Uri(@"/AzurLaneDatabase;component/Resources/star.png", UriKind.Relative));
        BitmapImage emptyStar = new BitmapImage(new Uri(@"/AzurLaneDatabase;component/Resources/emptystar.png", UriKind.Relative));

        WrapPanel wrapPanel = MainWindow.Instance.WrapPanel;

        TextBox searchInput = MainWindow.Instance.SearchText;

        Dictionary<Grid, Character> characterGrids = new Dictionary<Grid, Character>();

        (char, char)[] nonAscii = { ('É', 'E'), ('â', 'a'), ('è','e'), ('é','e'), ('ö','o'), ('ü','u') };

        public UIManager() {
            searchInput.TextChanged += On_TextChanged;
        }

        private void On_TextChanged(object sender, TextChangedEventArgs e) {
            TextBox tb = (TextBox)sender;

            if(tb.Text == "") {
                foreach(KeyValuePair<Grid, Character> value in characterGrids) {
                    value.Key.Visibility = Visibility.Visible;
                }
                return;
            }

            if(tb.Text.ToLower() == "owned") {
                foreach(KeyValuePair<Grid, Character> value in characterGrids) {
                    if(value.Value.isOwned) {
                        value.Key.Visibility = Visibility.Visible;
                    } else {
                        value.Key.Visibility = Visibility.Collapsed;
                    }
                }
                return;
            }

            if(tb.Text.ToLower() == "missing") {
                foreach(KeyValuePair<Grid, Character> value in characterGrids) {
                    if(!value.Value.isOwned) {
                        value.Key.Visibility = Visibility.Visible;
                    } else {
                        value.Key.Visibility = Visibility.Collapsed;
                    }
                }
                return;
            }

            if(tb.Text.ToLower() == "lb") {
                foreach(KeyValuePair<Grid, Character> value in characterGrids) {
                    if(value.Value.stars == value.Value.maxStars) {
                        value.Key.Visibility = Visibility.Collapsed;
                    } else {
                        value.Key.Visibility = Visibility.Visible;
                    }
                }
                return;
            }

            if(tb.Text.ToLower() == "retrofit") {
                foreach(KeyValuePair<Grid, Character> value in characterGrids) {
                    if(value.Value.canRetrofit) {
                        value.Key.Visibility = Visibility.Visible;
                    } else {
                        value.Key.Visibility = Visibility.Collapsed;
                    }
                }
                return;
            }

            if(tb.Text.ToLower() == "retrofitted") {
                foreach(KeyValuePair<Grid, Character> value in characterGrids) {
                    if(value.Value.isRetrofit) {
                        value.Key.Visibility = Visibility.Visible;
                    } else {
                        value.Key.Visibility = Visibility.Collapsed;
                    }
                }
                return;
            }

            foreach(KeyValuePair<Grid, Character> value in characterGrids) {
                string name = value.Value.Name;
                string asciiName = value.Value.Name;
                foreach((char, char) c in nonAscii) {
                    asciiName = asciiName.Replace(c.Item1, c.Item2);
                }
                if(name.ToLowerInvariant().Contains(tb.Text.ToLowerInvariant()) || asciiName.ToLowerInvariant().Contains(tb.Text.ToLowerInvariant())) {
                    value.Key.Visibility = Visibility.Visible;
                } else {
                    value.Key.Visibility = Visibility.Collapsed;
                }
            }
        }

        private BitmapImage GetRarityBG(Character character) {
            switch(character.rarity) {
                case Rarity.Normal:
                    return normalBG;
                case Rarity.Rare:
                    return rareBG;
                case Rarity.SR:
                    return srBG;
                case Rarity.SSR:
                    return ssrBG;
                case Rarity.UR:
                    return urBG;
            }
            return null;
        }

        public void CreateNewCharacterWindow(Character character) {
            Grid container = new Grid() {
                Height = 260,
                Width = 196,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
            };


            Rectangle blackBorder = new Rectangle() {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Fill = new SolidColorBrush(Colors.Black),
            };

            Image charRarityBG = new Image() {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 192,
                Height = 256
            };
            charRarityBG.Source = GetRarityBG(character);

            Image charImage = new Image() {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 192,
                Height = 256,
                Source = character.image,
            };

            if(character.isOwned) {
                charImage.Opacity = 1d;
            } else {
                charImage.Opacity = 0.3d;
            }

            Grid nameGrid = new Grid() {
                Height = 32,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 0, 35),
            };

            Rectangle nameBG = new Rectangle() {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Fill = new SolidColorBrush(Colors.Black),
                Opacity = 0.5d,
                Margin = new Thickness(0, 5, 0, 5),
            };

            Label nameLabel = new Label() {
                Content = character.Name,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush(Colors.White),
                Margin = new Thickness(),
            };

            // <Style TargetType="{x:Type Grid}"> <Setter Property="Margin" Value="1,0,1,0"/> </Style>
            StackPanel starStackPanel = new StackPanel() {
                VerticalAlignment = VerticalAlignment.Bottom,
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 8),
                //Style = starStyle,
            };


            List<Image> starImages = new List<Image>();

            for(int i = 0; i < 6; i++) {
                starImages.Add(new Image() {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 16,
                    Height = 16,
                    Source = emptyStar,
                    Visibility = Visibility.Collapsed
                });
            }
            for(int i = 0; i < character.maxStars; i++) {
                starImages[i].Visibility = Visibility.Visible;
            }
            for(int i = 0; i < character.stars; i++) {
                starImages[i].Source = star;
            }

            wrapPanel.Children.Add(container);

            container.Children.Add(blackBorder);
            container.Children.Add(charRarityBG);
            container.Children.Add(charImage);
            container.Children.Add(nameGrid);
            nameGrid.Children.Add(nameBG);
            nameGrid.Children.Add(nameLabel);

            container.Children.Add(starStackPanel);
            for(int i = 0; i < starImages.Count; i++) {
                starStackPanel.Children.Add(starImages[i]);
            }

            characterGrids.Add(container, character);

            starStackPanel.MouseDown += StarStackPanel_MouseDown;
            charImage.MouseDown += CharImage_MouseDown;

            character.rarityImage = charRarityBG;
            character.charImage = charImage;
        }

        private void ChangeStarImages(StackPanel s, Character character) {
            for(int i = 0; i < 6; i++) {
                Image id = (Image)s.Children[i];
                id.Source = emptyStar;
                id.Visibility = Visibility.Collapsed;
            }
            for(int i = 0; i < character.maxStars; i++) {
                Image id = (Image)s.Children[i];
                id.Visibility = Visibility.Visible;
            }
            for(int i = 0; i < character.stars; i++) {
                Image id = (Image)s.Children[i];
                id.Source = star;
            }
            ForceSearchRefresh();
        }

        void UpdateStarImages(Grid grid, Character character) {
            for(int i = 0; i < grid.Children.Count; i++) {
                if(grid.Children[i].GetType() == typeof(StackPanel)) {
                    ChangeStarImages((StackPanel)grid.Children[i], character);
                }
            }
        }

        private void CharImage_MouseDown(object sender, MouseButtonEventArgs e) {
            Image im = (Image)sender;
            Character character = characterGrids[(Grid)im.Parent];
            if(e.ChangedButton == MouseButton.Left) {
                character.isOwned = true;
                im.Opacity = 1d;
            }
            if(e.ChangedButton == MouseButton.Right) {
                character.isOwned = false;
                im.Opacity = 0.3d;
                character.ResetStars();
                UpdateStarImages((Grid)im.Parent, character);
            }
            if(e.ChangedButton == MouseButton.Left && Keyboard.Modifiers == ModifierKeys.Control) {
                character.isOwned = true;
                character.SetStars(character.maxStars);
                UpdateStarImages((Grid)im.Parent, character);
            }
            if(e.ChangedButton == MouseButton.Left && Keyboard.Modifiers == ModifierKeys.Shift) {
                if(character.Retrofit(true)) {
                    character.isOwned = true;
                    character.SetStars(character.maxStars);
                    UpdateStarImages((Grid)im.Parent, character);
                    character.rarityImage.Source = GetRarityBG(character);
                }
            }
            if(e.ChangedButton == MouseButton.Left && Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift)) {
                character.Retrofit(true);
                character.isOwned = true;
                character.SetStars(character.maxStars);
                UpdateStarImages((Grid)im.Parent, character);
                character.rarityImage.Source = GetRarityBG(character);
            }
            ForceSearchRefresh();
        }

        private void ForceSearchRefresh() {
            string orgText = searchInput.Text;
            searchInput.Text = @"https://www.youtube.com/watch?v=rW6M8D41ZWU";
            searchInput.Text = orgText;
            MainWindow.Instance.ChangeTitle(true);
        }

        private void StarStackPanel_MouseDown(object sender, MouseButtonEventArgs e) {
            StackPanel s = (StackPanel)sender;
            Character character = characterGrids[(Grid)s.Parent];
            if(e.ChangedButton == MouseButton.Left) {
                character.AddStar();
            }
            if(e.ChangedButton == MouseButton.Right) {
                character.RemoveStar();
            }
            if(e.ChangedButton == MouseButton.Middle) {
                character.SetStars(character.maxStars);
            }
            ChangeStarImages(s, character);
            ForceSearchRefresh();
        }
    }
}
